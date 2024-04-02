using AutoMapper;
using LostAndFound.AuthService.Core.DateTimeProviders;
using LostAndFound.AuthService.Core.TokenGenerators;
using LostAndFound.AuthService.Core.TokenValidators;
using LostAndFound.AuthService.CoreLibrary.Exceptions;
using LostAndFound.AuthService.CoreLibrary.Messages;
using LostAndFound.AuthService.CoreLibrary.Requests;
using LostAndFound.AuthService.CoreLibrary.Responses;
using LostAndFound.AuthService.DataAccess.Entities;
using LostAndFound.AuthService.DataAccess.Repositories.Interfaces;
using LostAndFound.AuthService.ThirdPartyServices.RabbitMQ.MessageProducers;
using Microsoft.AspNetCore.Identity;

namespace LostAndFound.AuthService.Core.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordHasher<Account> _passwordHasher;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenValidator _refreshTokenValidator;
        private readonly IMessageProducer _messageProducer;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AccountService(IPasswordHasher<Account> passwordHasher, IAccountsRepository accountsRepository, IAccessTokenGenerator accessTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator, IRefreshTokenValidator refreshTokenValidator, IMessageProducer messageProducer, IMapper mapper,
            IDateTimeProvider dateTimeProvider)
        {
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _accountsRepository = accountsRepository ?? throw new ArgumentNullException(nameof(accountsRepository));
            _accessTokenGenerator = accessTokenGenerator ?? throw new ArgumentNullException(nameof(accessTokenGenerator));
            _refreshTokenGenerator = refreshTokenGenerator ?? throw new ArgumentNullException(nameof(refreshTokenGenerator));
            _refreshTokenValidator = refreshTokenValidator ?? throw new ArgumentNullException(nameof(refreshTokenValidator));
            _messageProducer = messageProducer ?? throw new ArgumentNullException(nameof(messageProducer));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<AuthenticatedUserResponseDto> AuthenticateUser(LoginRequestDto dto)
        {
            var account = await _accountsRepository.GetSingleAsync(ac => ac.Email == dto.Email);
            if (account == null)
            {
                throw new BadRequestException("An account with a specified email does not exist.");
            }

            var result = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password");
            }

            return await CreateAuthenticatedUser(account);
        }

        public async Task ChangePassword(ChangeAccountPasswordRequestDto dto, string rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                throw new UnauthorizedException();
            }

            var account = await _accountsRepository.GetSingleAsync(ac => ac.UserId == userId);
            if (account == null)
            {
                throw new UnauthorizedException();
            }

            var result = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid current password");
            }

            var newPasswordHash = _passwordHasher.HashPassword(account, dto.NewPassword);         
            await _accountsRepository.UpdateAccountPasswordHashAsync(account.Email, newPasswordHash);
        }

        public async Task LogoutUser(string rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                throw new UnauthorizedException();
            }

            var account = await _accountsRepository.GetSingleAsync(ac => ac.UserId == userId);
            if (account == null)
            {
                throw new UnauthorizedException();
            }

            await _accountsRepository.RemoveRefreshTokenFromAccountAsync(account.Email);
        }

        public async Task<AuthenticatedUserResponseDto> RefreshUserAuthentication(RefreshRequestDto dto)
        {
            var account = await _accountsRepository.GetSingleAsync(acc => acc.RefreshToken == dto.RefreshToken);
            bool isRefreshTokenValid = _refreshTokenValidator.ValidateRefreshToken(dto.RefreshToken);
            if (account == null || !isRefreshTokenValid || account.RefreshToken != dto.RefreshToken)
            {
                throw new BadRequestException("Invalid refresh token.");
            }

            var accessToken = _accessTokenGenerator.GenerateAccessToken(account);
            return new AuthenticatedUserResponseDto
            {
                AccessToken = accessToken.Value,
                AccessTokenExpirationTime = accessToken.ExpirationTime,
                RefreshToken = account.RefreshToken,
            };
        }

        public async Task<RegisteredUserAccountResponseDto> RegisterUserAccount(RegisterUserAccountRequestDto registerUserDto)
        {
            var newUserAccount = _mapper.Map<Account>(registerUserDto);
            if (newUserAccount is null)
            {
                throw new BadRequestException("Invalid account data.");
            }
            newUserAccount.CreationTime = _dateTimeProvider.UtcNow;

            newUserAccount.PasswordHash = _passwordHasher.HashPassword(newUserAccount, registerUserDto.Password);
            await _accountsRepository.InsertOneAsync(newUserAccount);

            var newUserAccountMessage = _mapper.Map<NewUserAccountMessageDto>(newUserAccount);
            _messageProducer.SendMessage(newUserAccountMessage);

            var registeredUseResponseDto = _mapper.Map<RegisteredUserAccountResponseDto>(newUserAccount);
            return registeredUseResponseDto;
        }

        private async Task<AuthenticatedUserResponseDto> CreateAuthenticatedUser(Account account)
        {
            var accessToken = _accessTokenGenerator.GenerateAccessToken(account);
            var refreshTokenRaw = _refreshTokenGenerator.GenerateRefreshToken();

            await _accountsRepository.UpdateAccountRefreshTokenAsync(account.Email, refreshTokenRaw);

            return new AuthenticatedUserResponseDto
            {
                AccessToken = accessToken.Value,
                AccessTokenExpirationTime = accessToken.ExpirationTime,
                RefreshToken = refreshTokenRaw,
            };
        }
    }
}
