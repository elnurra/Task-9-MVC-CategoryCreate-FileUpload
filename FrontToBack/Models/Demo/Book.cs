namespace FrontToBack.Models.Demo
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookImage> BookImages{ get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
        public List<BookGenre> BookGenres { get; set; }

    }
}
