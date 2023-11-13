
namespace DevIO.Business.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
