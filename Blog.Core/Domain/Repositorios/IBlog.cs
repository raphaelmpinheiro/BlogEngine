using System.Collections.Generic;
using Blog.Core.Framework;

namespace Blog.Core.Domain.Repositorios
{
    public interface IBlog
    {
        void Postar(Post post);
        IEnumerable<Post> GetAllPost();
        IEnumerable<Post> GetAllPost(QuerySpec querySpec);
        PaginatedList<Post> GetAllPost(QuerySpec querySpec, int pagina, int tamanho);        
    }
}