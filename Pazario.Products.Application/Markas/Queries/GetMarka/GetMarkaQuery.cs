using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Application.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Markas.Queries.GetMarka
{
    public class GetMarkaQuery : IQuery<GetMarkaResponse>
    {
        public Guid Id { get; set; }
    }
}
