using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class Conexao
    {
        public SqlCommand Conectar()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=T:\Documentos\NossoEstoque.mdf;Integrated Security=True;Connect Timeout=30";
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            return command;

            
        }
    }
}
