using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string? EntityType { get; }

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string entityType, string message)
            : base(message)
        {
            EntityType = entityType;
        }

        public EntityNotFoundException(string entityType, string message, Exception innerException)
            : base(message, innerException)
        {
            EntityType = entityType;
        }

    }
}
