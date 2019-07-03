using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class EstoqueRepositorio
    {
        //Através do objreto da conexao poderá ter acesso ao sqlcommand
        //de uma forma mais facil
        Conexao connection = new Conexao();

        public int Inserir(Estoque estoque)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"INSERT INTO estoques(nome, valor, quantidade)
VALUES (@NOME, @VALOR, @QUANTIDADE)";
            //Substitui as variáveis do comando acima
            command.Parameters.AddWithValue("@NOME", estoque.Nome);
            command.Parameters.AddWithValue("@VALOR", estoque.Valor);
            command.Parameters.AddWithValue("@QUANTIDADE", estoque.Quantidade);
            //Execura o comando no BD e obtém o id que foi gerado automaticamente pela tabela.
            //ExecuteScalar: executa um comando no BD e obtém uma infomação.
            int id = Convert.ToInt32(command.ExecuteScalar());
            //Fecha a conexao com o BD
            command.Connection.Close();
            //retorna o ID que foi gerado no BD
            return id;
        }

        public List<Estoque> ObterTodos(string busca)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"SELECT * FROM estoques
WHERE nome LIKE @BUSCA";
            busca = $"%{busca}%";
            command.Parameters.AddWithValue("@BUSCA", busca);

            List<Estoque> estoques = new List<Estoque>();
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            command.Connection.Close();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Estoque estoque = new Estoque();

                estoque.Id = Convert.ToInt32(linha["id"]);
                estoque.Nome = linha["nome"].ToString();
                estoque.Quantidade = Convert.ToInt32(linha["quantidade"]);
                estoque.Valor = Convert.ToDecimal(linha["valor"]);

                estoques.Add(estoque);
            }
            return estoques;
        }

        public bool Apagar(int id)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"DELETE FROM estoques WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Estoque ObterPeloId(int id)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"SELECT * FROM estoques WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());

            if(tabela.Rows.Count == 1)
            {
                DataRow linha = tabela.Rows[0];
                Estoque estoque = new Estoque();
                estoque.Id = Convert.ToInt32(linha["id"]);
                estoque.Nome = linha["nome"].ToString();
                estoque.Quantidade = Convert.ToInt32(linha["quantidade"]);
                estoque.Valor = Convert.ToDecimal(linha["valor"]);
                return estoque;
            }
            return null;
        }

        public bool Atualizar(Estoque estoque)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"UPDATE estoques SET
nome = @NOME, quantidade = @QUANTIDADE, valor = @VALOR WHERE id = @ID";
            command.Parameters.AddWithValue("@NOME", estoque.Nome);
            command.Parameters.AddWithValue("@QUANTIDADE", estoque.Quantidade);
            command.Parameters.AddWithValue("@VALOR", estoque.Valor);
            command.Parameters.AddWithValue("@ID", estoque.Id);

            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
