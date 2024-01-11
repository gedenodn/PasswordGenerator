using GeneratePasswordAPI.Models;
using GeneratePasswordAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeneratePasswordAPI.Controllers
{
    [ApiController]
    [Route("api/password")]
    public class GeneratePasswordController : ControllerBase
    {
        private readonly PasswordGenerator _passwordGenerator;
        private readonly PasswordStrengthEvaluator _passwordStrengthEvaluator;

        public GeneratePasswordController(PasswordGenerator passwordGenerator, PasswordStrengthEvaluator passwordStrengthEvaluator)
        {
            _passwordGenerator = passwordGenerator;
            _passwordStrengthEvaluator = passwordStrengthEvaluator;
        }

        [HttpGet]
        public ActionResult<PasswordModel> GeneratePassword(int length = 12, bool includeSpecialChars = true)
        {
            string generatedPassword = PasswordGenerator.GeneratePassword(length, includeSpecialChars);
            int passwordScore = _passwordStrengthEvaluator.EvaluatePassword(generatedPassword);

            var passwordModel = new PasswordModel
            {
                GeneratedPassword = generatedPassword,
                PasswordScore = passwordScore.ToString()
            };

            return Ok(passwordModel);
        }
    }
}
