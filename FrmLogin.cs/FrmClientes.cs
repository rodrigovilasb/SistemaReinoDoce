using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.Data;
namespace SistemaReinoDoce
{
    public partial class FrmClientes : Form
    {
        // Instancia a classe Cliente
        Cliente cliente = new Cliente();

        public FrmClientes()
        {
            InitializeComponent();
        }

        // Método auxiliar para atualizar o Grid
        private void Listar()
        {
            try
            {
                dgvClientes.DataSource = cliente.ListarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar dados: " + ex.Message);
            }
        }

        // Método para limpar os campos da tela
        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtCpfCnpj.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            txtNome.Focus();
        }


        // A partir de agora, serão os eventos gerados ao clicar na tela (Botão Salvar e carregar a tela, estamos no progresso)

        // 1. Ao abrir a tela, carrega a lista
        private void FrmClientes_Load(object sender, EventArgs e)
        {
            dgvClientes.ReadOnly = true;
            Listar();
        }

        // 2. Botão SALVAR
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtCpfCnpj.Text == "")
            {
                MessageBox.Show("Nome e CPF/CNPJ são obrigatórios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // A variável 'cliente' agora existe :)
            cliente.nome_cli = txtNome.Text;
            cliente.cpf_cnpj_cli = txtCpfCnpj.Text;
            cliente.email_cli = txtEmail.Text;
            cliente.telefone_cli = txtTelefone.Text;

            if (cliente.InserirCliente()) // Assume que InserirCliente() está no Cliente.cs
            {
                MessageBox.Show("Cliente cadastrado com sucesso!");
                Listar();       
                LimparCampos();  
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar cliente. Verifique o CPF/CNPJ.");
            }
        }

       

      
        private void lblId_Click(object sender, EventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTelefone_Click(object sender, EventArgs e)
        {

        }
    }
}