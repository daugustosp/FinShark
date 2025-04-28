using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("clientes")]
        public class Cliente
    {
       
        public int id { get; set; }
        public string Nome { get; set; }
        public string email { get; set; }
        public string Endereco { get; set; }
        public string Cpf { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}