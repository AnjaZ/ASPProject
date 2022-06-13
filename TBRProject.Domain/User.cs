using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public int? ImageId { get; set; }
        public virtual Role Role { get; set; }
        public virtual Image Image { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
        public virtual ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<UserUseCase> UseCases { get; set; }


    }
}
