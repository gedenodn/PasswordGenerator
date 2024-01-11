using System;
using System.Linq;
using GeneratePasswordAPI.Services;

public class PasswordStrengthEvaluator
{
    public int EvaluatePassword(string password)
    {
        int score = 0;

        
        if (password.Length < 8)
        {
            score -= 2; 
        }
        else if (password.Length >= 8 && password.Length < 12)
        {
            score += 2;
        }
        else if (password.Length >= 12)
        {
            score += 4;
        }

        
        if (password.Any(char.IsUpper))
        {
            score += 2;
        }

        
        if (password.Any(char.IsLower))
        {
            score += 2;
        }

        
        if (password.Any(char.IsDigit))
        {
            score += 2;
        }

        
        if (password.Intersect(PasswordGenerator.GetSpecialChars()).Any())
        {
            score += 2;
        }

        
        if (password.Any(char.IsDigit) && password.Any(c => char.IsDigit(c) && int.Parse(c.ToString()) % 2 != 0))
        {
            score += 1;
        }

       

        return Math.Min(10, Math.Max(0, score)); 
    }

}
