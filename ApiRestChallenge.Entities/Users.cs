using System;

namespace ApiRestChallenge.Entities
{
    public class Users : Entity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        
        public string Contraseña { get; set; }

    }
}
