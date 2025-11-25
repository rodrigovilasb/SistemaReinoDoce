using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace SistemaReinoDoce
{
    public partial class FrmVendas : Form
    {
        Venda venda = new Venda();

        // Variável para somar o total geral
        decimal totalGeral = 0;

        public FrmVendas()
        {
            InitializeComponent();
        }

        private void btnBuscarCli_Click(object sender, EventArgs e)
        {
            if (txtIdCliente.Text == "") return;

            // Chama o método da lá da classe Venda
            DataTable dt = venda.BuscarCliente(txtIdCliente.Text);

            if (dt != null && dt.Rows.Count > 0)
            {
                // Preenche o nome do cliente na tela
                txtNomeCliente.Text = dt.Rows[0]["nome_cli"].ToString();

                // Foca no campo de produto para agilizar
                txtIdProduto.Focus();
                //Tô ficando maluc, rs
            }
            else
            {
                MessageBox.Show("Cliente não encontrado!");
                txtNomeCliente.Clear();
            }
        }

        private void txtIdProduto_Leave(object sender, EventArgs e)
        {
            if (txtIdProduto.Text == "") return;

            int idProd;
            if (int.TryParse(txtIdProduto.Text, out idProd))
            {
                DataTable dt = venda.BuscarProduto(idProd);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtNomeProduto.Text = dt.Rows[0]["nome_prod"].ToString();
                    txtPreco.Text = dt.Rows[0]["preco_venda"].ToString();
                    txtQtd.Text = "1"; // Sugere 1 uni
                    txtQtd.Focus();
                }
                else
                {
                    MessageBox.Show("Produto não encontrado!");
                    txtNomeProduto.Clear();
                    txtPreco.Clear();
                }
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            // Validações básicas
            if (txtNomeProduto.Text == "" || txtQtd.Text == "")
            {
                MessageBox.Show("Pesquise um produto e informe a quantidade.");
                return;
            }

            // Pega os valores
            int idProd = int.Parse(txtIdProduto.Text);
            string nomeProd = txtNomeProduto.Text;
            decimal preco = decimal.Parse(txtPreco.Text);
            int qtd = int.Parse(txtQtd.Text);

            // Calcula o Subtotal deste item
            decimal subtotal = preco * qtd;

            
            dgvItens.Rows.Add(idProd, nomeProd, qtd, preco, subtotal);

            // Atualiza o Totalzão lá embaixo
            AtualizarTotal(subtotal);

            // Limpa os campos de produto para o próximo
            txtIdProduto.Clear();
            txtNomeProduto.Clear();
            txtPreco.Clear();
            txtQtd.Clear();
            txtIdProduto.Focus();
        }

        // Método auxiliar para somar o total
        private void AtualizarTotal(decimal valor)
        {
            totalGeral += valor;
            lblTotalFinal.Text = totalGeral.ToString("C2"); // Formata como a Moeda + top (R$)
        }

        
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (dgvItens.Rows.Count == 0 || txtNomeCliente.Text == "")
            {
                MessageBox.Show("Selecione um cliente e adicione itens ao carrinho.");
                return;
            }

            try
            {
                // Cria a lista de itens para enviar para a classe Venda
                List<Venda.ItemPedido> listaItens = new List<Venda.ItemPedido>();

                // 'Varre' o Grid linha por linha
                foreach (DataGridViewRow linha in dgvItens.Rows)
                {
                    Venda.ItemPedido item = new Venda.ItemPedido();
                    item.IdProd = Convert.ToInt32(linha.Cells[0].Value); // Coluna 0: ID
                    // Pula nome (1)
                    item.Quantidade = Convert.ToInt32(linha.Cells[2].Value); // Coluna 2: Qtd
                    item.PrecoUnit = Convert.ToDecimal(linha.Cells[3].Value); // Coluna 3: Preço
                    item.Subtotal = Convert.ToDecimal(linha.Cells[4].Value); // Coluna 4: Subtotal

                    listaItens.Add(item);
                }

                int idCliente = int.Parse(txtIdCliente.Text);

                // Chama o método poderoso da classe Venda que grava tudo, espero que grave né?!
                if (venda.FinalizarPedido(idCliente, listaItens))
                {
                    MessageBox.Show("Venda realizada com sucesso! Pedido Gravado.");

                    // Limpa tudo para a próxima venda
                    dgvItens.Rows.Clear();
                    totalGeral = 0;
                    lblTotalFinal.Text = "R$ 0,00";
                    txtIdCliente.Clear();
                    txtNomeCliente.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao fechar venda: " + ex.Message);
            }
        }
    }
}