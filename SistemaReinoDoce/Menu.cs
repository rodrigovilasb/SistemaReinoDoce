using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace SistemaReinoDoce
{
    internal class Menu
    {
        int opcao;
        Produto p = new Produto();
        Cliente c = new Cliente();
        bool sair = false;

        public void MenuPrincipal()
        {
            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("=====Bem-vindo ao Sistema de Gerenciamento do Reino Doce!=====");
                Console.WriteLine("MENU PRINCIPAL");
                Console.WriteLine("1. Gerenciar Clientes");
                Console.WriteLine("2. Gerenciar Produtos");
                Console.WriteLine("3. Gerenciar Vendas");
                Console.WriteLine("0. Sair");

                Console.Write("Selecione uma opção: ");

                while (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.Write("Opção inválida. Digite novamente: ");
                }

                switch (opcao)
                {
                    case 1:
                        MenuClientes();
                        break;
                    case 2:
                        MenuProdutos();
                        break;
                    case 3:
                        // Implementar MenuVendas();
                        break;
                    case 0:
                        Environment.Exit(0);
                        Console.WriteLine("Saindo do sistema. Até logo!");
                        break;
                  
                }
            }
        }

        public void MenuProdutos()
        {   bool voltar = false;
            while(!voltar)
            {
                Console.WriteLine("MENU DE PRODUTOS");
                Console.WriteLine("1. Adicionar Produto");
                Console.WriteLine("2. Listar Produtos");
                Console.WriteLine("3. Editar Produto");
                Console.WriteLine("4. Remover Produto");
                Console.WriteLine("5. Consultar Produto");
                Console.WriteLine("6. Voltar ao Menu Principal");
                Console.WriteLine("0. Sair");

                Console.WriteLine("Selecione uma opção: ");
                while (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.Write("Opção inválida. Digite novamente: ");
                }
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        p.InserirProduto();
                        break;
                    case 2:
                        p.ListarProdutos();
                        break;
                    case 3:
                        p.EditarProduto();
                        break;
                    case 4:
                        p.ExcluirProduto();
                        break;
                    case 5:
                        p.PesquisarProduto();
                        break;
                    case 6:
                        voltar = true;
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        public void MenuClientes()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.WriteLine("MENU DE CLIENTES");
                Console.WriteLine("1. Adicionar Cliente");
                Console.WriteLine("2. Listar Clientes");
                Console.WriteLine("3. Editar Cliente");
                Console.WriteLine("4. Remover Cliente");
                Console.WriteLine("5. Consultar Cliente");
                Console.WriteLine("6. Voltar ao Menu Principal");
                Console.WriteLine("0. Sair");
                Console.Write("Selecione uma opção: ");
                while (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.Write("Opção inválida. Digite novamente: ");
                }

                switch (opcao)
                {
                    case 1:
                        c.InserirCliente();
                        break;
                    case 2:
                        c.ListarClientes();
                        break;
                    case 3:
                        c.EditarCliente();
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
    }
}
