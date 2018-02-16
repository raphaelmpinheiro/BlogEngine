using Blog.Core.Domain;
using Blog.Core.Domain.Repositorios;
using Blog.View.Controllers.Base;
using Blog.View.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.View.Controllers
{
    [Route("api/[controller]")]
    public class BlogueiroController : SecureController
    {
        private readonly ITodosOsBlogueiros _todosOsBlogueiros;

        public BlogueiroController(ITodosOsBlogueiros todosOsBlogueiros)
        {
            _todosOsBlogueiros = todosOsBlogueiros;
        }

        [HttpPost]
        public IActionResult Post([FromBody] BlogueiroModel model)
        {
            var autenticaco = new Autenticacao(model.Email, model.Senha);
            var blogueiro = new Blogueiro(model.Nome, autenticaco);
            _todosOsBlogueiros.Adicionar(blogueiro);
            return Ok(new { Id = blogueiro.Id});
        }
        
    }
}