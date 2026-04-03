using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Models.Events;

namespace Pazario.Products.Domain.Models
{
    public class Model : BaseEntity
    {
        private Model()
        {
            Products = new HashSet<Products.Product>();
        }
        public Name Name { get; private set; }
        public Guid MarkaId { get; private set; }
        public Markas.Marka Marka { get; private set; }
        public HashSet<Products.Product> Products { get; private set; }

        public static Model Create(string name, Guid markaId)
        {
            var model = new Model
            {
                Name = (Name)name,
                MarkaId = markaId
            };
            model.AddDomainEvent(new AddModelEvent());
            return model;
        }

        public Model Update(string name, Guid markaId)
        {
            Name = (Name)name;
            MarkaId = markaId;
            AddDomainEvent(new UpdateModelEvent());
            return this;
        }
    }
}
