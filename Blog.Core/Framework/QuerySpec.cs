using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Blog.Core.Framework
{
    public class QuerySpec
    {
        private readonly string _queryString;

        public QuerySpec(string queryString)
        {
            _queryString = queryString;
        }

        public virtual FilterDefinition<BsonDocument> GetFilter()
        {
            var queryString = GetQueryString(_queryString);
            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = new BsonDocumentFilterDefinition<BsonDocument>(new BsonDocument());
            foreach (var query in queryString)
            {
                if (query.Key.Equals("page", StringComparison.InvariantCultureIgnoreCase)
                    || query.Key.Equals("size", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }
                
                var values = query.Value.Split(',');
                foreach (var value in values)
                {
                    var criteriaFilter = builder.Eq(query.Key, value);
                    if (filter != null)
                    {
                        filter = filter & criteriaFilter;
                    }
                    else
                    {
                        filter = criteriaFilter;
                    }
                }
            }

            return filter;
        }

        private Dictionary<string, string> GetQueryString(string queryString)
        {
            var queryStringCollection = HttpUtility.ParseQueryString(queryString);
            return queryStringCollection.Keys.Cast<string>()
                .ToDictionary(k => k, v => queryStringCollection[v]);
        }
    }
}