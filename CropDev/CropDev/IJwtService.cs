using System;

namespace CropDev.JwtInterface
{
    public interface IJwtService
    {
        string GenerateToken(string userId);
        bool ValidateToken(string token);
        string GetUserIdFromToken(string token);
    }
}
