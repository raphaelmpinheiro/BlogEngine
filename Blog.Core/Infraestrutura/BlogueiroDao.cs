using Blog.Core.Domain;
using MongoDB.Bson;

namespace Blog.Core.Infraestrutura
{
    public class BlogueiroDao : Dao<Blogueiro>
    {
        public BlogueiroDao()
            : base("blogueiro")
        {
        }

        protected override Blogueiro MapTo(BsonDocument document)
        {
            var id = document["_id"].AsObjectId.ToString();
            var nome = document["nome"].AsString;
            var email = document["email"].AsString;
            var senha = document["senha"].AsString;
            var autenticacao = new Autenticacao(email, senha);
            return new Blogueiro(nome, autenticacao)
            {
                Id = id
            };
        }

        protected override BsonDocument MapTo(Blogueiro obj)
        {
            return new BsonDocument()
            {
                {"nome", obj.Nome},
                {"email", obj.Autenticacao.Email},
                {"senha", obj.Autenticacao.Senha}
            };
        }
    }
}