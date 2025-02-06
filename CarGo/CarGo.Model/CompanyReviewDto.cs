namespace CarGo.Model
{
    public class CompanyInfoDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required List<string> Locations { get; set; }

        //Add review list when Review model is done
        //public List<Review> Reviews { get; set; }
    }
}