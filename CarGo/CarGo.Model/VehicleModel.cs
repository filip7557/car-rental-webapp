using System.ComponentModel.DataAnnotations;

namespace CarGo.Model
{
    public class VehicleModel
    {
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public Guid TypeId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int? EnginePower { get; set; }
    }
}