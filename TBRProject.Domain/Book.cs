using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Domain
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }   
        public virtual ICollection<AuthorBook> Authors{ get; set; } = new List<AuthorBook>();
        public virtual ICollection<UserBook> Users{ get; set; } = new List<UserBook>();
        public virtual ICollection<Review> Reviews{ get; set; } = new List<Review>();
        public virtual ICollection<BookGenre> BookGenre{ get; set; } = new List<BookGenre>();
    }
}
