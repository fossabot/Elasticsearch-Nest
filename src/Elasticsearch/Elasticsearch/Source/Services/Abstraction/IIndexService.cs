using Nest;

namespace Elasticsearch.Source.Services.Abstraction
{
    public interface IIndexService
    {
        ICreateIndexResponse Create<T>(string indexName)
            where T : class;

        IDeleteIndexResponse Delete(string indexName);

        bool IndexExists(string indexName);
    }
}
