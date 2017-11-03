/*!
 * Paradigm Framework - Service Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Paradigm.Services.Interfaces;
using Paradigm.Services.Interfaces.Extensions;

namespace Paradigm.Services.Domain
{
    /// <summary>
    /// Provides base functionality and methods for domain entities.
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class DomainBase
    {
        #region Public Methods

        /// <summary>
        /// Method called by the repositories before adding the new entity.
        /// </summary>
        public virtual void BeforeAdd()
        {
        }

        /// <summary>
        /// Method called by the repository after adding the new entity.
        /// </summary>
        public virtual void AfterAdd()
        {
        }

        /// <summary>
        /// Method called by the repository before editing an existing entity.
        /// </summary>
        public virtual void BeforeEdit()
        {
        }

        /// <summary>
        /// Method called by the repository after editing an existing entity.
        /// </summary>
        public virtual void AfterEdit()
        {
        }

        /// <summary>
        /// Method called by the repository before adding or editing an entity.
        /// </summary>
        public virtual void BeforeSave()
        {
        }

        /// <summary>
        /// Method called by the repository after adding or editing an entity.
        /// </summary>
        public virtual void AfterSave()
        {
        }

        /// <summary>
        /// Method called by the repository before removing an existing entity.
        /// </summary>
        public virtual void BeforeRemove()
        {
        }

        /// <summary>
        /// Method called by the repository after removing an existing entity.
        /// </summary>
        public virtual void AfterRemove()
        {
        }

        #endregion
    }

    /// <summary>
    /// Provides base functionality and methods for domain entities.
    /// </summary>
    /// <typeparam name="TInterface">The type of the domain interface.</typeparam>
    /// <typeparam name="TEntity">The type of domian entity.</typeparam>
    [DataContract]
    [Serializable]
    public abstract class DomainBase<TInterface, TEntity>: DomainBase, IDomainInterface
    where TInterface : IDomainInterface
    where TEntity : DomainBase<TInterface, TEntity>, TInterface, new()
    {
        #region Public Methods

        /// <summary>
        /// Maps the entity properties from a contract interface.
        /// </summary>
        /// <param name="contract">The contract to map.</param>
        /// <returns>A reference to the entity.</returns>
        public virtual TEntity MapFrom(TInterface contract)
        {
            return null;
        }

        /// <summary>
        /// Determines whether this entity is new.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is new; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsNew();

        /// <summary>
        /// Validates this entity.
        /// </summary>
        public virtual void Validate()
        {
            this.ValidateEntity();
        }

        /// <summary>
        /// Method called by the repositories before adding the new entity.
        /// </summary>
        /// <remarks>
        /// Before the adding occurs, the entity will be validated.
        /// </remarks>
        public override void BeforeAdd()
        {
            base.BeforeAdd();
            this.Validate();
        }

        /// <summary>
        /// Method called by the repository before editing an existing entity.
        /// </summary>
        /// <remarks>
        /// Before the adding occurs, the entity will be validated.
        /// </remarks>
        public override void BeforeEdit()
        {
            base.BeforeEdit();
            this.Validate();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets a list of properties to ignore when validating the entity contents.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<string> GetPropertiesToIgnoreInValidation()
        {
            return new List<string>();
        }

        /// <summary>
        /// Validates this entity.
        /// </summary>
        /// <param name="ignoreProperties">A list of properties ignored when validating the contents of the entity.</param>
        protected void ValidateEntity(IEnumerable<string> ignoreProperties = null)
        {
            this.BeforeValidate();
            this.ValidateAndThrow(null, ignoreProperties == null ? this.GetPropertiesToIgnoreInValidation() : ignoreProperties.Union(this.GetPropertiesToIgnoreInValidation()));
            this.AfterValidate();
        }

        /// <summary>
        /// Method called before the validation occurs.
        /// </summary>
        protected virtual void BeforeValidate()
        {
        }

        /// <summary>
        /// Method called after the validation occurs.
        /// </summary>
        protected virtual void AfterValidate()
        {
        }

        #endregion
    }
}