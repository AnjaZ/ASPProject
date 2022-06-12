using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Domain
{
    public class ReaderAction : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<UserBook> Reader { get; set; } = new List<UserBook>();
    }
}
