using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Domain
{
    public class Like
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public DateTime LikedAt { get; set; }
        public virtual User Users { get; set; }
        public virtual Review Reviews { get; set; }
    }
}
