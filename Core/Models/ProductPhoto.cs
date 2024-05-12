using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ProductPhoto:BaseEntity
    {
        public string PhotoUrl {get; set;}
        public bool IsMain { get; set;}
        public int ProductId {  get; set;}
        public Product Product { get; set;}
    }
}
