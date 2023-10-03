using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class Cliente : BaseEntity
{
    [Required]
    public string Nombre { get; set; }

    [Required]
    public string Apellido { get; set; }

    [Required]
    public string Email { get; set; }

    public ClienteDireccion ClientesDireccion { get; set; }
    public ICollection<ClienteTelefono> ClientesTelefonos { get; set; }
    public ICollection<Mascota> Mascotas { get; set; }
    public ICollection<Cita> Citas { get; set; }
}
