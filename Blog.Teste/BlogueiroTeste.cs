using Blog.Core.Domain;
using Xunit;

namespace Blog.Teste
{
    public class BlogueiroTeste
    {
        [Fact]
        public void CriarUmBlogueiroValidoTeste()
        {
            var autenticacao = new Autenticacao("admin@blog.com", "123");
            var blogueiro = new Blogueiro("Blogueiro 1", autenticacao);
        }

        [Fact]
        public void CriarUmBlogueiroComEmailInvalido()
        {
            var autenticacao = new Autenticacao("blog@blog.", "123");
            var blogueiro = new Blogueiro("Blogueiro 1", autenticacao);
        }

        [Fact]
        public void VerificarSenhaInvalidaTeste()
        {
            var autenticacao = new Autenticacao("blog@blog.com", "123");
            var autenticado = autenticacao.Autenticado("12321321");
            Assert.False(autenticado);
        }
        
        [Fact]
        public void VerificarSenhaValidaTeste()
        {
            var autenticacao = new Autenticacao("blog@blog.com", "123");
            var autenticado = autenticacao.Autenticado("123");
            Assert.True(autenticado);
        }
    }
}