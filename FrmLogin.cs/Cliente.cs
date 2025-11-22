using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SistemaReinoDoce
{
    internal class Cliente
    {
        private string conexaoString; // Declarado aqui

        // O construtor é executado ao criar uma nova instância (new Cliente())
        // Dentro da classe Cliente.cs, no construtor:
        public Cliente()
        {
            // O acesso agora é direto, sem precisar passar pelo FrmLogin
            conexaoString = ConexaoBD.StringConexao;
        }

        // Propriedades: Refletem a estrutura da sua tabela Cliente no MySQL
        public int id_cli { get; set; }
        public string nome_cli { get; set; }
        public string cpf_cnpj_cli { get; set; }
        public string telefone_cli { get; set; }
        public string email_cli { get; set; }
        public int? id_end { get; set; } // int? permite valores nulos (nullable)

        // --- MÉTODOS DE MANIPULAÇÃO DO BANCO DE DADOS (CRUD) ---

        // INSERIR
        // INSERIR
        public bool InserirCliente()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    // SQL atualizado para incluir cpf_cnpj_cli e id_end
                    string sql = "INSERT INTO cliente (nome_cli, cpf_cnpj_cli, telefone_cli, email_cli, id_end) VALUES (@Nome, @CPF, @Telefone, @Email, @IdEnd)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome_cli);
                        cmd.Parameters.AddWithValue("@CPF", cpf_cnpj_cli);
                        cmd.Parameters.AddWithValue("@Telefone", telefone_cli);
                        cmd.Parameters.AddWithValue("@Email", email_cli);

                        // Garante que id_end seja enviado como DBNull se for nulo
                        cmd.Parameters.AddWithValue("@IdEnd", id_end.HasValue ? (object)id_end.Value : DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    // Agora a exceção é re-lançada para o FrmClientes, onde será exibida.
                    throw;
                }
            }
        }

        // LISTAR (Retorna uma Tabela de Dados)
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

        // EDITAR
        // EDITAR
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

                        // CORRIGIDO: Executa apenas uma vez e armazena o resultado
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        // Retorna true se exatamente 1 linha foi editada
                        return linhasAfetadas == 1;
                    }
                }
                catch (Exception) { throw; } // Mantém o throw para notificar o formulário
            }
        }

        // REMOVER
        // REMOVER
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

                        // Executa e armazena o número de linhas removidas
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        // Retorna true apenas se 1 linha foi removida
                        return linhasAfetadas == 1;
                    }
                }
                catch (Exception ex) { throw; }
            }
        }
    }
}