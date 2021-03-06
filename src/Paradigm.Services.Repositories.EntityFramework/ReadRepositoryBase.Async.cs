﻿/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Paradigm.Services.Repositories.EntityFramework
{
    public abstract partial class ReadRepositoryBase<TEntity, TId, TContext> 
    {
        #region Implementation of IReadonlyRepository

        public abstract Task<TEntity> GetByIdAsync(TId id);

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await this.AsQueryable().ToListAsync();
        }

        #endregion
    }
}