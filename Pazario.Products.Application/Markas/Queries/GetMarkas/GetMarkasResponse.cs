using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Markas.Queries.GetMarkas
{
    public class GetMarkasResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int ModelsCount { get; set; }
    }
}
