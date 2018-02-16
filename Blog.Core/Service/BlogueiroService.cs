using Blog.Core.Domain;
using Blog.Core.Domain.Repositorios;

namespace Blog.Core.Service
{
    public class BlogueiroService
    {
        private readonly ITodosOsBlogueiros _todosOsBlogueiros;

        public BlogueiroService(ITodosOsBlogueiros todosOsBlogueiros)
        {
            _todosOsBlogueiros = todosOsBlogueiros;
        }

        public void GerarUsuarioPadrao()
        {
            var admin = _todosOsBlogueiros.ComEmail("admin@admin.com");
            if (admin == null)
            {
                var autenticacao = new Autenticacao("admin@admin.com", "123");
                var blogueiro = new Blogueiro("admin", autenticacao);
                _todosOsBlogueiros.Adicionar(blogueiro);
            }
        }
    }
}