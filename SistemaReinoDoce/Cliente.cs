using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace SistemaReinoDoce
{
    internal class Cliente
    {
        private string conexaoString = "server=localhost;uid=root;pwd='';database=reinodoce;port=3306";

        public int id_cli { get; set; }
        public string nome_cli { get; set; }
        public string email_cli { get; set; }
        public string telefone_cli { get; set; }

        public void InserirCliente()
        {
            Console.Write("Digite o nome do cliente: ");
            nome_cli = Console.ReadLine();

            Console.Write("Digite o email do cliente: ");
            email_cli = Console.ReadLine();

            Console.Write("Digite o telefone do cliente: ");
            telefone_cli = Console.ReadLine();

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    string sql = "INSERT INTO cliente (nome_cli, email_cli, telefone_cli) VALUES (@Nome, @Email, @Telefone)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome_cli);
                        cmd.Parameters.AddWithValue("@Email", email_cli);
                        cmd.Parameters.AddWithValue("@Telefone", telefone_cli);
                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine("Cliente cadastrado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao inserir cliente: " + ex.Message);
                }
            }

        }

        public void ListarClientes()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    string sql = "SELECT * FROM cliente";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("\n--- Lista de Clientes ---");
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["id_cli"]}, Nome: {reader["nome_cli"]}, Email: {reader["email_cli"]}, Telefone: {reader["telefone_cli"]}");
                            }
                            Console.WriteLine("-------------------------\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao listar clientes: " + ex.Message);
                }
            }
        }

        public void EditarCliente()
        {
            ListarClientes();
            Console.Write("Digite o ID do cliente que deseja editar: ");
            id_cli = int.Parse(Console.ReadLine());

            Console.Write("Novo nome: ");
            nome_cli = Console.ReadLine();

            Console.Write("Novo email: ");
            email_cli = Console.ReadLine();

            Console.Write("Novo telefone: ");
            telefone_cli = Console.ReadLine();

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    string sql = "UPDATE cliente SET nome_cli = @Nome, email_cli = @Email, telefone_cli = @Telefone WHERE id_cli = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome_cli);
                        cmd.Parameters.AddWithValue("@Email", email_cli);
                        cmd.Parameters.AddWithValue("@Telefone", telefone_cli);
                        cmd.Parameters.AddWithValue("@Id", id_cli);
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                            Console.WriteLine("Cliente atualizado com sucesso!");
                        else
                            Console.WriteLine("Cliente não encontrado.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao editar cliente: " + ex.Message);
                }
            }
        }

        public void RemoverCliente()
        {
            Console.Write("Digite o ID do cliente que deseja remover: ");
            id_cli = int.Parse(Console.ReadLine());

            try
            {
                using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                {
                    conexao.Open();
                    string sql = "DELETE FROM cliente WHERE id_cli = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Id", id_cli);
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                            Console.WriteLine("Cliente removido com sucesso!");
                        else
                            Console.WriteLine("Cliente não encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao remover cliente: " + ex.Message);
            }
        }

        public void ConsultarCliente()
        {
            Console.Write("Digite o ID do cliente que deseja consultar: ");
            id_cli = int.Parse(Console.ReadLine());

            try
            {
                using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                {
                    conexao.Open();
                    string sql = "SELECT * FROM cliente WHERE id_cli = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Id", id_cli);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["id_cli"]}, Nome: {reader["nome_cli"]}, Email: {reader["email_cli"]}, Telefone: {reader["telefone_cli"]}");
                            }
                            else
                            {
                                Console.WriteLine("Cliente não encontrado.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao consultar cliente: " + ex.Message);
            }
        }
    }
}
