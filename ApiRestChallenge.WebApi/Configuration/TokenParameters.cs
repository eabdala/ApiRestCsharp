using ApiRestChallenge.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestChallenge.WebApi.Configuration
{
    public class TokenParameters : ITokenParameters
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Id { get; set; }
    }
}
