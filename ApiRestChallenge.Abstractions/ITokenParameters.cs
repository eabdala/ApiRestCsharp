using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestChallenge.Abstractions
{
    public interface ITokenParameters
    {
        string Email { get; set; }
        string PasswordHash { get; set; }
        int Id { get; set; }
    }

}
