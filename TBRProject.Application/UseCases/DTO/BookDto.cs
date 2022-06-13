using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Application.UseCases.DTO
{
    public class BookDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Authors { get; set; }
    }
    public class TitleBookDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Authors { get; set; }
    }
    public class FindBookDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<ReviewDto> Reviews { get; set; }
    }
    public class PostBookDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IEnumerable<int> Genres { get; set; }
        public IEnumerable<int> Authors { get; set; }
    }
}
