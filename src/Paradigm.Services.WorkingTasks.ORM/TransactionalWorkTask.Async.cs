using System;
using System.Threading.Tasks;

namespace Paradigm.Services.WorkingTasks.ORM
{
    public partial class TransactionalWorkTask
    {
        #region Overrides

        protected override async Task BeforeExecuteAsync()
        {
            try
            {
                await base.BeforeExecuteAsync();
                this.Transaction = this.Connector.CreateTransaction(this.Isolationlevel);
            }
            catch (Exception ex)
            {
                throw new Exception("Transaction couldn't be created.", ex);
            }
        }

        protected override async Task AfterExecuteFailedAsync()
        {
            await base.AfterExecuteFailedAsync();
            await this.Transaction.RollbackAsync();
            this.Transaction.Dispose();
        }

        protected override async Task AfterExecuteSucceedAsync()
        {
            await base.AfterExecuteSucceedAsync();
            await this.Transaction.CommitAsync();
        }

        #endregion
    }
}
