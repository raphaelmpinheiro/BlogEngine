using System.Collections.Generic;
using System.Linq;
using Blog.Core.Framework;
using Blog.Core.Infraestrutura;
using MongoDB.Bson;

namespace Blog.Core.Domain.Repositorios
{
    public class BlogImpl : IBlog
    {
        private readonly BlogDao _blogDao;

        public BlogImpl()
        {
            _blogDao = new BlogDao();
        }

        public void Postar(Post post)
        {
            _blogDao.InsertOne(post);
        }

        public IEnumerable<Post> GetAllPost()
        {
            return _blogDao.Find(new BsonDocument());
        }

        public IEnumerable<Post> GetAllPost(QuerySpec querySpec)
        {
            return _blogDao.Find(querySpec.GetFilter());
        }

        public PaginatedList<Post> GetAllPost(QuerySpec querySpec, int pagina, int tamanho)
        {
            return new PaginatedList<Post>(_blogDao.Find(querySpec.GetFilter()).AsQueryable(), pagina, tamanho);            
        }
    }
}