/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Domain;
using Paradigm.Services.Exceptions;
using Paradigm.Services.Interfaces;
using Paradigm.Services.Repositories;
using Paradigm.Services.Repositories.UOW;

namespace Paradigm.Services.Providers
{
    public abstract partial class EditProviderBase<TInterface, TDomain, TView, TDomainRepository, TViewRepository, TId> :
        ReadProviderBase<TView, TViewRepository, TId>,
        IEditProvider<TInterface, TDomain, TView, TId>
        where TInterface : IDomainInterface
        where TDomain : DomainBase<TInterface, TDomain>, TInterface, new()
        where TView : DomainBase
        where TDomainRepository : IEditRepository<TDomain, TId>
        where TViewRepository : IReadRepository<TView, TId>
    {
        #region Constructor

        protected EditProviderBase(IServiceProvider serviceProvider, IUnitOfWork unitOfWork) : base(serviceProvider, unitOfWork)
        {
        }

        #endregion

        #region Public Methods

        public virtual List<TDomain> FindEntity()
        {
            return this.GetDomainRepository().GetAll();
        }

        public virtual TDomain GetEntity(TId id)
        {
            return this.CheckEntity(this.GetDomainRepository().GetById(id));
        }

        public virtual TDomain Add(TInterface contract)
        {
            var entity = this.GetNewDomainEntity();

            this.BeforeCreate(entity, contract);
            this.BeforeCreateAsync(entity, contract).Wait();
            this.BeforeSave(entity, contract);
            this.BeforeSaveAsync(entity, contract).Wait();

            entity.MapFrom(contract);
            this.GetDomainRepository().Add(entity);
            this.UnitOfWork.CommitChanges();

            this.AfterCreate(entity, contract);
            this.AfterCreateAsync(entity, contract).Wait();
            this.AfterSave(entity, contract);
            this.AfterSaveAsync(entity, contract).Wait();

            return entity;
        }

        public virtual IEnumerable<Tuple<TDomain, TInterface>> Add(IEnumerable<TInterface> contracts)
        {
            var repository = this.GetDomainRepository();
            var entities = new List<Tuple<TDomain, TInterface>>();

            foreach (var contract in contracts)
            {
                var entity = this.GetNewDomainEntity();

                this.BeforeCreate(entity, contract);
                this.BeforeCreateAsync(entity, contract).Wait();
                this.BeforeSave(entity, contract);
                this.BeforeSaveAsync(entity, contract).Wait();

                entity.MapFrom(contract);
                entities.Add(new Tuple<TDomain, TInterface>(entity, contract));
            }

            repository.Add(entities.Select(x => x.Item1));
            this.UnitOfWork.CommitChanges();

            foreach (var entity in entities)
            {
                this.AfterCreate(entity.Item1, entity.Item2);
                this.AfterCreateAsync(entity.Item1, entity.Item2).Wait();
                this.AfterSave(entity.Item1, entity.Item2);
                this.AfterSaveAsync(entity.Item1, entity.Item2).Wait();
            }

            return entities;
        }

        public virtual TDomain Edit(TInterface contract, TId id)
        {
            var repository = this.GetDomainRepository();
            var entity = this.CheckEntity(repository.GetById(id));

            this.BeforeEdit(entity, contract);
            this.BeforeEditAsync(entity, contract).Wait();
            this.BeforeSave(entity, contract);
            this.BeforeSaveAsync(entity, contract).Wait();

            entity.MapFrom(contract);
            repository.Edit(entity);
            this.UnitOfWork.CommitChanges();

            this.AfterEdit(entity, contract);
            this.AfterEditAsync(entity, contract).Wait();
            this.AfterSave(entity, contract);
            this.AfterSaveAsync(entity, contract).Wait();

            return entity;
        }

        public virtual IEnumerable<Tuple<TDomain, TInterface>> Edit(IEnumerable<TInterface> contracts, Func<TInterface, TId> getByIdPredicate)
        {
            var repository = this.GetDomainRepository();
            var entities = new List<Tuple<TDomain, TInterface>>();

            foreach (var contract in contracts)
            {
                var entity = this.CheckEntity(repository.GetById(getByIdPredicate(contract)));

                this.BeforeEdit(entity, contract);
                this.BeforeEditAsync(entity, contract).Wait();
                this.BeforeSave(entity, contract);
                this.BeforeSaveAsync(entity, contract).Wait();

                entity.MapFrom(contract);
                entities.Add(new Tuple<TDomain, TInterface>(entity, contract));
            }

            repository.Edit(entities.Select(x => x.Item1));
            this.UnitOfWork.CommitChanges();

            foreach (var entity in entities)
            {
                this.AfterEdit(entity.Item1, entity.Item2);
                this.AfterEditAsync(entity.Item1, entity.Item2).Wait();
                this.AfterSave(entity.Item1, entity.Item2);
                this.AfterSaveAsync(entity.Item1, entity.Item2).Wait();
            }

            return entities;
        }

        public virtual void Remove(TId id)
        {
            var repository = this.GetDomainRepository();
            var entity = this.CheckEntity(repository.GetById(id));

            this.BeforeRemove(entity);
            this.BeforeRemoveAsync(entity).Wait();

            repository.Remove(entity);
            this.UnitOfWork.CommitChanges();

            this.AfterRemove(entity);
            this.AfterRemoveAsync(entity).Wait();
        }

        public virtual void Remove(IEnumerable<TId> ids)
        {
            var repository = this.GetDomainRepository();
            var entities = new List<TDomain>();

            foreach (var id in ids)
            {
                var entity = this.CheckEntity(repository.GetById(id));

                this.BeforeRemove(entity);
                this.BeforeRemoveAsync(entity).Wait();

                entities.Add(entity);
            }

            repository.Remove(entities);
            this.UnitOfWork.CommitChanges();

            foreach (var entity in entities)
            {
                this.AfterRemove(entity);
                this.AfterRemoveAsync(entity).Wait();
            }
        }

        public virtual TDomain Save(TInterface contract, TId id)
        {
            return contract.IsNew() ? this.Add(contract) : this.Edit(contract, id);
        }

        public virtual TView Save(TInterface contract, Func<TInterface, TId> getByIdPredicate)
        {
            return this.GetView(getByIdPredicate(this.Save(contract, getByIdPredicate(contract))));
        }

        public virtual IEnumerable<Tuple<TDomain, TInterface>> Save(IEnumerable<TInterface> contracts, Func<TInterface, TId> getByIdPredicate)
        {
            var repository = this.GetDomainRepository();

            var newEntities = new List<Tuple<TDomain, TInterface>>();
            var oldEntities = new List<Tuple<TDomain, TInterface>>();

            foreach (var contract in contracts)
            {
                if (contract.IsNew())
                {
                    var entity = this.GetNewDomainEntity();

                    this.BeforeCreate(entity, contract);
                    this.BeforeCreateAsync(entity, contract).Wait();

                    entity.MapFrom(contract);

                    newEntities.Add(new Tuple<TDomain, TInterface>(entity, contract));
                }
                else
                {
                    var entity = repository.GetById(getByIdPredicate(contract));

                    this.BeforeEdit(entity, contract);
                    this.BeforeEditAsync(entity, contract).Wait();

                    entity.MapFrom(contract);

                    oldEntities.Add(new Tuple<TDomain, TInterface>(entity, contract));
                }
            }

            repository.Add(newEntities.Select(x => x.Item1));
            repository.Edit(oldEntities.Select(x => x.Item1));

            this.UnitOfWork.CommitChanges();

            foreach (var entity in newEntities)
            {
                this.AfterCreate(entity.Item1, entity.Item2);
                this.AfterCreateAsync(entity.Item1, entity.Item2).Wait();
            }
            foreach (var entity in oldEntities)
            {
                this.AfterEdit(entity.Item1, entity.Item2);
                this.AfterEditAsync(entity.Item1, entity.Item2).Wait();
            }

            return newEntities.Union(oldEntities);
        }

        #endregion

        #region Protected Methods

        protected virtual TDomain GetNewDomainEntity()
        {
            return this.ServiceProvider.GetService<TDomain>() ?? Activator.CreateInstance<TDomain>();
        }

        protected virtual TDomainRepository GetDomainRepository()
        {
            return this.GetRepository<TDomainRepository>();
        }

        protected virtual void BeforeCreate(TDomain entity, TInterface contract)
        {
        }

        protected virtual void AfterCreate(TDomain entity, TInterface contract)
        {
        }

        protected virtual void BeforeEdit(TDomain entity, TInterface contract)
        {
        }

        protected virtual void AfterEdit(TDomain entity, TInterface contract)
        {
        }

        protected virtual void BeforeSave(TDomain entity, TInterface contract)
        {
        }

        protected virtual void AfterSave(TDomain entity, TInterface contract)
        {
        }

        protected virtual void BeforeRemove(TDomain entity)
        {
        }

        protected virtual void AfterRemove(TDomain entity)
        {
        }

        protected virtual TDomain CheckEntity(TDomain entity)
        {
            if (entity == null)
            {
                throw new NotFoundException(typeof(TDomain));
            }

            return entity;
        }

        #endregion
    }
}