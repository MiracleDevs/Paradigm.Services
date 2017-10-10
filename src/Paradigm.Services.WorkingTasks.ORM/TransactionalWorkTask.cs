using System;
using System.Data;
using Paradigm.ORM.Data.Database;

namespace Paradigm.Services.WorkingTasks.ORM
{
    public partial class TransactionalWorkTask : WorkTask,  ITransactionalWorkTask
    {
        #region Properties

        private IDatabaseConnector Connector { get; set; }

        private IDatabaseTransaction Transaction { get; set; }

        private IsolationLevel Isolationlevel { get; }

        #endregion

        #region Constructor

        public TransactionalWorkTask(IDatabaseConnector connector, IsolationLevel isolationLevel = IsolationLevel.Serializable)
        {
            this.Connector = connector;
            this.Isolationlevel = isolationLevel;
        }

        #endregion

        #region Overrides

        protected override void BeforeExecute()
        {
            try
            {
                base.BeforeExecute();
                this.Transaction = this.Connector.CreateTransaction(this.Isolationlevel);
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
            this.Connector = null;
        }

        #endregion
    }
}
