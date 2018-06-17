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

        public ISearchResponse<TObject> GetBySelector<TObject>(string indexName,
            Func<SearchDescriptor<TObject>, ISearchRequest<TObject>> selector)
            where TObject : class
            => Elastic.Search(indexName, selector);
    }
}
