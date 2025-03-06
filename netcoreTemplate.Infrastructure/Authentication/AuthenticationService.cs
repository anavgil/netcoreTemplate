using System.Collections.ObjectModel;
using System.Security.Claims;
using Application.Abstractions;
using Application.Users.Dtos;
using Domain.Identity.Model;
using FluentResults;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Infrastructure.Authentication;

public class AuthenticationService(SignInManager<User> signInManager, ITokenService tokenService) : IAuthenticationService
{
    public async Task<IResult<string>> CreateUserAsync(string name, string surname, string user, string pass, string email)
    {
        Collection<Claim> claimCollection = [];
        try
        {
            var identityResult = await signInManager.UserManager.CreateAsync(new User { UserName = user, FirstName = name, LastName = surname, Email = email }, pass);

            if (!identityResult.Succeeded)
            {
                Result.Fail<string>("User creation failed")
                    .WithError(identityResult.Errors.Select(e => e.Description).FirstOrDefault());
            }

            claimCollection.Add(new Claim(Claims.Full, Claims.Full));
            claimCollection.Add(new Claim(ClaimTypes.Country, "SPAIN"));

            var userEntity = await signInManager.UserManager.FindByNameAsync(user);
            var roleTask = signInManager.UserManager.AddToRoleAsync(userEntity, Roles.Admin);
            var claimTask = signInManager.UserManager.AddClaimsAsync(userEntity, claimCollection);
            var result = await Task.WhenAll(roleTask, claimTask);

            return result.All(r => r.Succeeded) ?
                    Result.Ok<string>(userEntity.Id) :
                    Result.Fail<string>("User role/claims creation failed")
                .WithError(result.Where(x=> !x.Succeeded).Select(x=> x.Errors).Select(e => e.FirstOrDefault().Description).FirstOrDefault());
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
            var loginResult = await signInManager.PasswordSignInAsync(user, pass, true, false);
            if (!loginResult.Succeeded)
            {
                return Result.Fail<LoginResponseDto>("Unauthorized Access");
            }

            var userModel = await signInManager.UserManager.FindByNameAsync(user);

            // creating the necessary claims
            List<Claim> authClaims = [
                    new (ClaimTypes.Name, userModel.FirstName),
                    new (ClaimTypes.Surname, userModel.LastName),
                    new (ClaimTypes.Email, userModel.Email),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // unique id for token
        ];

            var userRolesTask = signInManager.UserManager.GetRolesAsync(userModel);
            var userClaimsTask = signInManager.UserManager.GetClaimsAsync(userModel);

            var (userRoles, userClaims) = await TaskExtension.WhenAllExt(userRolesTask,userClaimsTask);

            // adding roles to the claims. So that we can get the user role from the token.
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            if(userClaims.Any())
                authClaims.AddRange(userClaims);

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
