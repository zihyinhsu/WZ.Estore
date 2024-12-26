namespace WZ.Estore.Controllers
{
	public class RegisterDto
	{
		public string Name { get; set; }
		public string Account { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string Password { get; set; }
		public string EncryptedPassword { get; set; }
	}
}