using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository,
                              INotificador notificador,
                              IUnitOfWork uow) : base(notificador, uow)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            var produtoExistente = await _produtoRepository.ObterPorId(produto.Id);

            if(produtoExistente != null)
            {
                Notificar("Já existe um produto com o ID informado!");
                return;
            }

            _produtoRepository.Adicionar(produto);

            await Commit();
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            _produtoRepository.Atualizar(produto);
            await Commit();
        }

        public async Task Remover(Guid id)
        {
            _produtoRepository.Remover(id);
            await Commit();
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
