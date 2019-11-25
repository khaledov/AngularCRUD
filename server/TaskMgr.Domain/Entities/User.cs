using Framework;
using Framework.Mongo;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TaskMgr.Domain.Entities
{
    public class User : IIdentity<string>
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public string Id { get; private set; }
        public string Email { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PasswordHash { get; private set; }
        public string RandomPasswordHash { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected User()
        {
        }

        public User(string id, string email, string firstName, string lastName)
        {
            if (!EmailRegex.IsMatch(email))
            {
                throw new TaskMgrException(Codes.InvalidEmail,
                    $"Invalid email: '{email}'.");
            }

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrWhiteSpace(firstName))
                throw new TaskMgrException("First Name is required");

            if (string.IsNullOrEmpty(lastName) || string.IsNullOrWhiteSpace(lastName))
                throw new TaskMgrException("Last Name is required");



            Id = id;
            Email = email.ToLowerInvariant();
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new TaskMgrException(Codes.InvalidPassword,
                    "Password can not be empty.");
            }
            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public void SetTempPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new TaskMgrException(Codes.InvalidPassword,
                    "Password can not be empty.");
            }
            RandomPasswordHash = passwordHasher.HashPassword(this, password);
        }
        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, PasswordHash, password) != PasswordVerificationResult.Failed;

        public bool ValidateRandomPassword(string password, IPasswordHasher<User> passwordHasher)
              => passwordHasher.VerifyHashedPassword(this, RandomPasswordHash, password) != PasswordVerificationResult.Failed;
    }
}
