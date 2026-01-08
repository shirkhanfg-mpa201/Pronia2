namespace Pronia2.Abstractions
{
    public interface IEMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
