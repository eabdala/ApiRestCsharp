using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestChallenge.Abstractions
{
    public interface IEntity
    {
        int Id { get; set; }
        string Email { get; set; }

    }
}
