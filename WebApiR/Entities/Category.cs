using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiR.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public byte[] Image { get; set; }

    }
}
