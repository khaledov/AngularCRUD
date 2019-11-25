using Framework;
using Framework.CQRS.Commands;
using Framework.EventBus.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using TaskMgr.Domain.Commands.Identity;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Events;
using TaskMgr.Domain.Repositories;

namespace TaskMgr.Domain.CommandHandlers
{
    public class IdentityCommandsHandler :
       ICommandHandler<SignUp>,
       ICommandHandler<ChangePassword>,
        ICommandHandler<ChangePasswordWithRandom>,
       ICommandHandler<RecoverPassword>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEventBus _eventBus;

        public IdentityCommandsHandler(IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IEventBus eventBus)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(SignUp command)
        {
            var user = await _userRepository.GetByEmail(command.Email);
            if (user != null)
            {
                throw new TaskMgrException(Codes.EmailInUse,
                    $"Email: '{command.Email}' is already in use.");
            }

            user = new User(
                            command.Id,
                            command.Email,
                            command.FirstName,
                            command.LastName
                            );
            user.SetPassword(command.Password, _passwordHasher);
            await _userRepository.AddAsync(user);
            await _eventBus.Publish<SignedUp>(new SignedUp(command.Id,
                                                        command.Email,
                                                        command.FirstName,
                                                        command.LastName));
        }

        public async Task HandleAsync(ChangePassword command)
        {
            var user = await _userRepository.GetById(command.UserId);
            if (user == null)
            {
                throw new TaskMgrException(Codes.UserNotFound,
                    $"User with id: '{command.UserId}' was not found.");
            }
            if (!user.ValidatePassword(command.CurrentPassword, _passwordHasher))
            {
                throw new TaskMgrException(Codes.InvalidCurrentPassword,
                    "Invalid current password.");
            }
            user.SetPassword(command.NewPassword, _passwordHasher);
            await _userRepository.UpdateAsync(user);
            await _eventBus.Publish<PasswordChanged>(new PasswordChanged(command.UserId));
        }

        public async Task HandleAsync(RecoverPassword command)
        {
            var user = await _userRepository.GetByEmail(command.Email);
            if (user == null)
            {
                throw new TaskMgrException(Codes.UserNotFound,
                    $"User with email: '{command.Email}' was not found.");
            }
            var randomPassword = GenerateOTP();
            user.SetTempPassword(randomPassword, _passwordHasher);
            await _userRepository.UpdateAsync(user);
            await _eventBus.Publish<NewCodeGenerated>(new NewCodeGenerated(user.Id, command.Email, randomPassword));
        }

        public async Task HandleAsync(ChangePasswordWithRandom command)
        {
            var user = await _userRepository.GetByEmail(command.Email);
            if (user == null)
            {
                throw new TaskMgrException(Codes.UserNotFound,
                    $"User with email: '{command.Email}' was not found.");
            }
            if (!user.ValidateRandomPassword(command.RandomPassword, _passwordHasher))
            {
                throw new TaskMgrException(Codes.InvalidCurrentPassword,
                    "Invalid current password.");
            }
            user.SetPassword(command.NewPassword, _passwordHasher);
            await _userRepository.UpdateAsync(user);

        }

        private string GenerateOTP()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;

            characters += alphabets + small_alphabets + numbers;

            int length = 6;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }
    }
}
