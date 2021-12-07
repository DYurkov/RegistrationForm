namespace WebApi.Entities
{
    public class RegistrationReferrer
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public bool CanEnterManually { get; set; }
	}
}