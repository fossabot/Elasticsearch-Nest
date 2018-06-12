using Elasticsearch.Source.Core.Abstraction;
using Elasticsearch.Source.Services.Abstraction;
using Nest;

namespace Elasticsearch.Source.Services.Implementation
{
    public class IndexService : BaseService, IIndexService
    {
        public IndexService(IElastic elastic)
            : base(elastic) { }

        public ICreateIndexResponse Create<T>(string indexName) where T : class
            => Elastic.CreateIndex<T>(indexName);

        public IDeleteIndexResponse Delete(string indexName)
            => Elastic.DeleteIndex(indexName);

        public bool IndexExists(string indexName)
            => Elastic.IndexExists(indexName);
    }
}
