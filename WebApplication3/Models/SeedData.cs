using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Areas.Identity.Data;
using WebApplication3.Data;

namespace WebApplication3.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<WebApplication3User>>();
            IdentityResult roleResult;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            WebApplication3User user = await UserManager.FindByEmailAsync("admin@webapplication3.com");
            if (user == null)
            {
                var User = new WebApplication3User();
                User.Email = "admin@webapplication3.com";
                User.UserName = "admin@webapplication3.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }
            // Create "User" Role
            IdentityResult userRoleResult;
            var userRoleCheck = await RoleManager.RoleExistsAsync("User");
            if (!userRoleCheck)
            {
                userRoleResult = await RoleManager.CreateAsync(new IdentityRole("User"));
            }
        }

        public static void Initialize (IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                if (context.Book.Any() || context.Author.Any() || context.Genre.Any() || context.UserBooks.Any() || context.Review.Any())
                {
                    return;
                }

                context.Author.AddRange(
                    new Author { FirstName = "George", LastName = "R.R.Martin", BirthDate = DateTime.Parse("1948-9-20"), Nationality = "American", Gender = "Male" },
                    new Author { FirstName = "J.K.", LastName = "Rowling", BirthDate = DateTime.Parse("1965-7-31"), Nationality = "British", Gender = "Female" },
                    new Author { FirstName = "Nicholas", LastName = "Sparks", BirthDate = DateTime.Parse("1965-12-31"), Nationality = "American", Gender = "Male" },
                    new Author { FirstName = "Elena", LastName = "Armas", BirthDate = DateTime.Parse("1999-9-9"), Nationality = "Spanish", Gender = "Female" },
                    new Author { FirstName = "Suzanne", LastName = "Collins", BirthDate = DateTime.Parse("1962-8-10"), Nationality = "American", Gender = "Female" },
                    new Author { FirstName = "Rebecca", LastName = "Yarros", BirthDate = DateTime.Parse("1981-4-14"), Nationality = "American", Gender = "Female" }
                );
                context.SaveChanges();

                context.Book.AddRange(
                    /*1*/new Book { Title="Fire and Blood", YearPublished=2018, NumPages=736,
                    Description= "Fire & Blood is a fantasy book by American writer George R. R. Martin and illustrated by Doug Wheatley. It tells the history of House Targaryen, the dynasty that ruled the Seven Kingdoms of Westeros in the backstory of his series A Song of Ice and Fire.",
                    Publisher="Nemira",FrontPage= "https://m.media-amazon.com/images/I/81pa0QoG6ML._SL1500_.jpg",
                    DownloadUrl= "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.amazon.com%2FFire-Blood-Thrones-Targaryen-History%2Fdp%2F152479628X&psig=AOvVaw1-ab6lteB-wh6pNIyjMaq4&ust=1715852718323000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCNiKvtGvj4YDFQAAAAAdAAAAABAI",
                    AuthorId =context.Author.Single(d => d.FirstName == "George" && d.LastName=="R.R.Martin").Id
                    },
                    new Book
                    {
                        Title = "A Game of Thrones",
                        YearPublished = 1996,
                        NumPages = 694,
                        Description = "A Game of Thrones, the first novel in American author George R. R. Martin's A Song of Ice and Fire series, was published in 1996. The novel is a work of epic fantasy that follows the political machinations of several noble families as they vie for control of the Iron Throne of the land of Westeros",
                        Publisher = "Nemira",
                        FrontPage = "https://awoiaf.westeros.org/images/9/93/AGameOfThrones.jpg",
                        DownloadUrl = "https://www.nothuman.net/images/files/discussion/1/e72f9f1f181a66887baa7270037c582e.pdf",
                        AuthorId = context.Author.Single(d => d.FirstName == "George" && d.LastName == "R.R.Martin").Id
                    },
                    new Book
                    {
                        Title = "Harry Potter and the Philosopher's Stone",
                        YearPublished = 1997,
                        NumPages = 223,
                        Description = "The book is about 11 year old Harry Potter, who receives a letter saying that he is invited to attend Hogwarts, school of witchcraft and wizardry. He then learns that a powerful wizard and his minions are after the sorcerer's stone that will make this evil wizard immortal and undefeatable",
                        Publisher = "Bloomsbury",
                        FrontPage = "https://upload.wikimedia.org/wikipedia/en/6/6b/Harry_Potter_and_the_Philosopher%27s_Stone_Book_Cover.jpg",
                        DownloadUrl = "https://docenti.unimc.it/antonella.pascali/teaching/2018/19055/files/ultima-lezione/harry-potter-and-the-philosophers-stone",
                        AuthorId = context.Author.Single(d => d.FirstName == "J.K." && d.LastName == "Rowling").Id
                    },
                    new Book
                    {
                        Title = "The Notebook",
                        YearPublished = 1996,
                        NumPages = 214,
                        Description = "The story centers on the relationship between Noah Calhoun and Allie Nelson. Spanning over five decades, their love endures an uncertain beginning, the onset and conclusion of World War II, the death of one child, and Allie's eventual diagnosis of Alzheimer's disease. The novel is framed by the titular notebook.",
                        Publisher = "Warner Books",
                        FrontPage = "https://m.media-amazon.com/images/I/51TziXvwncL.jpg",
                        DownloadUrl = "https://docs.google.com/file/d/0B3vyNXp6qDWwOVRWTDJFdmNyYUk/view?resourcekey=0-5sQvu9p-L-v8Ss9Y16GPZw",
                        AuthorId = context.Author.Single(d => d.FirstName == "Nicholas" && d.LastName == "Sparks").Id
                    },
                    new Book
                    {
                        Title = "The American Roommate Experiment",
                        YearPublished = 2022,
                        NumPages = 400,
                        Description = "From the author of the Goodreads Choice Award winner The Spanish Love Deception, the eagerly anticipated follow-up featuring Rosie Graham and Lucas Martín, who are forced to share a New York apartment.",
                        Publisher = "Simon & Schuster",
                        FrontPage = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1660147951i/60114057.jpg",
                        DownloadUrl = "https://www.docdroid.net/L6LZP1u/pdf-download-the-american-roommate-experiment-spanish-love-deception-2-by-pdf",
                        AuthorId = context.Author.Single(d => d.FirstName == "Elena" && d.LastName == "Armas").Id
                    },
                    new Book
                    {
                        Title = "The Hunger Games",
                        YearPublished = 2008,
                        NumPages = 374,
                        Description = "The Hunger Games is a 2008 dystopian young adult novel by the American writer Suzanne Collins. It is written in the perspective of 16-year-old Katniss Everdeen, who lives in the future, post-apocalyptic nation of Panem in North America.",
                        Publisher = "Scholastic",
                        FrontPage = "https://upload.wikimedia.org/wikipedia/en/d/dc/The_Hunger_Games.jpg",
                        DownloadUrl = "https://archive.org/details/the-hunger-games-pdf-download",
                        AuthorId = context.Author.Single(d => d.FirstName == "Suzanne" && d.LastName == "Collins").Id
                    },
                    new Book
                    {
                        Title = "Catching Fire",
                        YearPublished = 2009,
                        NumPages = 391,
                        Description = "Catching Fire is a 2009 dystopian young adult fiction novel by the American novelist Suzanne Collins, the second book in The Hunger Games series. As the sequel to the 2008 bestseller The Hunger Games, it continues the story of Katniss Everdeen and the post-apocalyptic nation of Panem.",
                        Publisher = "Scholastic",
                        FrontPage = "https://upload.wikimedia.org/wikipedia/en/a/a2/Catching_Fire_%28Suzanne_Collins_novel_-_cover_art%29.jpg",
                        DownloadUrl = "https://archive.org/details/the-hunger-games-pdf-download",
                        AuthorId = context.Author.Single(d => d.FirstName == "Suzanne" && d.LastName == "Collins").Id
                    },
                    new Book
                    {
                        Title = "Mockingjay",
                        YearPublished = 2010,
                        NumPages = 390,
                        Description = "Mockingjay is a 2010 dystopian young adult fiction novel by American author Suzanne Collins. It is chronologically the last installment of The Hunger Games series, following 2008's The Hunger Games and 2009's Catching Fire. The book continues the story of Katniss Everdeen, who agrees to unify the districts of Panem in a rebellion against the tyrannical Capitol.",
                        Publisher = "Scholastic",
                        FrontPage = "https://upload.wikimedia.org/wikipedia/en/c/cc/Mockingjay.JPG",
                        DownloadUrl = "https://archive.org/details/hunger-games-mocking-jay",
                        AuthorId = context.Author.Single(d => d.FirstName == "Suzanne" && d.LastName == "Collins").Id
                    },
                    new Book
                    {
                        Title = "Fourth Wing",
                        YearPublished = 2023,
                        NumPages = 512,
                        Description = "Fourth Wing is a 2023 new adult[1] fantasy novel by American author Rebecca Yarros. It is the first book in the Empyrean series. Released on May 2, 2023, the novel achieved viral success on TikTok's reader community BookTok, which propelled it to reach No. 1 on The New York Times bestseller list.",
                        Publisher = "Red Tower Books",
                        FrontPage = "https://upload.wikimedia.org/wikipedia/en/d/dd/Fourth_Wing_Cover_Art.jpeg",
                        DownloadUrl = "https://archive.org/details/fourth-wing-the-empyrean-book-01",
                        AuthorId = context.Author.Single(d => d.FirstName == "Rebecca" && d.LastName == "Yarros").Id
                    },
                    new Book
                    {
                        Title = "Iron Flame",
                        YearPublished = 2023,
                        NumPages = 623,
                        Description = "In Iron Flame, Violet Sorrengail returns to Basgiath War College as a second-year after managing to survive the trials of her first year and after coming across a number of new revelations about the political machinations brewing across the continent",
                        Publisher = "Red Tower Books",
                        FrontPage = "https://i0.wp.com/recaptains.co.uk/wp-content/uploads/2023/11/iron-flame.png?fit=1299%2C1938&ssl=1",
                        DownloadUrl = "https://www.pdfreaderpro.com/blog/iron-flame-pdf-download",
                        AuthorId = context.Author.Single(d => d.FirstName == "Rebecca" && d.LastName == "Yarros").Id
                    });
                context.SaveChanges();

                context.Genre.AddRange(
                    new Genre { GenreName = "Fantasy" },
                    new Genre { GenreName = "Romance"},
                    new Genre { GenreName ="Science Fiction"},
                    new Genre { GenreName="Action"}
                    );
                context.SaveChanges();

                context.BookGenre.AddRange(
                    new BookGenre { BookId = 1, GenreId = 1 },
                    new BookGenre { BookId = 2, GenreId = 1 },
                    new BookGenre { BookId = 3, GenreId = 1 },
                    new BookGenre { BookId = 4, GenreId = 2 },
                    new BookGenre { BookId = 5, GenreId = 2 },
                    new BookGenre { BookId = 6, GenreId = 3 },
                    new BookGenre { BookId = 6, GenreId = 4 },
                    new BookGenre { BookId = 7, GenreId = 3 },
                    new BookGenre { BookId = 8, GenreId = 3 },
                    new BookGenre { BookId = 8, GenreId = 4 },
                    new BookGenre { BookId = 9, GenreId = 1 },
                    new BookGenre { BookId = 9, GenreId = 2 },
                    new BookGenre { BookId = 10, GenreId = 1 },
                    new BookGenre { BookId = 10, GenreId = 2 }
                    );
                context.SaveChanges();

                context.Review.AddRange(
                    new Review { BookId = 1, AppUser="Ana", Comment="Amazing book!", Rating=10},
                    new Review { BookId = 1, AppUser = "Emily", Comment = "Could've been better", Rating = 9 },
                    new Review { BookId = 1, AppUser = "Victoria", Comment = "I liked it", Rating = 9 },
                    new Review { BookId = 2, AppUser = "Lukas", Comment = "The show doesn't do it justice", Rating = 10 },
                    new Review { BookId = 2, AppUser = "Ana", Comment = "Loved it", Rating = 10 },
                    new Review { BookId = 3, AppUser = "Lukas", Comment = "The movie was better..", Rating = 5 },
                    new Review { BookId = 3, AppUser = "Theo", Comment = "Fantastic", Rating = 10 },
                    new Review { BookId = 4, AppUser = "Emily", Comment = "I loved it", Rating = 9 },
                    new Review { BookId = 4, AppUser = "Victoria", Comment = "I loved the story!", Rating = 10 },
                    new Review { BookId = 5, AppUser = "Ana", Comment = "Fun to read", Rating = 8 },
                    new Review { BookId = 6, AppUser = "Maya", Comment = "What a start to a series", Rating = 10 },
                    new Review { BookId = 6, AppUser = "Ana", Comment = "I like Divergent better", Rating = 6 },
                    new Review { BookId = 7, AppUser = "Victoria", Comment = "My favourite from the series!", Rating = 10 },
                    new Review { BookId = 7, AppUser = "Maya", Comment = "So good", Rating = 8 },
                    new Review { BookId = 8, AppUser = "Lukas", Comment = "What an ending", Rating = 8 },
                    new Review { BookId = 8, AppUser = "Victoria", Comment = "They ruined my favorite series", Rating = 4 },
                    new Review { BookId = 8, AppUser = "Emily", Comment = "Could've been better", Rating = 6 },
                    new Review { BookId = 9, AppUser = "Ana", Comment = "Looking forward to the series!", Rating = 8 },
                    new Review { BookId = 10, AppUser = "Victoria", Comment = "Amazing storytelling", Rating = 9 },
                    new Review { BookId = 10, AppUser = "Maya", Comment = "Good read.", Rating = 8 }
                    );
                context.SaveChanges();

                context.UserBooks.AddRange(
                    new UserBooks { AppUser="Emily", BookId=1},
                    new UserBooks { AppUser = "Ana", BookId = 1 },
                    new UserBooks { AppUser = "Victoria", BookId = 1 },
                    new UserBooks { AppUser = "Lukas", BookId = 2 },
                    new UserBooks { AppUser = "Ana", BookId = 2 },
                    new UserBooks { AppUser = "Theo", BookId = 3 },
                    new UserBooks { AppUser = "Maya", BookId = 6 },
                    new UserBooks { AppUser = "Maya", BookId = 7 },
                    new UserBooks { AppUser = "Emily", BookId = 4 },
                    new UserBooks { AppUser = "Victoria", BookId = 10 }
                    );
                context.SaveChanges();
            }
        }
    }
}
