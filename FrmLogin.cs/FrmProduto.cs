using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaReinoDoce
{
    public partial class FrmProduto : Form
    {
    
        Produto produto = new Produto();

        public FrmProduto()
        {
            InitializeComponent();
        }

        

        private void Listar()
        {
            try
            {
                dgvProdutos.ReadOnly = true;
                dgvProdutos.DataSource = produto.ListarProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar produtos: " + ex.Message, "Erro de Listagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtCategoria.Clear();
            txtDescricao.Clear();
            txtPrecoVenda.Clear();
            txtUnidade.Clear();
            txtNome.Focus();
        }

      

        private void FrmProduto_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validação de campos obrigatórios
            if (txtNome.Text == "" || txtPrecoVenda.Text == "")
            {
                MessageBox.Show("Preencha o Nome e o Preço de Venda.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                produto.nome_prod = txtNome.Text;
                produto.categoria_prod = txtCategoria.Text;
                produto.descricao_prod = txtDescricao.Text;

                // Conversão de decimal: Usaremos o de gente 'normal', que é a vírgula. (Todo respeito aos EUA)
                if (decimal.TryParse(txtPrecoVenda.Text, out decimal preco))
                {
                    produto.preco_venda = preco;
                }
                else
                {
                    MessageBox.Show("Valor do Preço de Venda inválido. Use um formato numérico válido (ex: 15,50).", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                produto.unidade = txtUnidade.Text;

                if (produto.InserirProduto())
                {
                    MessageBox.Show("Produto cadastrado com sucesso!");
                    Listar();
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO CRÍTICO NO SQL do InserirProduto: " + ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow linha = dgvProdutos.Rows[e.RowIndex];

                txtId.Text = linha.Cells["id_prod"].Value.ToString();
                txtNome.Text = linha.Cells["nome_prod"].Value.ToString();
                txtCategoria.Text = linha.Cells["categoria_prod"].Value.ToString();
                txtPrecoVenda.Text = linha.Cells["preco_venda"].Value.ToString();
                txtUnidade.Text = linha.Cells["unidade"].Value.ToString();

                // A Descrição (descricao_prod) não está no SELECT do Listar, então não dá para preencher aqui.
            }
        }

        private void lblId_Click(object sender, EventArgs e)
        {

        }

        private void FrmProduto_Load_1(object sender, EventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}