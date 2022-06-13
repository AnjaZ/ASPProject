using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Application.UseCases.DTO
{
    public class GenreDto : BaseDto
    {
        public string Name { get; set; }
    }

    public class BooksGenresDto
    {
        public IEnumerable<TitleBookDto> Books { get; set; }
    }
}
