using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Domain.Entities.Base
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; private set; }

        protected Entity()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
