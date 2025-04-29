using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiMvp.Model;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace ApiMvp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LogisticMvpContext _context;

        public UsersController(LogisticMvpContext context)
        {
            _context = context;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostUser(User user)
        {
            user.Password = HashMethod(user.Password);
            user.Name = HashMethod(user.Name);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if (await _context.Users.ContainsAsync(user)) return Ok();
            else return NoContent();
        }

        private string HashMethod(string password)
        {
            var bytes = Encoding.ASCII.GetBytes(password);
            StringBuilder result = new StringBuilder();
            using (var md5 = MD5.Create())
            using (var ms = new MemoryStream(bytes))
            {
                var hash = md5.ComputeHash(ms);
                foreach (var b in hash)
                    result.Append(b.ToString("x2"));
            }
            return result.ToString();
        }

        [HttpGet ("SignIn/{username}/{hashPassword}")]
        public async Task<ActionResult<UserDTO>> SignIn(string username, string hashPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
            if (user == null) return NotFound();
            if (hashPassword != user.Password) return Unauthorized();
            var claims = new List<Claim> {
                //Кладём Id (если нужно)
                new Claim(ClaimValueTypes.Integer32, user.Id.ToString()),
                //Кладём роль
                new Claim(ClaimTypes.Name, user.Name)
            };
            var jwt = new JwtSecurityToken(
                   issuer: AuthOptions.ISSUER,
                   audience: AuthOptions.AUDIENCE,
                   //кладём полезную нагрузку
                   claims: claims,
                   //устанавливаем время жизни токена 30 минуты
                   expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
                   signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            UserDTO result = new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Token = token
            };
            return Ok(result);
        }
    }
}
