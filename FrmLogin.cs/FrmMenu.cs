using FrmLogin.cs;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace SistemaReinoDoce
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        // Evento ao fechar o formulário pelo "X" lá em cima
        private void FrmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Garante que o programa fecha total
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FrmClientes formClientes = new FrmClientes();
            formClientes.Show();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            // FrmProdutos tela = new FrmProdutos();
            // tela.ShowDialog();
            MessageBox.Show("A tela de Produtos será criada em breve!");
        }

        private void btnVendas_Click(object sender, EventArgs e)
        {
            FrmVendas frmVendas = new FrmVendas();
            frmVendas.Show();
        }

        private void btnProdutos_Click_1(object sender, EventArgs e)
        {
            FrmProduto frmProduto = new FrmProduto();
            frmProduto.Show();
        }
    }
}