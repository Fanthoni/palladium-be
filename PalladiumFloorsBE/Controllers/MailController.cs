// using System.Net;
using System.Net.Mail;
using System.Net;

public class MailController {

    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="MailController"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public MailController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Sends an email with the specified message, email, and name.
    /// </summary>
    /// <param name="message">The message to be sent in the email.</param>
    /// <param name="email">The recipient's email address.</param>
    /// <param name="name">The recipient's name.</param>
    public void SendEmail(string message, string email, string name) {
        var companyEmail = _configuration["CompanyEmail"];
        var fromPassword = _configuration["CompanyPassword"];

        if (string.IsNullOrEmpty(companyEmail) || string.IsNullOrEmpty(fromPassword))
        {
            throw new InvalidOperationException("Company email or password is not set in configuration.");
        }

        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var fromAddress = new MailAddress(companyEmail, name);
        var toAddress = new MailAddress(companyEmail, "PalladiumFloors");
        string subject = $"Message from {name}";

        var smtp = new SmtpClient
        {
            Host = "smtp.oxcs.bluehost.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };
        using var emailMessage = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = message
        };
        smtp.Send(emailMessage);
    }
}
