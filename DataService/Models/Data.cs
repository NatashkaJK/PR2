using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Models
{
    public class Data
    {
        private static int _nextId = 1;
        public int Id { get; set;}
        public string Name { get; set;}
        public string Email { get; set;}
    

        public Data()
        {
            Id = _nextId++;
        }
    }
}