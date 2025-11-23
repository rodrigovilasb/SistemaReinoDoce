using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SistemaReinoDoce
{
    internal class Cliente
    {
        private string conexaoString; 

        public Cliente()
        {
         
            conexaoString = ConexaoBD.StringConexao;
        }

        
        public int id_cli { get; set; }
        public string nome_cli { get; set; }
        public string cpf_cnpj_cli { get; set; }
        public string telefone_cli { get; set; }
        public string email_cli { get; set; }
        public int? id_end { get; set; } 

       
        public bool InserirCliente()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    string sql = "INSERT INTO cliente (nome_cli, cpf_cnpj_cli, telefone_cli, email_cli, id_end) VALUES (@Nome, @CPF, @Telefone, @Email, @IdEnd)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome_cli);
                        cmd.Parameters.AddWithValue("@CPF", cpf_cnpj_cli);
                        cmd.Parameters.AddWithValue("@Telefone", telefone_cli);
                        cmd.Parameters.AddWithValue("@Email", email_cli);

                       //Faz com que o id_end seja enviado como DBNull se for nulo
                        cmd.Parameters.AddWithValue("@IdEnd", id_end.HasValue ? (object)id_end.Value : DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    //Manda de volta para o formulário capturar o erro real
                    throw;
                }
            }
        }
        public DataTable ListarClientes()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    string sql = "SELECT id_cli, nome_cli, cpf_cnpj_cli, telefone_cli, email_cli FROM cliente"; // Seleciona campos principais
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexao))
                    {
                        DataTable tabela = new DataTable();
                        adapter.Fill(tabela);
                        return tabela;
                    }
                }
                catch (Exception ex) { throw; }
            }
        }

        public bool EditarCliente()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    string sql = "UPDATE cliente SET nome_cli = @Nome, cpf_cnpj_cli = @CPF, telefone_cli = @Telefone, email_cli = @Email, id_end = @IdEnd WHERE id_cli = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome_cli);
                        cmd.Parameters.AddWithValue("@CPF", cpf_cnpj_cli);
                        cmd.Parameters.AddWithValue("@Telefone", telefone_cli);
                        cmd.Parameters.AddWithValue("@Email", email_cli);
                        cmd.Parameters.AddWithValue("@IdEnd", id_end.HasValue ? (object)id_end.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Id", id_cli);
                        // Executa e armazena o número de linhas removidas
                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        // Retorna true apenas se 1 linha foi removida
                        return linhasAfetadas == 1;
                        //Desisto de fazer dar certo, já estou a mais de 12h...
                    }
                }
                catch (Exception) { throw; } //Mesma coisa lá de cima
            }
        }


        public bool RemoverCliente()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    string sql = "DELETE FROM cliente WHERE id_cli = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Id", id_cli);
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                     
                        return linhasAfetadas == 1;
                        //Já entendi que nao quer remover :(
                    }
                }
                catch (Exception ex) { throw; }
            }
        }
    }
}