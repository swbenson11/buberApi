using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.application.Common.Interfaces.Authentication;
using Microsoft.IdentityModel.Tokens;
using BuberDinner.application.Common.Interfaces.Services;

namespace BuberDinner.application.Common.Authentication;

public class JwtTokenGenerator: IJwtTokenGenerator {
   private readonly IDateTimeProvider _dateTimeProvider;

      public JwtTokenGenerator(IDateTimeProvider dateTimeProvider){
      _dateTimeProvider = dateTimeProvider;

   }

   public string GenerateToken(Guid userId, string firstName, string lastName){
      var signingCredentials = new SigningCredentials(
         new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes("super-secert-key")),
            SecurityAlgorithms.HmacSha256
      );

      var claims = new []
      {
         new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
         new Claim(JwtRegisteredClaimNames.GivenName, firstName),
         new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      };

      var securityToken = new JwtSecurityToken( 
         issuer: "BuberDinner",
         expires: _dateTimeProvider.UtcNow.AddDays(1),
         claims: claims,
         signingCredentials: signingCredentials    
      );

      return new JwtSecurityTokenHandler().WriteToken(securityToken);
   }
}