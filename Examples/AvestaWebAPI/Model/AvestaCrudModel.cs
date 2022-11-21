using Avesta.Share.Model;

namespace AvestaWebAPI.Model
{
    public class AvestaCrudModel : BaseModel
    {
        public int Number { get; set; }
        public string? Content { get; set; }
    }
    public class CreateAvestaCrudModel : AvestaCrudModel { }
    public class EditAvestaCrudModel : AvestaCrudModel { }
}
