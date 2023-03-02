namespace FrontToBack.Models.Demo
{
    public class BookImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int BookId { get; set; }

        public List<BookImage> BookImages { get; set;}
    }
}
