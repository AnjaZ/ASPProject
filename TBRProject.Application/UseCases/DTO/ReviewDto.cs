using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Application.UseCases.DTO
{
    public class ReviewDto : BaseDto
    {
        public string User { get; set; }
        public string Content { get; set; }
        public int Stars { get; set; }
    }

    public class PostReviewDto : BaseDto
    {
        public int Book { get; set; }
        public string Content { get; set; }
        public int Stars { get; set; }
    }
}
