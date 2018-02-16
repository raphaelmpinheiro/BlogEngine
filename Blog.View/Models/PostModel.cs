using System.Collections.Generic;
using System.Linq;
using Blog.Core.Domain;

namespace Blog.View.Models
{
    public class PostModel
    {
        public PostModel()
        {
            ComentarioModel = new List<ComentarioModel>();
        }

        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string[] Etiquetas { get; set; }
        public IList<ComentarioModel> ComentarioModel { get; set; }

        public static PostModel Build(Post post)
        {
            var postModel = new PostModel();
            postModel.Conteudo = post.Conteudo;
            postModel.Titulo = post.Titulo;
            postModel.Id = post.Id;
            foreach (var comentario in post.TodosOsComentarios())
            {
                var comentarioModel = new ComentarioModel();
                comentarioModel.Comento = comentario.Comento;
                comentarioModel.Momento = comentario.Momento;
                postModel.ComentarioModel.Add(comentarioModel);
            }

            postModel.Etiquetas = post.TodasAsEtiquetas().ToArray();            

            return postModel;
        }
    }
}