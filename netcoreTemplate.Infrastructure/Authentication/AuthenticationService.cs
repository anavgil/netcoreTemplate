using Application.Abstractions;
using Application.Users.Login;
using Domain.Identity.Model;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Infrastructure.Authentication;

public class AuthenticationService(UserManager<User> userManager, ITokenService tokenService) : IAuthenticationService
{
    public async Task<IResult<string>> CreateUserAsync(string name, string surname, string user, string pass, string email)
    {
        try
        {
            var identityResult = await userManager.CreateAsync(new User { UserName = user, FirstName = name, LastName = surname, Email = email }, pass);

            if (identityResult.Succeeded)
            {
                Result.Fail<string>("User creation failed")
                    .WithError(identityResult.Errors.Select(e => e.Description).FirstOrDefault());
            }

            var userEntity = await userManager.FindByNameAsync(user);

            var roleResult = await userManager.AddToRoleAsync(userEntity, "Admin");

            return roleResult.Succeeded ?
                    Result.Ok<string>(userEntity.Id) :
                    Result.Fail<string>("User role creation failed")
                .WithError(roleResult.Errors.Select(e => e.Description).FirstOrDefault());
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

            // creating the necessary claims
            List<Claim> authClaims = [
                    new (ClaimTypes.Name, userModel.UserName),
                    new (ClaimTypes.Email, userModel.Email),
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
}
