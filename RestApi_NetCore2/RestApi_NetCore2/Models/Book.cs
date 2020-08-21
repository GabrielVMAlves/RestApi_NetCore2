using RestApi_NetCore2.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_NetCore2.Models
{
    public class Book : BaseEntity
    {
        public string Author {get; set;}
        public DateTime LaunchDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
    }
}
