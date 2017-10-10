using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradigm.Services.Providers
{
    public abstract partial class EditProviderBase<TInterface, TDomain, TView, TDomainRepository, TViewRepository, TId>
    {
        #region Public Methods

        public virtual async Task<List<TDomain>> FindEntityAsync()
        {
            return await this.GetDomainRepository().GetAllAsync();
        }

        public virtual async Task<TDomain> GetEntityAsync(TId id)
        {
            return this.CheckEntity(await this.GetDomainRepository().GetByIdAsync(id));
        }

        public virtual async Task<TDomain> AddAsync(TInterface contract)
        {
            var entity = new TDomain();
            
            this.BeforeCreate(entity, contract);
            await this.BeforeCreateAsync(entity, contract);

            entity.MapFrom(contract);
            await this.GetDomainRepository().AddAsync(entity);
            await this.UnitOfWork.CommitChangesAsync();

            this.AfterCreate(entity, contract);
            await this.AfterCreateAsync(entity, contract);

            return entity;
        }

        public virtual async Task<IEnumerable<Tuple<TDomain, TInterface>>> AddAsync(IEnumerable<TInterface> contracts)
        {
            var repository = this.GetDomainRepository();
            var entities = new List<Tuple<TDomain, TInterface>>();

            foreach (var contract in contracts)
            {
                var entity = new TDomain();

                this.BeforeCreate(entity, contract);
                await this.BeforeCreateAsync(entity, contract);

                entity.MapFrom(contract);
                entities.Add(new Tuple<TDomain, TInterface>(entity, contract));
            }

            await repository.AddAsync(entities.Select(x => x.Item1));
            await this.UnitOfWork.CommitChangesAsync();

            foreach (var entity in entities)
            {
                this.AfterCreate(entity.Item1, entity.Item2);
                await this.AfterCreateAsync(entity.Item1, entity.Item2);
            }

            return entities;
        }

        public virtual async Task<TDomain> EditAsync(TInterface contract, TId id)
        {
            var repository = this.GetDomainRepository();
            var entity = this.CheckEntity(await repository.GetByIdAsync(id));

            this.BeforeEdit(entity, contract);
            await this.BeforeEditAsync(entity, contract);

            entity.MapFrom(contract);
            await repository.EditAsync(entity);
            await this.UnitOfWork.CommitChangesAsync();

            this.AfterEdit(entity, contract);
            await this.AfterEditAsync(entity, contract);

            return entity;
        }

        public virtual async Task<IEnumerable<Tuple<TDomain, TInterface>>> EditAsync(IEnumerable<TInterface> contracts, Func<TInterface, TId> getByIdPredicate)
        {
            var repository = this.GetDomainRepository();
            var entities = new List<Tuple<TDomain, TInterface>>();

            foreach (var contract in contracts)
            {
                var entity = this.CheckEntity(await repository.GetByIdAsync(getByIdPredicate(contract)));

                this.BeforeEdit(entity, contract);
                await this.BeforeEditAsync(entity, contract);

                entity.MapFrom(contract);
                entities.Add(new Tuple<TDomain, TInterface>(entity, contract));
            }

            await repository.EditAsync(entities.Select(x => x.Item1));
            await this.UnitOfWork.CommitChangesAsync();

            foreach (var entity in entities)
            {
                this.AfterEdit(entity.Item1, entity.Item2);
                await this.AfterEditAsync(entity.Item1, entity.Item2);
            }

            return entities;
        }

        public virtual async Task RemoveAsync(TId id)
        {
            var repository = this.GetDomainRepository();
            var entity = this.CheckEntity(await repository.GetByIdAsync(id));

            this.BeforeRemove(entity);
            await this.BeforeRemoveAsync(entity);

            await repository.RemoveAsync(entity);
            await this.UnitOfWork.CommitChangesAsync();

            this.AfterRemove(entity);
            await this.AfterRemoveAsync(entity);
        }

        public virtual async Task RemoveAsync(IEnumerable<TId> ids)
        {
            var repository = this.GetDomainRepository();
            var entities = new List<TDomain>();

            foreach (var id in ids)
            {
                var entity = this.CheckEntity(await repository.GetByIdAsync(id));

                this.BeforeRemove(entity);
                await this.BeforeRemoveAsync(entity);

                entities.Add(entity);
            }

            await repository.RemoveAsync(entities);
            await this.UnitOfWork.CommitChangesAsync();

            foreach (var entity in entities)
            {
                this.AfterRemove(entity);
                await this.AfterRemoveAsync(entity);
            }
        }

        public virtual async Task<TDomain> SaveAsync(TInterface contract, TId id)
        {
            return contract.IsNew() ? await this.AddAsync(contract) : await this.EditAsync(contract, id);
        }

        public virtual async Task<TView> SaveAsync(TInterface contract, Func<TInterface, TId> getByIdPredicate)
        {
            return await this.GetViewAsync(getByIdPredicate(await this.SaveAsync(contract, getByIdPredicate(contract))));
        }

        public virtual async Task<IEnumerable<Tuple<TDomain, TInterface>>> SaveAsync(IEnumerable<TInterface> contracts, Func<TInterface, TId> getByIdPredicate)
        {
            var repository = this.GetDomainRepository();

            var newEntities = new List<Tuple<TDomain, TInterface>>();
            var oldEntities = new List<Tuple<TDomain, TInterface>>();

            foreach (var contract in contracts)
            {
                if (contract.IsNew())
                {
                    var entity = new TDomain();

                    this.BeforeCreate(entity, contract);
                    await this.BeforeCreateAsync(entity, contract);

                    entity.MapFrom(contract);

                    newEntities.Add(new Tuple<TDomain, TInterface>(entity, contract));
                }
                else
                {
                    var entity = await repository.GetByIdAsync(getByIdPredicate(contract));

                    this.BeforeEdit(entity, contract);
                    await this.BeforeEditAsync(entity, contract);

                    entity.MapFrom(contract);
                    oldEntities.Add(new Tuple<TDomain, TInterface>(entity, contract));
                }
            }

            await repository.AddAsync(newEntities.Select(x => x.Item1));
            await repository.EditAsync(oldEntities.Select(x => x.Item1));

            await this.UnitOfWork.CommitChangesAsync();

            foreach (var entity in newEntities)
            {
                this.AfterCreate(entity.Item1, entity.Item2);
                await this.AfterCreateAsync(entity.Item1, entity.Item2);
            }

            foreach (var entity in oldEntities)
            {
                this.AfterEdit(entity.Item1, entity.Item2);
                await this.AfterEditAsync(entity.Item1, entity.Item2);
            }

            return newEntities.Union(oldEntities);
        }

        #endregion

        #region Protected Methods

        protected virtual Task BeforeCreateAsync(TDomain entity, TInterface contract)
        {
            return System.Threading.Tasks.Task.FromResult(default(object));
        }

        protected virtual Task AfterCreateAsync(TDomain entity, TInterface contract)
        {
            return System.Threading.Tasks.Task.FromResult(default(object));
        }

        protected virtual Task BeforeEditAsync(TDomain entity, TInterface contract)
        {
            return System.Threading.Tasks.Task.FromResult(default(object));
        }

        protected virtual Task AfterEditAsync(TDomain entity, TInterface contract)
        {
            return System.Threading.Tasks.Task.FromResult(default(object));
        }

        protected virtual Task BeforeRemoveAsync(TDomain entity)
        {
            return System.Threading.Tasks.Task.FromResult(default(object));
        }

        protected virtual Task AfterRemoveAsync(TDomain entity)
        {
            return System.Threading.Tasks.Task.FromResult(default(object));
        }

        #endregion
    }
}