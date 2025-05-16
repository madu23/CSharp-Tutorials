using System.Security.Claims;

namespace RtlEmployeeApi.Models.service
{
    public interface ITokenService
    {
        string GenerateToken(string username, string role = "User");
    }
}
