namespace Web.Services
{
	public interface IMailService
	{
		bool SendMail(string from, string to, string body);
	}
}