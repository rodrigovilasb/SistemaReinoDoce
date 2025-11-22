using System;
using SistemaReinoDoce;

namespace SistemaReinoDoce // Deve ser o namespace do seu projeto Forms
{
    // A classe deve ser pública e estática para ser acessada de qualquer lugar
    public static class ConexaoBD
    {
        // Certifique-se de colocar sua string de conexão real aqui!
        public static string StringConexao = "server=localhost;database=reinodoce;uid=root;pwd='';";
    }
}