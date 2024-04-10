

using MoviesApiDevCreed.Repository.ModelRepositories;

namespace MoviesApiDevCreed.Model
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

       
    }
}
