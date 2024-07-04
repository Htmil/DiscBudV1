using DiscBudV1.Areas.Identity.Data;

namespace DiscBudV1.Models
{
    public class Invdisc
    {
        public int? Id { get; set; } // InventoryId
        public string? UserId { get; set; }
        public int DiscId { get; set; }
        public DiscBudV1User User { get; set; }
        public Disc Disc { get; set; } //Foreign key to DiscDb
    }
}
