using DevIO.Business.Models;
using DevIO.Data.Context;

namespace DevIO.Api.Configuration
{
    public static class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<MeuDbContext>();

            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                await context.Database.EnsureCreatedAsync();
                await EnsureSeedProducts(context);
            }
        }

        private static async Task EnsureSeedProducts(MeuDbContext context)
        {
            if (context.Fornecedores.Any())
                return;

            var idFornecedor = Guid.NewGuid();

            await context.Fornecedores.AddAsync(new Fornecedor() { 
                Id = idFornecedor,
                Nome = "Fornecedor Teste", 
                Documento = "49445522389",
                TipoFornecedor = TipoFornecedor.PessoaFisica,
                Ativo = true,
                Endereco = new Endereco() 
                {
                   Logradouro = "Rua Teste",
                   Numero = "123",
                   Complemento = "Complemento",
                   Bairro = "Teste",
                   Cep = "03180000",
                   Cidade = "São Paulo",
                   Estado = "SP"
                }
            });

            await context.SaveChangesAsync();

            if (context.Produtos.Any())
                return;

            await context.Produtos.AddAsync(new Produto() { Nome = "Livro CSS",    Valor = 50,  Descricao = "Teste",
            Ativo = true, DataCadastro = DateTime.Now, FornecedorId = idFornecedor});

            await context.Produtos.AddAsync(new Produto() { Nome = "Livro jQuery", Valor = 150, Descricao = "Teste",
            Ativo = true, DataCadastro = DateTime.Now, FornecedorId = idFornecedor});
            
            await context.Produtos.AddAsync(new Produto() { Nome = "Livro HTML",   Valor = 90,  Descricao = "Teste",
            Ativo = true, DataCadastro = DateTime.Now, FornecedorId = idFornecedor});
            
            await context.Produtos.AddAsync(new Produto() { Nome = "Livro Razor", Valor = 50,  Descricao = "Teste", 
            Ativo = true, DataCadastro = DateTime.Now, FornecedorId = idFornecedor});

            await context.SaveChangesAsync();            
        }
    }
}
