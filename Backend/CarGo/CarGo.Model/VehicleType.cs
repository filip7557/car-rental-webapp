using System.ComponentModel.DataAnnotations;

namespace CarGo.Model
{
    public class VehicleType
    {
        [Key] public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}