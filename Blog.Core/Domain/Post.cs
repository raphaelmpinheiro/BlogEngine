using System;
using System.Collections.Generic;
using System.Linq;
using AssertLibrary;

namespace Blog.Core.Domain
{
    public class Post : Entidade
    {
        private readonly ICollection<Comentario> _comentarios = new List<Comentario>();
        private readonly ICollection<string> _etiquetas = new List<string>();

        public Post(string titulo, string conteudo, Blogueiro blogueiro)
            : this(DateTime.Now, titulo, conteudo, blogueiro)
        {
        }

        public Post(DateTime postagem, string titulo, string conteudo, Blogueiro blogueiro)
        {
            Postagem = postagem;
            Titulo = titulo;
            Conteudo = conteudo;
            Blogueiro = blogueiro;
        }

        private DateTime _postagem;
        private string _titulo;
        private string _conteudo;
        private Blogueiro _blogueiro;

        public DateTime Postagem
        {
            get => _postagem;
            private set
            {
                Assert.IsNotNull(value, "Postagem não pode ser nula.");
                Assert.IsTrue(value > DateTime.MinValue);
                _postagem = value;
            }
        }

        public string Titulo
        {
            get => _titulo;
            private set
            {
                Assert.IsFalse(string.IsNullOrEmpty(value), "Titulo não pode ser vazio ou nulo.");
                _titulo = value;
            }
        }

        public string Conteudo
        {
            get => _conteudo;
            private set
            {
                Assert.IsFalse(string.IsNullOrEmpty(value), "Conteúdo não pode ser vazio ou nulo.");
                _conteudo = value;
            }
        }

        public Blogueiro Blogueiro
        {
            get => _blogueiro;
            private set
            {
                Assert.IsNotNull(value, "Não pode existir um post sem blogueiro.");
                _blogueiro = value;
            }
        }

        public void Comentar(Comentario comentario)
        {
            _comentarios.Add(comentario);
        }

        public IEnumerable<Comentario> TodosOsComentarios()
        {
            return _comentarios.OrderByDescending(x => x.Momento);
        }

        public void Etiquetar(string etiqueta)
        {
            Assert.IsFalse(string.IsNullOrEmpty(etiqueta), "Etiqueta não pode ser vazia ou nula.");
            _etiquetas.Add(etiqueta);
        }

        public IEnumerable<string> TodasAsEtiquetas()
        {
            return _etiquetas;
        }
    }
}