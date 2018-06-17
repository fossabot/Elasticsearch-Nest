using System;
using Nest;

namespace Elasticsearch.Source.Core.Abstraction
{
    public interface IElastic
    {
        ICreateIndexResponse CreateIndex<T>(string indexName)
            where T : class;

        bool IndexExists(string indexName);

        IIndexResponse AddOrUpdateDocument<TObject, TKey>(string indexName, TKey id, TObject @object)
            where TObject : class
            where TKey : Id;

        IDeleteResponse DeleteDocumentById<TObject, TKey>(string indexName, TKey id)
            where TObject : class
            where TKey : Id;

        IDeleteIndexResponse DeleteIndex(string indexName);

        ISearchResponse<TObject> GetAll<TObject>(string indexName)
            where TObject : class;

        TObject GetById<TObject>(string indexName, Id id)
            where TObject : class;

        ISearchResponse<TObject> Search<TObject>(
            Func<SearchDescriptor<TObject>, ISearchRequest<TObject>> selector)
            where TObject : class;
    }
}
