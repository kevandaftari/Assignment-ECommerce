
namespace ECommerceService.Entity.Models
{
    public class Item
    {    
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemDescription { get; set; }
        public float Price { get; set; }
        public int NumberOfPurchases { get; set; }
        public int RemainingCount { get; set; }
    }
}
