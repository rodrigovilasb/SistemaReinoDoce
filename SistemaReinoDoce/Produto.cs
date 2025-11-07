using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SistemaReinoDoce
{
    internal class Produto
    {
        private string conexaoString = "server=localhost;uid=root;pwd='';database=reinodoce;port=3306";
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public DateTime DataValidade { get; set; }

        public void InserirProduto()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                {
                    Console.WriteLine("Digite o nome do Produto: ");
                    Nome = Console.ReadLine();

                    Console.WriteLine("Digite a categoria do Produto: ");
                    Categoria = Console.ReadLine();

                    Console.WriteLine("Digite a descrição do Produto: ");
                    Descricao = Console.ReadLine();

                    Console.WriteLine("Digite o preço do Produto: ");
                    decimal preco;
                    while (!decimal.TryParse(Console.ReadLine(), out preco))
                    {
                        Console.Write("Valor inválido. Digite novamente o preço: ");
                    }
                    Preco = preco;

                    Console.Write("Digite a quantidade em estoque: ");
                    int qtd;
                    while (!int.TryParse(Console.ReadLine(), out qtd))
                    {
                        Console.Write("Valor inválido. Digite novamente a quantidade: ");
                    }
                    QuantidadeEstoque = qtd;

                    Console.Write("Digite a data de validade (AAAA-MM-DD): ");
                    DateTime data;
                    while (!DateTime.TryParse(Console.ReadLine(), out data))
                    {
                        Console.Write("Data inválida. Digite novamente (AAAA-MM-DD): ");
                    }
                    DataValidade = data;

                    conexao.Open();
                    string query = "INSERT INTO produtos (Nome, Categoria, Descricao, Preco, QuantidadeEstoque, DataValidade) " +
                                   "VALUES (@Nome, @Categoria, @Descricao, @Preco, @QuantidadeEstoque, @DataValidade)";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@Nome", Nome);
                    comando.Parameters.AddWithValue("@Categoria", Categoria);
                    comando.Parameters.AddWithValue("@Descricao", Descricao);
                    comando.Parameters.AddWithValue("@Preco", Preco);
                    comando.Parameters.AddWithValue("@QuantidadeEstoque", QuantidadeEstoque);
                    comando.Parameters.AddWithValue("@DataValidade", DataValidade);

                    int linhasAfetadas = comando.ExecuteNonQuery();
                    if (linhasAfetadas > 0)
                    {
                        Console.WriteLine("Produto incluido com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Não foi possível incluir o produto :( ");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir produto: " + ex.Message);
            }
        }



        public void ListarProdutos()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                {
                    conexao.Open();
                    string query = "SELECT * FROM produtos";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    MySqlDataReader leitor = comando.ExecuteReader();
                    Console.WriteLine("Lista de Produtos:");
                    while (leitor.Read())
                    {
                        Console.WriteLine($"ID: {leitor["Id"]}, Nome: {leitor["Nome"]}, Categoria: {leitor["Categoria"]}, " +
                                          $"Descrição: {leitor["Descricao"]}, Preço: {leitor["Preco"]}, " +
                                          $"Quantidade em Estoque: {leitor["QuantidadeEstoque"]}, Data de Validade: {leitor["DataValidade"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar produtos: " + ex.Message);
            }
        }


        public void EditarProduto()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                { 

                    ListarProdutos();
                    Console.Write("Digite o ID do produto que deseja editar: ");
                    int id;
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.Write("ID inválido. Digite novamente o ID do produto: ");
                    }
                    Id = id;

                    Console.WriteLine("Digite o novo nome do Produto: ");
                    Nome = Console.ReadLine();

                    Console.WriteLine("Digite a nova categoria do Produto: ");
                    Categoria = Console.ReadLine();

                    Console.WriteLine("Digite a nova descrição do Produto: ");
                    Descricao = Console.ReadLine();

                    Console.WriteLine("Digite o novo preço do Produto: ");
                    decimal preco;
                    while (!decimal.TryParse(Console.ReadLine(), out preco))
                    {
                        Console.Write("Valor inválido. Digite novamente o preço: ");
                    }
                    Preco = preco;

                    Console.Write("Digite a nova quantidade em estoque: ");
                    int qtd;
                    while (!int.TryParse(Console.ReadLine(), out qtd))
                    {
                        Console.Write("Valor inválido. Digite novamente a quantidade: ");
                    }
                    QuantidadeEstoque = qtd;

                    Console.Write("Digite a nova data de validade (AAAA-MM-DD): ");
                    DateTime data;
                    while (!DateTime.TryParse(Console.ReadLine(), out data))
                    {
                        Console.Write("Data inválida. Digite novamente (AAAA-MM-DD): ");
                    }
                    DataValidade = data;


                    conexao.Open();
                    string query = "UPDATE produtos SET Nome=@Nome, Categoria=@Categoria, Descricao=@Descricao, " +
                                   "Preco=@Preco, QuantidadeEstoque=@QuantidadeEstoque, DataValidade=@DataValidade " +
                                   "WHERE Id=@Id";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@Nome", Nome);
                    comando.Parameters.AddWithValue("@Categoria", Categoria);
                    comando.Parameters.AddWithValue("@Descricao", Descricao);
                    comando.Parameters.AddWithValue("@Preco", Preco);
                    comando.Parameters.AddWithValue("@QuantidadeEstoque", QuantidadeEstoque);
                    comando.Parameters.AddWithValue("@DataValidade", DataValidade);
                    comando.Parameters.AddWithValue("@Id", Id);

                    int linhasAfetadas = comando.ExecuteNonQuery();
                    if (linhasAfetadas > 0)
                    {
                        Console.WriteLine("Produto atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Nenhum produto encontrado com o ID fornecido.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao editar produto: " + ex.Message);

            }
        }

        public void ExcluirProduto()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                {
                    ListarProdutos();
                    Console.Write("Digite o ID do produto que deseja excluir: ");
                    int id;
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.Write("ID inválido. Digite novamente o ID do produto: ");
                    }
                    Id = id;

                    conexao.Open();
                    string query = "DELETE FROM produtos WHERE Id=@Id";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@Id", Id);

                    int linhasAfetadas = comando.ExecuteNonQuery();
                    if (linhasAfetadas > 0)
                    {
                        Console.WriteLine("Produto excluído com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Nenhum produto encontrado com o ID fornecido.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir produto: " + ex.Message);
            }
        }

        public void PesquisarProduto()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                {
                    Console.Write("Digite o nome do produto que deseja pesquisar: ");
                    string nomePesquisa = Console.ReadLine();
                    conexao.Open();
                    string query = "SELECT * FROM produtos WHERE Nome LIKE @Nome";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@Nome", "%" + nomePesquisa + "%");
                    MySqlDataReader leitor = comando.ExecuteReader();
                    Console.WriteLine("Resultados da pesquisa:");
                    while (leitor.Read())
                    {
                        Console.WriteLine($"ID: {leitor["Id"]}, Nome: {leitor["Nome"]}, Categoria: {leitor["Categoria"]}, " +
                                          $"Descrição: {leitor["Descricao"]}, Preço: {leitor["Preco"]}, " +
                                          $"Quantidade em Estoque: {leitor["QuantidadeEstoque"]}, Data de Validade: {leitor["DataValidade"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao pesquisar produto: " + ex.Message);
            }
        }
    }
}
