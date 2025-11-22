using MySql.Data.MySqlClient;
using System;
using System.Data; // Necessário para o DataTable
using System.Windows.Forms;

namespace SistemaReinoDoce
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim(); // Remove espaços em branco
            string senha = txtSenha.Text;

            // 1. Validação simples de campos vazios
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Por favor, preencha usuário e senha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Chama o método de Login no Banco de Dados
            if (VerificarLogin(usuario, senha))
            {
                MessageBox.Show("Bem-vindo ao Reino Doce!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // NAVEGAÇÃO CORRETA APÓS O LOGIN
                this.Hide();
                FrmMenu formMenu = new FrmMenu();
                formMenu.Show();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Clear();
                txtUsuario.Focus();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // --- NOVO MÉTODO: VERIFICAÇÃO DE LOGIN NO BANCO DE DADOS ---

        private bool VerificarLogin(string usuario, string senha)
        {
            // O DataTable irá armazenar o resultado da consulta SQL
            DataTable dt = new DataTable();

            // Usa a string de conexão que está na classe ConexaoBD
            using (MySqlConnection conexao = new MySqlConnection(ConexaoBD.StringConexao))
            {
                try
                {
                    conexao.Open();

                    // Consulta SQL: Busca o usuário e a senha
                    string sql = "SELECT * FROM usuario WHERE login = @Usuario AND senha = @Senha";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        // Parâmetros: Evita SQL Injection e garante que a senha seja checada
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }

                    // Se a consulta retornar pelo menos uma linha, o login foi bem-sucedido
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    // Se houver um erro de conexão (servidor desligado, etc.), avisa o usuário
                    MessageBox.Show("Erro ao tentar conectar ao banco de dados: " + ex.Message, "Erro Crítico de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}