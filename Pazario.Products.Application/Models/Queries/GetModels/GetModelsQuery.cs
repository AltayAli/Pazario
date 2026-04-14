using Pazario.Common.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Models.Queries.GetModels
{
    public class GetModelsQuery : IQuery<List<GetModelsQueryResponse>>
    {
        public Guid MarkaId {  get; set; }
    }
}
