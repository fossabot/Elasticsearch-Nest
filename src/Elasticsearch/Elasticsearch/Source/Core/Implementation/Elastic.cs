using System;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Source.Core.Abstraction;
using Nest;

namespace Elasticsearch.Source.Core.Implementation
{
    public class Elastic : IElastic
    {
        public ElasticClient Client { get; }

        public Elastic(Uri uri = null)
        {
            var settings = new ConnectionSettings(uri)
                .DisableAutomaticProxyDetection()
                .EnableHttpCompression()
                .DisableDirectStreaming()
                .PrettyJson()
                .RequestTimeout(TimeSpan.FromMinutes(2));

            Client = new ElasticClient(settings);
        }

        public ICreateIndexResponse CreateIndex<T>(string indexName) where T : class
            => Client.CreateIndex(indexName, c => c
                .Mappings(m => m.Map<T>(mp => mp.AutoMap()))
            );

        public bool IndexExists(string indexName)
            => Client.IndexExists(indexName).Exists;

        public IIndexResponse AddOrUpdateDocument<TObject, TKey>(string indexName, TKey id, TObject @object)
            where TObject : class
            where TKey : Id
            => Client.Index(@object, i => i
                .Index(indexName)
                .Type(GetTypeName<TObject>())
                .Id(id)
                .Refresh(Refresh.True)
            );

        public IDeleteResponse DeleteDocumentById<TObject, TKey>(string indexName, TKey id)
            where TObject : class
            where TKey : Id
            => Client.Delete<TObject>(id, d => d
                .Index(indexName)
                .Type(GetTypeName<TObject>())
            );

        public IDeleteIndexResponse DeleteIndex(string indexName)
            => Client.DeleteIndex(indexName);

        public ISearchResponse<TObject> GetAll<TObject>(string indexName) where TObject : class
            => Client.Search<TObject>(s => s
                .Index(indexName)
                .Type(GetTypeName<TObject>())
                .MatchAll()
            );

        public TObject GetById<TObject>(string indexName, Id id)
            where TObject : class
            => Client.Search<TObject>(s => s
                .Index(indexName)
                .Type(GetTypeName<TObject>())
                .Query(q => q.Term(t => t.Field("_id").Value(id)))
            ).Documents.FirstOrDefault();

        public ISearchResponse<TObject> Search<TObject>(string indexName,
            Func<SearchDescriptor<TObject>, ISearchRequest<TObject>> selector)
            where TObject : class
            => Client.Search(selector);

        private static string GetTypeName<T>() where T : class
            => typeof(T).Name.ToLower();
    }
}
