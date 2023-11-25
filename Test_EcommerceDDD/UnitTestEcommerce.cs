using Domain.Entities;
using Domain.Interfaces.InferfaceServices;
using Domain.Interfaces.InterfaceProduto;
using Domain.Services;
using Infra.Repositories.RepositoryProduto;
using System.ComponentModel.DataAnnotations;

namespace Test_EcommerceDDD
{
    [TestClass]
    public class UnitTestEcommerce
    {
        [TestMethod]
      public async Task AddProdutoComSucesso()
        {
            try
            {
                IProduto _Iproduto = new RepositoryProduto();
                IProdutoService _IserviceProduto = new ProdutoService(_Iproduto);
                var produto = new Produto
                {
                    Descricao = string.Concat("Descrição teste TDD", DateTime.Now.ToString()),
                    QtdCompra = 10,
                    Nome = string.Concat("Nome teste TDD", DateTime.Now.ToString()),
                    Valor = 20,
                    UserId = "2b6a5d5c-196c-4f08-bbe9-88ed4f8c50fc",
                    Observacao = string.Concat("Observação teste TDD", DateTime.Now.ToString()),
                };

                await _IserviceProduto.AddProduto(produto);

                Assert.IsNotNull(produto.Id);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
          
               
        }

        [TestMethod]
        public async Task AddProdutoComValidacaoCampoObrigatorio()
        {
            try
            {
                IProduto _Iproduto = new RepositoryProduto();
                IProdutoService _IserviceProduto = new ProdutoService(_Iproduto);
                var produto = new Produto
                {

                };

               await Assert.ThrowsExceptionAsync<ValidationException>(async () => await _IserviceProduto.AddProduto(produto));
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ListaProdutoUsuario()
        {
            try
            {
                IProduto _Iproduto = new RepositoryProduto();
                var listaProduto = await _Iproduto.ListarUsuarioLogado("2b6a5d5c-196c-4f08-bbe9-88ed4f8c50fc");

                Assert.IsTrue(listaProduto.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetEntityById()
        {
            try
            {
                IProduto _Iproduto = new RepositoryProduto();
                var listaProduto = await _Iproduto.ListarUsuarioLogado("2b6a5d5c-196c-4f08-bbe9-88ed4f8c50fc");
                var produto = await _Iproduto.GetEntityById(listaProduto.LastOrDefault().Id);

                Assert.IsTrue(produto != null);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            try
            {
                IProduto _Iproduto = new RepositoryProduto();
                var listaProduto = await _Iproduto.ListarUsuarioLogado("2b6a5d5c-196c-4f08-bbe9-88ed4f8c50fc");
                var ultimoProduto = listaProduto.LastOrDefault();
                await _Iproduto.Delete(ultimoProduto);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

    }
}



