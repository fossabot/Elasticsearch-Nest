using System;
using Nest;

namespace Elasticsearch.Source.Services.Abstraction
{
    public interface ISearchService
    {
        ISearchResponse<TObject> GetByQuery<TObject>(string indexName,
            Func<QueryContainerDescriptor<TObject>, QueryContainer> query)
            where TObject : class;
    }
}
