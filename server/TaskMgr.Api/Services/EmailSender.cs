using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TaskMgr.Api.Settings;

namespace TaskMgr.Api.Services
{
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// Gets the email settings.
        /// </summary>
        /// <value>The email settings.</value>
        public EmailSettings _emailSettings { get; }

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<EmailSender> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender" /> class.
        /// </summary>
        /// <param name="emailSettings">The email settings.</param>
        /// <param name="logger">The logger.</param>
        public EmailSender(IOptions<EmailSettings> emailSettings, ILogger<EmailSender> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }
        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="message">The message.</param>
        /// <returns>Task.</returns>
        public Task SendEmailAsync(string email, string message)
        {

            Execute(email, message).Wait();
            return Task.FromResult(0);
        }

        /// <summary>
        /// Executes the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="message">The message.</param>
        /// <returns>Task.</returns>
        public async Task Execute(string email, string message)
        {
            try
            {

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.Sender)
                };
                mail.To.Add(new MailAddress(email));

                mail.Subject = _emailSettings.Subject;
                mail.Body = string.Format(_emailSettings.Message, message);
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.Smtp, _emailSettings.Port))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    smtp.EnableSsl = _emailSettings.IsTls;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error has been occured during sending email: {ex}");
                throw ex;

            }
        }
    }
}
