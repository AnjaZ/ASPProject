using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Domain
{
    public class AuthorBook
    {
        public int BookId { get; set; }
        public int UserId { get; set; }

        public virtual User Author { get; set; } 
        public virtual Book Book { get; set; }
    }
}
