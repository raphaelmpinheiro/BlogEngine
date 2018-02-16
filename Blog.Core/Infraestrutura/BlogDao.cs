using System.Linq;
using Blog.Core.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Blog.Core.Infraestrutura
{
    public class BlogDao : Dao<Post>
    {
        public BlogDao() : base("post")
        {
        }

        protected override Post MapTo(BsonDocument document)
        {
            var id = document["_id"].AsObjectId.ToString();
            var titulo = document["titulo"].AsString;
            var conteudo = document["conteudo"].AsString;
            var postagem = document["postagem"].ToUniversalTime();
            var blogueiroId = document["blogueiro_id"].AsString;

            var blogueiroDao = new BlogueiroDao();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(blogueiroId));
            var blogueiro = blogueiroDao.FindOne(filter);

            var post = new Post(postagem, titulo, conteudo, blogueiro) {Id = id};

            var etiquetas = document["etiquetas"].AsBsonArray;
            foreach (var etiqueta in etiquetas)
            {
                post.Etiquetar(etiqueta.AsString);
            }

            var comentarios = document["comentarios"].AsBsonArray;

            foreach (var bsonValue in comentarios)
            {
                var value = bsonValue.AsBsonDocument;
                var comentario = new Comentario(value["comento"].AsString, value["momento"].ToUniversalTime());
                post.Comentar(comentario);
            }

            return post;
        }

        protected override BsonDocument MapTo(Post obj)
        {
            var doc = new BsonDocument()
            {
                {"titulo", obj.Titulo},
                {"conteudo", obj.Conteudo},
                {"postagem", obj.Postagem},
                {"blogueiro_id", obj.Blogueiro.Id},
                {"etiquetas", new BsonArray(obj.TodasAsEtiquetas())},
            };

            var comentarios = obj.TodosOsComentarios()
                .Select(comentario => new BsonDocument()
                {
                    {"comento", comentario.Comento},
                    {"momento", comentario.Momento}
                });

            doc.Add("comentarios", new BsonArray(comentarios));
            return doc;
        }
    }
}