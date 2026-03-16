using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Models.Queries.GetModels
{
    public class GetModelsQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
