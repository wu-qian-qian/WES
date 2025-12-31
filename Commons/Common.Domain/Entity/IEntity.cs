using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Entity
{
   public abstract class  IEntity<T>
    {
        public T Id { get;init;  }
    }
}
