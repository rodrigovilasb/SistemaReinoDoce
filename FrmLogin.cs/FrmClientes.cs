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

    
        // A partir de agora, serão os eventos gerados ao clicar na tela
      
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

        // 3. Botão EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Selecione um cliente na tabela para editar.");
                return;
            }

            cliente.id_cli = int.Parse(txtId.Text);
            cliente.nome_cli = txtNome.Text;
            cliente.cpf_cnpj_cli = txtCpfCnpj.Text;
            cliente.email_cli = txtEmail.Text;
            cliente.telefone_cli = txtTelefone.Text;

            if (cliente.EditarCliente())
            {
                MessageBox.Show("Cliente editado com sucesso!");
                Listar();
                LimparCampos();
            }
            else
            {
                MessageBox.Show("Erro ao editar cliente.");
            }
        }

        // 4. Botão EXCLUIR
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Selecione um cliente na tabela para excluir.");
                return;
            }

            if (MessageBox.Show("Tem certeza que deseja excluir?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                cliente.id_cli = int.Parse(txtId.Text);
                if (cliente.RemoverCliente())
                {
                    MessageBox.Show("Cliente removido!");
                    Listar();
                    LimparCampos();
                }
                else
                {
                    MessageBox.Show("Erro ao remover cliente.");
                }
            }
        }

        // 5. Botão LIMPAR
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        // 6. Clicar no Grid para Preencher os Campos (Evento CellClick)
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                // 1. Pega a linha completa onde o usuário clicou
                DataGridViewRow linha = dgvClientes.Rows[e.RowIndex];

                // 2. Transfere os dados da linha para os TextBox do formulário
                

                txtId.Text = linha.Cells["id_cli"].Value.ToString();
                txtNome.Text = linha.Cells["nome_cli"].Value.ToString();
                txtCpfCnpj.Text = linha.Cells["cpf_cnpj_cli"].Value.ToString();
                txtTelefone.Text = linha.Cells["telefone_cli"].Value.ToString();
                txtEmail.Text = linha.Cells["email_cli"].Value.ToString();

  
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