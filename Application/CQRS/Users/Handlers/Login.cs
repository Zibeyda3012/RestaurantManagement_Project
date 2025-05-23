﻿using Application.CQRS.Users.DTOs;
using Application.Services;
using Application.Services.LogService;
using Common.Exceptions;
using Common.GlobalResponse.Generics;
using Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Repository.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.CQRS.Users.Handlers;

public class Login
{
    public class LoginRequest : IRequest<ResponseModel<LoginResponseDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IConfiguration configuration, ILoggerService loggerService) : IRequestHandler<LoginRequest, ResponseModel<LoginResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILoggerService _loggerService = loggerService;

        public async Task<ResponseModel<LoginResponseDTO>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            _loggerService.LogInfo($"{request.Email} ile sistemə giriş etmək istədi");

            User currentUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);

            if (currentUser == null)
            {
                _loggerService.LogWarning($"{request.Email} ile istifadeci movcud deyildir");
                throw new BadRequestException("User with provided email doesn't exist"); 
            }

            var hashedPassword = PasswordHasher.ComputePasswordToSha256Has(request.Password);

            if (hashedPassword != currentUser.PasswordHash) 
            {
                _loggerService.LogWarning("Yalnis parol daxil edildi");
                throw new BadRequestException("Wrong Password");
            }

            List<Claim> authClaim = [
                new Claim(ClaimTypes.NameIdentifier,currentUser.Id.ToString()),
                new Claim(ClaimTypes.Name,currentUser.Name),
                new Claim(ClaimTypes.Email,currentUser.Email),
                new Claim(ClaimTypes.MobilePhone,currentUser.Phone),
                new Claim(ClaimTypes.Role,currentUser.UserRole.ToString())
                ];

            JwtSecurityToken token = TokenService.CreateToken(authClaim, _configuration);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            string refreshTokenString = TokenService.GenerateRefreshToken();

            RefreshToken refreshToken = new()
            {
                Token = refreshTokenString,
                UserId = currentUser.Id,
                ExpirationDate = DateTime.Now.AddDays(Double.Parse(_configuration.GetRequiredSection("JWT:RefreshTokenExpirationDays").Value!))
            };

            await _unitOfWork.RefreshTokenRepository.SaveRefreshToken(refreshToken);
            await _unitOfWork.SaveChanges();

            LoginResponseDTO response = new LoginResponseDTO()
            {
                AccessToken = tokenString,
                RefreshToken = refreshTokenString
            };

            _loggerService.LogInfo($"{request.Email} sisteme daxil oldu");

            return new ResponseModel<LoginResponseDTO>
            {
                Data = response
            };
        }
    }

}
