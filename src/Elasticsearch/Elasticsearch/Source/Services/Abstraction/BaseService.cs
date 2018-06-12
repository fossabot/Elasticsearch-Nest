using Elasticsearch.Source.Core.Abstraction;

namespace Elasticsearch.Source.Services.Abstraction
{
    public abstract class BaseService
    {
        protected BaseService(IElastic elastic)
            => Elastic = elastic;

        protected IElastic Elastic { get; }
    }
}
