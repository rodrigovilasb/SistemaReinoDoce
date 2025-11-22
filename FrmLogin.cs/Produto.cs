using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaReinoDoce
{
    internal class Produto
    {
        private string conexaoString;

        public Produto()
        {
            // O acesso à string de conexão deve ser feito aqui
            conexaoString = ConexaoBD.StringConexao;
        }

        // Propriedades: Mapeando os campos da sua tabela Produto
        public int id_prod { get; set; }
        public string nome_prod { get; set; }
        public string categoria_prod { get; set; }
        public string descricao_prod { get; set; }
        public decimal preco_venda { get; set; }
        public string unidade { get; set; }

        // ------------------------------------------------
        // MÉTODOS CRUD DE PRODUTOS
        // ------------------------------------------------

        // 1. INSERIR PRODUTO (CREATE)
        // 1. INSERIR PRODUTO (CREATE)
        public bool InserirProduto()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    // Verifique se os nomes das colunas aqui (nome_prod, categoria_prod, etc.)
                    // são exatamente os mesmos da sua tabela MySQL.
                    string sql = "INSERT INTO produto (nome_prod, categoria_prod, descricao_prod, preco_venda, unidade) VALUES (@Nome, @Categoria, @Descricao, @PrecoVenda, @Unidade)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        // Verifique se os parâmetros @Nome, @Categoria, etc., são os mesmos que os usados no SQL.
                        cmd.Parameters.AddWithValue("@Nome", nome_prod);
                        cmd.Parameters.AddWithValue("@Categoria", categoria_prod);
                        cmd.Parameters.AddWithValue("@Descricao", descricao_prod);
                        cmd.Parameters.AddWithValue("@PrecoVenda", preco_venda);
                        cmd.Parameters.AddWithValue("@Unidade", unidade);

                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception)
                {
                    throw; // Crucial para o FrmProduto capturar o erro de SQL real!
                }
            }
        }

        // 2. LISTAR PRODUTOS (READ)
        public DataTable ListarProdutos()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    // Seleciona as colunas para exibir no grid. Nota: 'descricao_prod' não está incluída para manter a lista simples.
                    string sql = "SELECT id_prod, nome_prod, categoria_prod, preco_venda, unidade FROM produto ORDER BY nome_prod ASC";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexao))
                    {
                        DataTable tabela = new DataTable();
                        adapter.Fill(tabela);
                        return tabela;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}