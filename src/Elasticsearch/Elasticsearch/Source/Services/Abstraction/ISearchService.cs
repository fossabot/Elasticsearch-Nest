using System;
using Nest;

namespace Elasticsearch.Source.Services.Abstraction
{
    public interface ISearchService
    {
        ISearchResponse<TObject> GetBySelector<TObject>(string indexName,
            Func<SearchDescriptor<TObject>, ISearchRequest<TObject>> selector)
            where TObject : class;
    }
}
