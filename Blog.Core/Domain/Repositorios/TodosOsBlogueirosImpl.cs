using Blog.Core.Infraestrutura;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Blog.Core.Domain.Repositorios
{
    public class TodosOsBlogueirosImpl : ITodosOsBlogueiros
    {
        private readonly BlogueiroDao _blogueiroDao;

        public TodosOsBlogueirosImpl()
        {
            _blogueiroDao = new BlogueiroDao();
        }

        public void Adicionar(Blogueiro blogueiro)
        {
            _blogueiroDao.InsertOne(blogueiro);
        }

        public Blogueiro ComEmail(string email)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("email", email);
            return _blogueiroDao.FindOne(filter);
        }
    }
}