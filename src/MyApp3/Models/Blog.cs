using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp3.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
