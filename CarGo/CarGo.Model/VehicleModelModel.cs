using System.ComponentModel.DataAnnotations;

namespace CarGo.Model
{
    public class VehicleModel
    {
        [Key] public Guid ID { get; set; }
        public int MakeID { get; set; }
        public int TypeID { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int? EnginePower { get; set; }
    }
}