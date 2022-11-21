using Avesta.Data.Model;

namespace AvestaWebAPI.Data
{
    public class AvestaCrudEntity : BaseEntity
    {
        public int Number { get; set; }
        public string? Content { get; set; }
    }
}
