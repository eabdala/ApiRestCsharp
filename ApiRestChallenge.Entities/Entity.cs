using ApiRestChallenge.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestChallenge.Entities
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
