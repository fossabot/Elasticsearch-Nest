using System;
using Elasticsearch.Source.Core.Abstraction;
using Elasticsearch.Source.Services.Abstraction;
using Nest;

namespace Elasticsearch.Source.Services.Implementation
{
    public class SearchService : BaseService, ISearchService
    {
        public SearchService(IElastic elastic)
            : base(elastic) { }

        public ISearchResponse<TObject> GetByQuery<TObject>(string indexName,
            Func<QueryContainerDescriptor<TObject>, QueryContainer> query)
            where TObject : class
        {
            ISearchRequest<TObject> Selector(SearchDescriptor<TObject> descriptor)
            {
                return descriptor
                    .Index(indexName)
                    .Type(GetTypeName<TObject>())
                    .Query(query) as ISearchRequest<TObject>;
            }

            return Elastic.Search<TObject>(Selector);
        }

        private static string GetTypeName<T>() where T : class
            => typeof(T).Name.ToLower();
    }
}
