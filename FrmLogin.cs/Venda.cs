using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SistemaReinoDoce
{
    public class Venda
    {
       
        public class ItemPedido
        {
            public int IdProd { get; set; }
            public int Quantidade { get; set; }
            public decimal PrecoUnit { get; set; }
            public decimal Subtotal { get; set; }
        }

        private string conexaoString;

        public Venda()
        {
            conexaoString = ConexaoBD.StringConexao;
        }

    
        public DataTable BuscarCliente(string termo)
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                   
                    string sql = "SELECT id_cli, nome_cli, cpf_cnpj_cli FROM cliente WHERE id_cli = @Termo OR cpf_cnpj_cli = @Termo";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Termo", termo);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
                catch (Exception) { return null; }
            }
        }

       
        public DataTable BuscarProduto(int id)
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    string sql = "SELECT id_prod, nome_prod, preco_venda FROM produto WHERE id_prod = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
                catch (Exception) { return null; }
            }
        }

       
        public bool FinalizarPedido(int idCliente, List<ItemPedido> listaItens)
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                conexao.Open();

                // Inicia uma TRANSAÇÃO . Se der erro nos itens, cancela o pedido e aí me complica.
                MySqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    // 1. Inserir o Cabeçalho (Pedido_Venda)
                    string sqlPedido = "INSERT INTO Pedido_Venda (id_cli, data_pedido, status_pedido) VALUES (@IdCli, @Data, 'aberto'); SELECT LAST_INSERT_ID();";

                    int idPedidoGerado = 0;

                    using (MySqlCommand cmdPedido = new MySqlCommand(sqlPedido, conexao, transacao))
                    {
                        cmdPedido.Parameters.AddWithValue("@IdCli", idCliente);
                        cmdPedido.Parameters.AddWithValue("@Data", DateTime.Now);

                        // Executa e pega o ID gerado (O 'id_pedv')
                        idPedidoGerado = Convert.ToInt32(cmdPedido.ExecuteScalar());
                    }

                    // 2. Inserir os Itens (Pedido_Venda_Item)
                    foreach (var item in listaItens)
                    {
                        string sqlItem = "INSERT INTO Pedido_Venda_Item (id_pedv, id_prod, quantidade, preco_unit, subtotal) VALUES (@IdPed, @IdProd, @Qtd, @Preco, @Subtotal)";

                        using (MySqlCommand cmdItem = new MySqlCommand(sqlItem, conexao, transacao))
                        {
                            cmdItem.Parameters.AddWithValue("@IdPed", idPedidoGerado);
                            cmdItem.Parameters.AddWithValue("@IdProd", item.IdProd);
                            cmdItem.Parameters.AddWithValue("@Qtd", item.Quantidade);
                            cmdItem.Parameters.AddWithValue("@Preco", item.PrecoUnit);
                            cmdItem.Parameters.AddWithValue("@Subtotal", item.Subtotal);

                            cmdItem.ExecuteNonQuery();
                        }
                    }

                    // Se chegou até aqui, grava tudo no banco
                    transacao.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // Se deu erro, desfaz tudo (não se pode criar pedido pela metade, infelizmente
                    transacao.Rollback();
                    throw ex; 
                }
            }
        }
    }
}