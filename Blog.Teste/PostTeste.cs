using System;
using System.Linq;
using Blog.Core.Domain;
using Blog.Core.Domain.Repositorios;
using Blog.Core.Framework;
using Xunit;

namespace Blog.Teste
{
    public class PostTeste
    {
        [Fact]
        public void CriarUmNovoPostTest()
        {
            var autenticacao = new Autenticacao("admin@blog.com", "123");
            var blogueiro = new Blogueiro("Blogueiro 1", autenticacao);
            var post = new Post("Primeiro post", "Este é um primeiro post de testes", blogueiro);
            post.Comentar(new Comentario("First"));
            post.Etiquetar("Etiqueta1");
            post.Etiquetar("Etiqueta2");

            var todasAsEtiquetas = post.TodasAsEtiquetas();
            Assert.True(todasAsEtiquetas.Count() == 2);

            Assert.Equal(post.Postagem.Date, DateTime.Now.Date);
        }      
    }
}