using Framework;
using Framework.Authentication;
using Framework.CQRS.Queries;
using Framework.EventBus.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Events;
using TaskMgr.Domain.Queries;
using TaskMgr.Domain.Repositories;
using TaskMgr.Domain.Services;

namespace TaskMgr.Domain.QueryHandlers
{
    public class SignInQueryHandler : IQueryHandler<SignIn, JsonWebToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IClaimsProvider _claimsProvider;
        private readonly IEventBus _eventBus;
        public SignInQueryHandler(IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtHandler jwtHandler,
            IClaimsProvider claimsProvider,
            IRefreshTokenRepository refreshTokenRepository,
            IEventBus eventBus)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _refreshTokenRepository = refreshTokenRepository;
            _claimsProvider = claimsProvider;
            _eventBus = eventBus;

        }
        public async Task<JsonWebToken> HandleAsync(SignIn query)
        {
            var user = await _userRepository.GetByEmail(query.Email);
            if (user == null || !user.ValidatePassword(query.Password, _passwordHasher))
            {
                throw new TaskMgrException(Codes.InvalidCredentials,
                    "Invalid credentials.");
            }
            var refreshToken = new RefreshToken(user, _passwordHasher);
            var claims = await _claimsProvider.GetAsync(user.Id);

            claims.Add("FirstName", user.FirstName);
            claims.Add("LastName", user.LastName);

            var jwt = _jwtHandler.CreateToken(user.Id, null, claims);
            jwt.RefreshToken = refreshToken.Token;
            await _eventBus.Publish<SignedIn>(new SignedIn(user.Id, user.FirstName, user.LastName, refreshToken));
            await _refreshTokenRepository.AddAsync(refreshToken);
            return jwt;
        }
    }
}
