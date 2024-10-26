using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using BibliotecaAPI.Models;
using MySqlX.XDevAPI;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Repositories
{
    public class LivroRepository
    {
        private readonly string _connectionString;

        public LivroRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection =>
            new MySqlConnection(_connectionString);

        public async Task<int> CadastrarLivro(Livro livro)
        {
            var sql = "INSERT INTO Livros (Titulo,Autor,AnoPublicacao,Genero,Disponivel) " +
                "values (@Titulo,@Autor,@AnoPublicacao,@Genero,@Disponivel)";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql,
                    new
                    {
                        Titulo = livro.Titulo,
                        Autor = livro.Autor,
                        AnoPublicacao = livro.AnoPublicacao,
                        Genero = livro.Genero,
                        Disponivel = livro.Disponivel
                    });
            }
        }

        public async Task<IEnumerable<Livro>> ListarTodosLivros(bool? disponivel)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Livros liv  WHERE liv.Disponivel = @Disponivel";

                return await conn.QueryAsync<Livro>(sql, new { Disponivel = disponivel });

            }
        }

        public async Task<int> AtualizarLivro(Livro dados)
        {
            var sql = "UPDATE Livros set Titulo = @Titulo, Autor = @Autor, AnoPublicacao = @AnoPublicacao,  Genero = @Genero,  Disponivel = @Disponivel WHERE Id = @id";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, dados);
            }
        }

        public async Task<int> ListarLivrosDisponiveisDB(int id)
        {
            using (var conn = Connection)
            {
                var sql = "select count(*) from Livros where  Id = @Id and Disponivel = 1";

                return await conn.QueryFirstOrDefaultAsync<int>(sql, new { Id = id });

            }
        }

        public async Task<int> DeletarLivroPorId(int id)
        {
            var sql = "DELETE FROM Livros WHERE Id = @Id" ;

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<int> CadastrarUsuario(Usuario dados)
        {
            var sql = "INSERT INTO Usuarios (Nome,Email) " +
                "values (@Nome,@Email)";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql,
                    new
                    {
                        Nome = dados.Nome,
                        Email = dados.Email,
                    });
            }
        }

        public async Task<int> VerificarEmprestimo(int id)
        {
            var sql = "select count(*) from Livros WHERE Id = @Id and Disponivel = 1";

            using (var conn = Connection)
                {


                return await conn.QueryFirstOrDefaultAsync<int>(sql, new { Id = id });
            }
        }

      

        public async Task<int> FazerEmprestimoPorId(Emprestimo emprestimo)
        {
            var sql = "insert into Emprestimos (LivroId,UsuarioId,DataEmprestimo,DataDevolucao) values(@LivroId,@UsuarioId,@DataEmprestimo,@Datadevolucao)";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql,
                    new
                    {
                        LivroId = emprestimo.LivroId,
                        UsuarioId = emprestimo.UsuarioId,
                        DataEmprestimo = emprestimo.DataEmprestimo,
                        DataDevolucao = emprestimo.DataDevolucao,
                    });
            }
        }

        public async Task<Usuario> BuscarPorNome(string nome)
        {
            var sql = "SELECT  * FROM Usuarios WHERE Nome = @Nome";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Usuario>(sql, new { Nome = nome });
            }
        }

        public async Task<Usuario> BuscarPorEmail(string email)
        {
            var sql = "SELECT * FROM Usuarios WHERE Email = @Email";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Usuario>(sql, new { Email = email });
            }
        }

        public async Task<Livro> BuscarPorGenero(string genero)
        {
            var sql = "SELECT * FROM Livros WHERE Genero = @Genero";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Livro>(sql, new { Genero = genero });
            }
        }
        public async Task<Livro> BuscarPorAutor(string autor)
        {
            var sql = "SELECT * FROM Livros WHERE Autor = @Autor";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Livro>(sql, new { Autor = autor });
            }
        }
        public async Task<Livro> BuscarPorAno(string ano)
        {
            var sql = "SELECT * FROM Livros WHERE AnoPublicacao = @AnoPublicacao";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Livro>(sql, new { AnoPublicacao = ano });
            }
        }


    }
}

