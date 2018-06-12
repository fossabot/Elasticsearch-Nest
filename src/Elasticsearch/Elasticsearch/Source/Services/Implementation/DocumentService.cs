using Elasticsearch.Source.Core.Abstraction;
using Elasticsearch.Source.Services.Abstraction;
using Nest;

namespace Elasticsearch.Source.Services.Implementation
{
    public class DocumentService : BaseService, IDocumentService
    {
        public DocumentService(IElastic elastic)
            : base(elastic) { }

        public IIndexResponse AddOrUpdate<TObject, TKey>(string indexName, TKey id, TObject @object)
            where TObject : class
            where TKey : Id
            => Elastic.AddOrUpdateDocument(indexName, id, @object);

        public IDeleteResponse DeleteById<TObject, TKey>(string indexName, TKey id)
            where TObject : class
            where TKey : Id
            => Elastic.DeleteDocumentById<TObject, TKey>(indexName, id);

        public TObject GetById<TObject>(string indexName, Id id)
            where TObject : class
            => Elastic.GetById<TObject>(indexName, id);

        public ISearchResponse<TObject> GetAll<TObject>(string indexName)
            where TObject : class
            => Elastic.GetAll<TObject>(indexName);
    }
}
