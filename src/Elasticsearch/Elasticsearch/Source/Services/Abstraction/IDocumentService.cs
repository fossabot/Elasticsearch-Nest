using Nest;

namespace Elasticsearch.Source.Services.Abstraction
{
    public interface IDocumentService
    {
        IIndexResponse AddOrUpdate<TObject, TKey>(string indexName, TKey id,TObject @object)
            where TObject : class
            where TKey : Id;

        IDeleteResponse DeleteById<TObject, TKey>(string indexName, TKey id)
            where TObject : class
            where TKey : Id;

        TObject GetById<TObject>(string indexName, Id id)
            where TObject : class;

        ISearchResponse<TObject> GetAll<TObject>(string indexName)
            where TObject : class;
    }
}
