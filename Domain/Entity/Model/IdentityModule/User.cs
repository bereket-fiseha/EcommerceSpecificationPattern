namespace Domain.Entity.Model.IdentityModule
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }


        public required string UserName { get; set; }





    }
}
