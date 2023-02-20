
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceService.Entity.Models
{
    public class Cart
    {
        public Cart()
        {
            this.Items = new List<CartItemDetails>();
        }
        [Key]
        public int UserId { get; set; }
        public virtual ICollection<CartItemDetails> Items { get; set; }
    }

    public class CartItemDetails
    {
   
        public int ItemId { get; set; }
        public int Count { get; set; }

        #region Navigation Property
        public Cart? Cart { get; set; }
        public int CartUserId { get; set; }
        #endregion
    }
}
