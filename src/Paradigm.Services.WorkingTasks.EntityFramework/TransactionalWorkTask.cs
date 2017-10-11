/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Paradigm.Services.WorkingTasks.EntityFramework
{
    public partial class TransactionalWorkTask : WorkTask, ITransactionalWorkTask
    {
        #region Properties

        private DbContext Context { get; set; }

        private IDbContextTransaction Transaction { get; set; }

        #endregion

        #region Constructor

        public TransactionalWorkTask(DbContext context)
        {
            this.Context = context;
        }

        #endregion

        #region Overrides

        protected override void BeforeExecute()
        {
            try
            {
                base.BeforeExecute();
                this.Transaction = this.Context.Database.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception("Transaction couldn't be created.", ex);
            }
        }

        protected override void AfterExecuteFailed()
        {
            if (this.Transaction == null)
                return;

            base.AfterExecuteFailed();
            this.Transaction.Rollback();
            this.Transaction.Dispose();
        }

        protected override void AfterExecuteSucceed()
        {
            base.AfterExecuteSucceed();
            this.Transaction.Commit();
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            this.Transaction?.Dispose();
            this.Transaction = null;
            this.Context = null;
        }

        #endregion
    }
}
