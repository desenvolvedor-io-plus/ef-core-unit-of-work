using DevIO.Business.Models;
using System.Linq.Expressions;

namespace DevIO.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();

        void Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        void Remover(Guid id);   
        
        Task<int> SaveChanges();
    }
}
