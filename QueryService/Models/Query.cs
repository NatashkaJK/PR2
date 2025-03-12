using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryService.Models
{
    public class Query
    {
        private static int _nextId = 1;
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }

        public Query()
        {
            Id = _nextId++;
        }
    }
}