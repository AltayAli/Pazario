using Pazario.Products.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Models.Queries.GetModels
{
    public class GetModelsQuery : IQuery<List<GetModelsQueryResponse>>
    {
        public Guid MarkaId {  get; set; }
    }
}
