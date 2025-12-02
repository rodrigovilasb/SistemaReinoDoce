using MySql.Data.MySqlClient;
using System;
using System.Data;
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

           //Validação básica para não ter nenhum campo vazio
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Por favor, preencha usuário e senha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Chama o método de Login no Banco de Dados
            if (VerificarLogin(usuario, senha))
            {
                MessageBox.Show("Bem-vindo ao Reino Doce!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // NAVEGAÇÃO CORRETA APÓS O LOGIN
                this.Hide(); // o hide esconde o formulário de login, porque como o jefão disse, se fechar ele, fecha o programa todo
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

       //VERIFICAÇÃO DE LOGIN NO BANCO DE DADOS ---

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
                     
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt); // Preenche o DataTable com o resultado da consulta
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

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}