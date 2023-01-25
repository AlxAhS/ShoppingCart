using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartMvcUI.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        //[Required]

        public int ShoppingCart_Id { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public Book Book { get; set; }
        public ShoppingCart ShoppingCart { get; set; } 
    }
}
