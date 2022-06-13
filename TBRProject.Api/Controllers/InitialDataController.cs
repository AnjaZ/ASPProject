using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TBRProject.DataAccess;
using TBRProject.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TBRProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialDataController : ControllerBase
    {
        // POST api/<InitialDataController>
        [HttpPost]
        public IActionResult Post()
        {
            //var context = new TBRContext();
            //if (context.Users.Any())
            //{
              //  return Conflict();
            //}

            var roles = new List<Role>
            {
                new Role { Name = "Admin"},
                new Role { Name = "Author"},
                new Role { Name = "Reader"}
            };

            var images = new List<Image>
            {
                new Image { Path = "image1.jpg"},   
                new Image { Path = "image2.jpg"},   
                new Image { Path = "image3.jpg"},   
                new Image { Path = "image4.jpg"},   
                new Image { Path = "image5.jpg"},   
                new Image { Path = "image6.jpg"},   
                new Image { Path = "image7.jpg"}   
            };

            var users = new List<User>
            {
                new User { FirstName = "Anja", LastName = "Zubac", Email = "anjaz@gamil.com", Password = BCrypt.Net.BCrypt.HashPassword("Sifra123!"), Role = roles.ElementAt(2), Image = images.First()},
                new User { FirstName = "Nikola", LastName = "Crvenkov", Email = "nikolac@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("Sifra123!"), Role = roles.ElementAt(2), Image = images.ElementAt(2)},
                new User { FirstName = "Luka", LastName = "Lukic", Email = "lukal@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("Sifra123!"), Role = roles.First(), Image = images.ElementAt(3)},
                new User { FirstName = "Patrick", LastName = "Rothfuss", Email = "rothfuss@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("Sifra123!"), Role = roles.ElementAt(1), Image = images.ElementAt(3)},
                new User { FirstName = "Colleen", LastName = "Hoover", Email = "hoover@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("Sifra123!"), Role = roles.ElementAt(1), Image = images.ElementAt(3)},
            };

            var genres = new List<Genre>
            {
                new Genre { Name = "Thriller"},
                new Genre { Name = "Horror"},
                new Genre { Name = "Historical"},
                new Genre { Name = "Romance"},
                new Genre { Name = "Fiction"},
                new Genre { Name = "Fantasy"},
                new Genre { Name = "Dystopian"}
            };

            var books = new List<Book>
            {
                new Book { Title = "Name of the wind", Description = "The Name of the Wind, also referred to as The Kingkiller Chronicle: Day One, is a heroic fantasy novel.", Image = images.First() },
                new Book { Title = "Verity", Description = "Lowen Ashleigh is a struggling writer on the brink of financial ruin when she accepts the job offer of a lifetime.", Image = images.ElementAt(4) },
                new Book { Title = "The Song of Achilles", Description = "Achilles, the best of all the Greeks, son of the cruel sea goddess Thetis and the legendary king Peleus, is strong, swift, and beautiful, irresistible to all who meet him.", Image = images.ElementAt(3) }
            };

            var actions = new List<ReaderAction>
            {
                new ReaderAction { Name = "want to read"},
                new ReaderAction { Name = "currently reading"},
                new ReaderAction { Name = "read"}
            };

            var authorbooks = new List<AuthorBook>
            {
                new AuthorBook { Author = users.ElementAt(3), Book = books.First()},
                new AuthorBook { Author = users.ElementAt(4), Book = books.ElementAt(1)},
                new AuthorBook { Author = users.ElementAt(3), Book = books.ElementAt(2)},
            };

            var bookganre = new List<BookGenre>
            {
                new BookGenre { Book = books.First(), Genre = genres.ElementAt(5)},
                new BookGenre { Book = books.ElementAt(1), Genre = genres.First()},
                new BookGenre { Book = books.ElementAt(2), Genre = genres.ElementAt(2)},
            };

            var readerlists = new List<UserBook>
            {
                new UserBook { Reader = users.First(), Books =books.First(), BookAction = actions.First()}
            };

            var reviews = new List<Review>
            {
                new Review { Books = books.First(), Users = users.First(), Content = "Love it", Stars = 5}
            };

            var likes = new List<Like>
            {
                new Like { Users = users.ElementAt(1),Reviews = reviews.First()}
            };

            var usecases = new List<UserUseCase>
            {
                new UserUseCase { UseCaseId = 11, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 10, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 14, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 9, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 13, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 2, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 7, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 1, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 5, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 3, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 6, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 8, User = users.ElementAt(2) },
                new UserUseCase { UseCaseId = 12, User = users.ElementAt(2) },
            };

            //context.Roles.AddRange(roles);
            //context.Images.AddRange(images);
            //context.Users.AddRange(users);
            //context.Genres.AddRange(genres);
            //context.Books.AddRange(books);
            //context.Reviews.AddRange(reviews);
            //context.Likes.AddRange(likes);
            //context.ReaderActions.AddRange(actions);
            //context.AuthorBooks.AddRange(authorbooks);
            //context.ReadersList.AddRange(readerlists);

            //context.SaveChanges();
            return StatusCode(201);
        }

    }
}
