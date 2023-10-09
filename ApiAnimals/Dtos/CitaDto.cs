using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnimals.Dtos
{
    public class CitaDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int IdCliente { get; set; }
        public int IdMascota { get; set; }
        public int IdServicio { get; set; }
    }
}