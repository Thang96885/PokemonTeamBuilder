using Shared_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Library.Dto
{
	public class AddPokemonSetupRequest
	{
        public int TeamId { get; set; }
        public IEnumerable<PokemonSetUp> PokemonSetupList { get; set; }
    }
}
