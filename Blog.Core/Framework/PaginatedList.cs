using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Framework
{
    public class PaginatedList<T>
    {
        private List<T> _list;
        public List<T> List
        {
            get { return _list ?? (_list = new List<T>()); }
        }

        private int _pageIndex;
        public int PageIndex { get { return _pageIndex + 1; } private set { _pageIndex = value - 1; } }
        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList()
        {            
        }

        public PaginatedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            

            if (TotalCount > 0)
                List.AddRange(source.Skip(_pageIndex * PageSize).Take(PageSize));
        }

        public PaginatedList(List<T> listaJaPaginada, int pageSize, int pageIndex, int totalCount)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            if (TotalPages > 0)
            {
                if (PageIndex > TotalPages)
                    throw new PaginaAcimaException(PageIndex, TotalPages);
            }
            else
            {
                PageIndex = 1;
            }

            _list = listaJaPaginada;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }

    public class PaginaAcimaException : ArgumentOutOfRangeException
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public PaginaAcimaException(int pageIndex, int totalPages)
            : base("pageIndex", @"Índice da página solicitada é maior que o total de páginas.")
        {
            PageIndex = pageIndex;
            TotalPages = totalPages;
        }
    }
}