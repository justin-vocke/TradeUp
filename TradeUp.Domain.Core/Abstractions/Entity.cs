using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Domain.Core.Abstractions
{
    public abstract class Entity
    {
        protected Entity(string id)
        {
            Id = id;
        }

        protected Entity()
        {

        }
        public string Id { get; init; }
    }
}
