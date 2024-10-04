namespace CRAVENEST.Model
{
    public class FoodItem
    {
        public int FoodItemId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public string? Cuisine { get; set; }
        public bool Available { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}