using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartMvcUI.Models
{
    [Table("Book")]
    public class Book
    {
        public int Id { get; set; }
        //[Required]
        [MaxLength(40)]
        public string BookName{ get; set; }   
        public int GenreId { get; set; }
        public Genre Genre;
    }
}
