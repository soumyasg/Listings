using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listings.Models
{
    public interface IModelRecord<TKey>
    {
        public TKey Id { get; set; }
    }
}
