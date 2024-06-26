using DiscBudV1.Areas.Identity.Data;
namespace DiscBudV1.Models
{
    public class Disc
    {
        public int Id { get; set; }
        public string? Manufacturer { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public double? Speed { get; set; }
        public double? Glide { get; set; }
        public double? Turn { get; set; }
        public double? Fade { get; set; }
        public string? Characteristics { get; set; }
        public string UserId { get; set; }
        public DiscBudV1User User { get; set; }
    }
}
