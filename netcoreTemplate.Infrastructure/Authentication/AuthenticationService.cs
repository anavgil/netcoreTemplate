using Application.Abstractions;
using Application.Users.Dtos;
using Domain.Identity.Model;
using FluentResults;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Infrastructure.Authentication;

public class AuthenticationService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService) : IAuthenticationService
{
    public async Task<IResult<string>> CreateUserAsync(string name, string surname, string user, string pass, string email)
    {
        try
        {
            var identityResult = await userManager.CreateAsync(new User { UserName = user, FirstName = name, LastName = surname, Email = email }, pass);

            if (!identityResult.Succeeded)
            {
                Result.Fail<string>("User creation failed")
                    .WithError(identityResult.Errors.Select(e => e.Description).FirstOrDefault());
            }

            var userEntity = userManager.FindByNameAsync(user);
            var existRole = roleManager.RoleExistsAsync(Roles.Admin);

            var (userResult, roleResult) = await TaskExtension.WhenAllExt(userEntity, existRole);

            if (!roleResult)
            {
                _ = await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            var role_ = await userManager.AddToRoleAsync(userResult, Roles.Admin);

            return identityResult.Succeeded ?
                    Result.Ok<string>(userResult.Id) :
                    Result.Fail<string>("User role creation failed")
                .WithError(identityResult.Errors.Select(e => e.Description).FirstOrDefault());
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.Message);
            return Result
                    .Fail<string>("Unauthorized Access")
                    .WithError(ex.Message);
        }
    }

    public async Task<IResult<LoginResponseDto>> LoginAsync(string user, string pass)
    {
        try
        {
            var userModel = await userManager.FindByNameAsync(user);

            if (userModel == null)
            {
                return Result.Fail<LoginResponseDto>("User not exist");
            }

            bool isValidPassword = await userManager.CheckPasswordAsync(userModel, pass);

            if (isValidPassword == false)
            {
                //return Unauthorized();
                return Result.Fail<LoginResponseDto>("Unauthorized Access");
            }

            var role = await userManager.GetRolesAsync(userModel);

            // creating the necessary claims
            List<Claim> authClaims = [
                    new (ClaimTypes.Name, userModel.UserName),
                    new (ClaimTypes.Email, userModel.Email),
                    new (ClaimTypes.Email, role.Any() ? role.First() : string.Empty),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                // unique id for token
        ];

            var userRoles = await userManager.GetRolesAsync(userModel);

            // adding roles to the claims. So that we can get the user role from the token.
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            // generating access token
            var token = tokenService.GenerateAccessToken(authClaims);

            string refreshToken = tokenService.GenerateRefreshToken();

            ////save refreshToken with exp date in the database
            //var tokenInfo = _context.TokenInfos.
            //            FirstOrDefault(a => a.Username == user.UserName);

            //// If tokenInfo is null for the user, create a new one
            //if (tokenInfo == null)
            //{
            //    var ti = new TokenInfo
            //    {
            //        Username = user.UserName,
            //        RefreshToken = refreshToken,
            //        ExpiredAt = DateTime.UtcNow.AddDays(7)
            //    };
            //    _context.TokenInfos.Add(ti);
            //}
            //// Else, update the refresh token and expiration
            //else
            //{
            //    tokenInfo.RefreshToken = refreshToken;
            //    tokenInfo.ExpiredAt = DateTime.UtcNow.AddDays(7);
            //}

            //await _context.SaveChangesAsync();

            //return Ok(new TokenModel
            //{
            //    AccessToken = token,
            //    RefreshToken = refreshToken
            //});
            return Result.Ok<LoginResponseDto>(new LoginResponseDto() { AccessToken = token, RefreshToken = refreshToken });
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.Message);
            return Result
                    .Fail<LoginResponseDto>("Unauthorized Access")
                    .WithError(ex.Message);
        }
    }

    public async Task<IResult<LoginResponseDto>> RefreshTokenAsync(LoginResponseDto loginResponseDto)
    {
        try
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(loginResponseDto.AccessToken);
            var username = principal.Identity.Name;

            //var tokenInfo = _context.TokenInfos.SingleOrDefault(u => u.Username == username);

            //if (tokenInfo == null || tokenInfo.RefreshToken != tokenModel.RefreshToken || tokenInfo.ExpiredAt <= DateTime.UtcNow)
            //{
            //    return BadRequest("Invalid refresh token. Please login again.");
            //}

            var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            //tokenInfo.RefreshToken = newRefreshToken; // rotating the refresh token
            //await _context.SaveChangesAsync();

            return await Task.FromResult(Result.Ok(new LoginResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            }));
        }
        catch (Exception ex)
        {
            return Result
                    .Fail<LoginResponseDto>("Invalid refresh token. Please login again.")
                    .WithError(ex.Message);
        }
    }
}
