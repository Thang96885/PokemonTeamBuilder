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
    public partial class PokemonSetUp
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int PokId { get; set; }
        public string PokemonName { get; set; }
        public string AbilityName { get; set; }
        public string ItemName { get; set; }
        public IList<PokemonMoveChoose> Moves { get; set; }
        public IList<TypeDto> Types { get; set; }

        public Team Team { get; set; }
    }

    public partial class PokemonSetUp
    {
        public string PicUrl { get; set; }

        public PokemonSetUp()
		{
			Moves = new List<PokemonMoveChoose>();
            for(int i = 0; i < 4; i++)
            {
                Moves.Add(new PokemonMoveChoose());
            }    
			Types = new List<TypeDto>();
		}
    }
}
