using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestChallenge.DataAccess;
using ApiRestChallenge.Entities;
using ApiRestChallenge.Application;
using ApiRestChallenge.WebApi.DTOs;
using System.Security.Cryptography;

namespace ApiRestChallenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IApplication<Users> _context;

        public UsersController(IApplication<Users> context)
        {
            _context = context; 
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            return Ok(await _context.GetAllAsync());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (id == 0) return NotFound();

            var Users = await _context.GetByIdAsync(id);
            return Ok(Users);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, UserDTO users)
        {
            if (id == 0 || users == null) return NotFound();

            var tmp = _context.GetById(id);

            if (tmp != null)
            {
                tmp.Id = id;
                tmp.Nombre = users.Nombre;
                tmp.Apellido = users.Apellido;
                tmp.Edad = users.Edad;
                tmp.Email = users.Email;
                
            }
            await _context.SaveAsync(tmp);
            return Ok(tmp);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async  Task<IActionResult> Save(UserDTO dto)
        {
            var f = new Users()
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Edad = dto.Edad,
                Email = dto.Email,
                Contraseña = HashPassword(dto.Contraseña)
            };
            await _context.SaveAsync(f);
            return Ok(f);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public  IActionResult DeleteUsers(int id)
        {
            if (id == 0) return NotFound();

            _context.DeleteAsync(id);
            return Ok();
        }


        #region "Hasheo"

     
        private static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private static bool ByteArraysEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
        #endregion

    }
}
