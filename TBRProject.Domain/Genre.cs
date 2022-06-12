using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Domain
{
    public class Genre : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<BookGenre> BookGenre { get; set; } = new List<BookGenre>();
    }
}
