using PokeApiNet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Library.Models
{
    public class PokemonMoveChoose
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
		[Required]
		public string Url { get; set; }
        public int? Power { get; set; }
		[Required]
		public TypeDto Type { get; set; }
    }
}
