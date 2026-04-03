using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Markas.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Markas
{
    public class Marka : BaseEntity
    {
        public Marka()
        {
            Models = new HashSet<Models.Model>();
        }
        public Name Name { get; private set; }
        public HashSet<Models.Model> Models { get; private set; }

        public static Marka Create(string name)
        {
            var marka = new Marka
            {
                Name = (Name)name
            };
            marka.AddDomainEvent(new AddMarkaEvent(marka));
            return marka;
        }

        public Marka Update(string name)
        {
            Name = (Name)name;
            AddDomainEvent(new UpdateMarkaEvent(Id));
            return this;
        }

        public void Remove()
        {
            AddDomainEvent(new RemoveMarkaEvent(Id));
        }
    }
}
