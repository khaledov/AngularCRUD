using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMgr.Api.Services
{
    /// <summary>
    /// Interface IEmailSender
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="message">The message.</param>
        /// <returns>Task.</returns>
        Task SendEmailAsync(string email, string message);
    }
}
