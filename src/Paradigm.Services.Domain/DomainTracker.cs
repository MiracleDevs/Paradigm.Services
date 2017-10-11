/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System.Collections.Generic;

namespace Paradigm.Services.Domain
{
    /// <summary>
    /// Provides the means to track changes in entity collections.
    /// </summary>
    /// <remarks>
    /// When working with domain models, an entity can be related with other entities
    /// in 1-1, 1-Many, etc relationships. This class provides a way to track changes
    /// in the relationships. 
    /// 
    /// This framework is domain oriented, and tries to make use of the aggregate model.
    /// The aggregate root and parent classes, will keep track of changes in their "children".
    /// These changes will be later reported to the repository, and there to database access or
    /// dbcontext depending in which ORM is being used.
    /// </remarks>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class DomainTracker<TEntity> where TEntity: DomainBase
    {
        #region Properties

        /// <summary>
        /// Gets the removed list.
        /// </summary>
        private List<TEntity> RemovedList { get; }

        /// <summary>
        /// Gets the edited list.
        /// </summary>
        private List<TEntity> EditedList { get; }

        /// <summary>
        /// Gets the added list.
        /// </summary>
        private List<TEntity> AddedList { get; }

        /// <summary>
        /// Gets a readonly collection of entities that were removed.
        /// </summary>
        public IReadOnlyCollection<TEntity> Removed => this.RemovedList;

        /// <summary>
        /// Gets a readonly collection of entities that were edited.
        /// </summary>
        public IReadOnlyCollection<TEntity> Edited => this.EditedList;

        /// <summary>
        /// Gets a readonly collection of entities that were added.
        /// </summary>
        public IReadOnlyCollection<TEntity> Added => this.AddedList;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainTracker{TEntity}"/> class.
        /// </summary>
        public DomainTracker()
        {
            this.RemovedList = new List<TEntity>();
            this.EditedList = new List<TEntity>();
            this.AddedList = new List<TEntity>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Marks specified entity as added.
        /// </summary>
        /// <param name="entity">The entity to mark.</param>
        public void Add(TEntity entity)
        {
            this.AddedList.Add(entity);
        }

        /// <summary>
        /// Marks specified entity as edited.
        /// </summary>
        /// <param name="entity">The entity to mark.</param>
        public void Edit(TEntity entity)
        {
            this.EditedList.Add(entity);
        }

        /// <summary>
        /// Marks specified entity as removed.
        /// </summary>
        /// <param name="entity">The entity to mark.</param>
        public void Remove(TEntity entity)
        {
            this.RemovedList.Add(entity);
        }

        #endregion
    }
}