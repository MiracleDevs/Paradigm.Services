using System;
using System.Threading.Tasks;

namespace Paradigm.Services.WorkingTasks.EntityFramework
{
    public partial class TransactionalWorkTask
    {
        #region Overrides

        protected override async Task BeforeExecuteAsync()
        {
            try
            {
                await base.BeforeExecuteAsync();
                this.Transaction = await this.Context.Database.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Transaction couldn't be created.", ex);
            }
        }

        protected override async Task AfterExecuteFailedAsync()
        {
            if (this.Transaction == null)
                return;

            await base.AfterExecuteFailedAsync();
            this.Transaction.Rollback();
            this.Transaction.Dispose();
        }

        protected override async Task AfterExecuteSucceedAsync()
        {
            await base.AfterExecuteSucceedAsync();
            this.Transaction.Commit();
        }

        #endregion
    }
}
