using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Domain
{
    public class Review : Entity
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int Stars { get; set; }
        public virtual Book Books { get; set; }
        public virtual User Users { get; set; }
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();    
    }
}
