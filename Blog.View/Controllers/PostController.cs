using System.Linq;
using Blog.Core.Domain;
using Blog.Core.Domain.Repositorios;
using Blog.Core.Framework;
using Blog.View.Controllers.Base;
using Blog.View.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.View.Controllers
{
    [Route("api/[controller]")]
    public class PostController : SecureController
    {
        private readonly IBlog _blog;
        private readonly ITodosOsBlogueiros _todosOsBlogueiros;

        public PostController(IBlog blog, ITodosOsBlogueiros todosOsBlogueiros)
        {
            _blog = blog;
            _todosOsBlogueiros = todosOsBlogueiros;
        }

        [HttpGet]
        public PaginatedList<PostModel> Get([FromQuery] PostRequest model)
        {
            var queryString = HttpContext.Request.QueryString.ToString();
            var querySpec = new QuerySpec(queryString);
            var result = _blog.GetAllPost(querySpec, model.Page, model.Size);
            return new PaginatedList<PostModel>(result.List.Select(PostModel.Build).ToList(), result.PageSize,
                result.PageIndex,
                result.TotalCount);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] PostModel model)
        {
            var blogueiro = _todosOsBlogueiros.ComEmail(UsuarioLogado());
            var post = new Post(model.Titulo, model.Conteudo, blogueiro);
            foreach (var etiqueta in model.Etiquetas)
            {
                post.Etiquetar(etiqueta);
            }

            _blog.Postar(post);

            return Ok(new {Id = post.Id});
        }
    }
}