using System.ComponentModel.DataAnnotations;

namespace CarGo.Model
{
    public class VehicleStatus
    {
        [Key] public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}