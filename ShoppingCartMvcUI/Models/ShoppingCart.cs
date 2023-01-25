using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartMvcUI.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; }
        //[Required]
        public string UserId { get; set; }

        public bool Isdeleted { get; set; } = false;

    }
}
