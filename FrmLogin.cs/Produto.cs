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
          
            conexaoString = ConexaoBD.StringConexao;
        }

       
        public int id_prod { get; set; }
        public string nome_prod { get; set; }
        public string categoria_prod { get; set; }
        public string descricao_prod { get; set; }
        public decimal preco_venda { get; set; }
        public string unidade { get; set; }

        public bool InserirProduto()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
            
                    string sql = "INSERT INTO produto (nome_prod, categoria_prod, descricao_prod, preco_venda, unidade) VALUES (@Nome, @Categoria, @Descricao, @PrecoVenda, @Unidade)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                    
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
                    throw; // Manda de volta igual o outro arquivo lá
                }
            }
        }

        
        public DataTable ListarProdutos()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    // Seleciona as colunas para exibir no grid. grid parece formula 1, rs
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