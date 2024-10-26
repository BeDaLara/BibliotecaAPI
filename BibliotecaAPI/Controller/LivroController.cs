using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivroRepository _livroRepository;

        public LivroController(LivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }



        // POST: api/<LivroController>
        [HttpPost("Cadastrar Novo Livro")]
        [SwaggerOperation(
            Summary = "Cadastrar um novo Livro",
            Description = "Este endpoint é responsavel por cadastrar um novo livro no banco")]
        public async Task<IActionResult> CadastroLivro([FromBody] Livro livro)
        {
          
            await _livroRepository.CadastrarLivro(livro);

            return Ok("Livro cadastrado com sucesso");
        }

        // GET api/<LivroController>/5
        [HttpGet("Listar Livros")]
        [SwaggerOperation(Summary = "Listar todos os livros", Description = "Este endpoint retorna um listagem de livros cadastrados.")]
        public async Task<IActionResult> Listar([FromQuery] bool? Disponivel = true)
        {
            var dados = await _livroRepository.ListarTodosLivros(Disponivel);

            if (dados == null)
            {
                return Ok("Não existe livros cadastrados");
            }

            return Ok(dados);

        }

        // PUT api/<LivroController>/5
        [HttpPut("Atualizar")]
        [SwaggerOperation(
             Summary = "Atualizar os dados de um Livro filtrando pelo ID.",
             Description = "Este endpoint é responsavel por atualizar os dados de um livro no banco")]
        public async Task<IActionResult> AtualizarLivro(int id, [FromBody] Livro dados)
        {
            dados.Id = id;
            await _livroRepository.AtualizarLivro(dados);
            return Ok();
        }

        // POST: api/<LivroController>
        [HttpPost("Cadastrar Novo Usuario")]
        [SwaggerOperation(
            Summary = "Cadastrar um novo Usuario",
            Description = "Este endpoint é responsavel por cadastrar um novo livro no banco")]
        public async Task<IActionResult> CadastroUsuario([FromBody] Usuario usuario)
        {

            await _livroRepository.CadastrarUsuario(usuario);

            return Ok("Usuario cadastrado com sucesso");
        }
                // DELETE api/<LivroController>/5
        [HttpDelete("{id}")]
        [SwaggerOperation(
             Summary = "Deletar um Livro filtrando pelo ID.",
             Description = "Este endpoint é responsavel por deletar um livro no banco")]
        public async Task<IActionResult> Delete(int id)
        {
           int disponibilidade = await _livroRepository.ListarLivrosDisponiveisDB(id);
            if(disponibilidade > 0)
            {
                _livroRepository.DeletarLivroPorId(id);
                return Ok("Livro Excluido com sucesso");
            }

            return BadRequest("Esse livro não pode ser excluido pois esta emprestado");
        }

        // POST: api/<LivroController>
        [HttpPost("Fazer um emprestimo")]
        [SwaggerOperation(
            Summary = "Fazer um emprestimo na biblioteca",
            Description = "Este endpoint é responsavel por emprestrar um livro ao usuario")]
        public async Task<IActionResult> Emprestimo([FromBody] Emprestimo emprestimo)
        {

            int dispoemprestimo = await _livroRepository.VerificarEmprestimo(emprestimo.LivroId);
            if (dispoemprestimo > 0)
            {
                emprestimo.DataDevolucao = emprestimo.DataEmprestimo.AddDays(14);
                await _livroRepository.FazerEmprestimoPorId(emprestimo);
                return Ok("Livro Emprestado com sucesso");
            }

            return BadRequest("Esse livro não pode ser emprestado pois ja esta emprestado");
        }

        // GET api/<PessoaController>/5
        [HttpGet("detalhes nome")]
        [SwaggerOperation(
            Summary = "Obtém dados de uma pessoa pelo Nome",
            Description = "Este endpoint retorna todos os dados de uma pessoa cadastrada filtrando pelo Nome.")]
        public async Task<IActionResult> BuscaNome(string nome)
        {
            var usuarionome = await _livroRepository.BuscarPorNome(nome);

            if (usuarionome == null)
            {
                return NotFound("Não existe pessoa cadastrada com o nome informado");
            }
            return Ok(usuarionome);
        }

        // GET api/<PessoaController>/5
        [HttpGet("detalhes email")]
        [SwaggerOperation(
            Summary = "Obtém dados de uma pessoa pelo Email",
            Description = "Este endpoint retorna todos os dados de uma pessoa cadastrada filtrando pelo Email.")]
        public async Task<IActionResult> BuscaEmail(string email)
        {
            var usuarioemail = await _livroRepository.BuscarPorEmail(email);

            if (usuarioemail == null)
            {
                return NotFound("Não existe pessoa cadastrada com o email informado");
            }
            return Ok(usuarioemail);
        }

        // GET api/<PessoaController>/5
        [HttpGet("detalhes genero")]
        [SwaggerOperation(
            Summary = "Obtém dados de um livro pelo Genero",
            Description = "Este endpoint retorna todos os dados de um livro cadastrado filtrando pelo Genero.")]
        public async Task<IActionResult> BuscaGenero(string genero)
        {
            var livrogenero = await _livroRepository.BuscarPorGenero(genero);

            if (livrogenero == null)
            {
                return NotFound("Não existe livro cadastrado com o genero informado");
            }
            return Ok(livrogenero);
        }

        // GET api/<PessoaController>/5
        [HttpGet("detalhes autor")]
        [SwaggerOperation(
            Summary = "Obtém dados de um livro pelo autor",
            Description = "Este endpoint retorna todos os dados de um livro cadastrado filtrando pelo autor.")]
        public async Task<IActionResult> BuscaAutor(string autor)
        {
            var livroautor = await _livroRepository.BuscarPorAutor(autor);

            if (livroautor == null)
            {
                return NotFound("Não existe livro cadastrado com o autor informado");
            }
            return Ok(livroautor);
        }

        // GET api/<PessoaController>/5
        [HttpGet("detalhes ano")]
        [SwaggerOperation(
            Summary = "Obtém dados de um livro pelo ano publicado",
            Description = "Este endpoint retorna todos os dados de um livro cadastrado filtrando pelo Ano.")]
        public async Task<IActionResult> BuscaAno(string ano)
        {
            var livroano = await _livroRepository.BuscarPorAno(ano);

            if (livroano == null)
            {
                return NotFound("Não existe livro cadastrado com o ano informado");
            }
            return Ok(livroano);
        }

    }
}
