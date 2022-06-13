using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Domain
{
    public class UserBook
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Action { get; set; }
        public virtual User Reader { get; set; }
        public virtual Book Books { get; set; }
        public virtual ReaderAction BookAction { get; set; }

    }
}
