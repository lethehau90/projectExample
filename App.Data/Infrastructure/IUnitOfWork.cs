namespace App.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}