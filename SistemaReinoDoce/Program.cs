using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SistemaReinoDoce
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcao;
            do
            {
                Console.WriteLine("Bem-vindo ao Sistema de Gerenciamento do Reino Doce!");
                Console.WriteLine("MENU PRINCIPAL");
                Console.WriteLine("1. Gerenciar Clientes");
                Console.WriteLine("2. Gerenciar Produtos");
                Console.WriteLine("3. Gerenciar Vendas");
                Console.WriteLine("4. Gerenciar Fornecedores");
                Console.WriteLine("0. Sair");

                Console.Write("Selecione uma opção: ");
                
                while (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.Write("Opção inválida. Digite novamente: ");
                }
            } while (opcao != 0);
         }
    }
}
