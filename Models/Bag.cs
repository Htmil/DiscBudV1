using DiscBudV1.Areas.Identity.Data;

namespace DiscBudV1.Models
{
    public class Bag
    {
        public int? BagId { get; set; }
        public string? UserId { get; set; }
        public DiscBudV1User User { get; set; }
        public int InvdiscId { get; set; }
        public Invdisc Invdisc { get; set; }
    }
}
