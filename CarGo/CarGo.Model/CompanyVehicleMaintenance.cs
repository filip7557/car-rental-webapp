namespace CarGo.Model
{
    public class CompanyVehicleMaintenance
    {
        public Guid Id { get; set; }
        public Guid CompanyVehicleId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}