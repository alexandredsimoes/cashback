namespace Cashback.Shared
{
    public class AlbumViewModel
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string GenreIdentifier { get; set; }
    }
}