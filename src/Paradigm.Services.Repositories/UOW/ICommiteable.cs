namespace Paradigm.Services.Repositories.UOW
{
    public partial interface ICommiteable
    {
        void CommitChanges();
    }
}