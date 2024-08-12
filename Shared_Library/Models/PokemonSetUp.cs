using PokeApiNet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Library.Models
{
    public class PokemonSetUp
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int PokId { get; set; }
        public string PokemonName { get; set; }
        public string AbilityName { get; set; }
        public string ItemName { get; set; }
        public ICollection<PokemonMoveChoose> Moves { get; set; }
        public ICollection<TypeDto> Types { get; set; }

        public Team Team { get; set; }
        
    }
}
