using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid id);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);

        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
        Task RemoverEnderecoFornecedor(Endereco endereco);
    }
}
