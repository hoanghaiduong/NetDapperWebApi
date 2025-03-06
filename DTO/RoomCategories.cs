using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetDapperWebApi.DTO
{
    public class AddRelationsMM<T, TKey>
    {
        public T EntityId { get; set; }
        public List<TKey> Ids { get; set; }
    }
}