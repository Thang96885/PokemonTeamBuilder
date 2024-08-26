using Microsoft.EntityFrameworkCore;
using PokeApiNet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Shared_Library.Models
{
    public partial class PokemonMoveChoose
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Power { get; set; }
		[Required]
		public TypeDto Type { get; set; }
        public PokemonSetUp? PokemonSetUp { get; set; }
    }


    public partial class  PokemonMoveChoose
    {
        [NotMapped]
        public bool IsValid { get; set; }
        [NotMapped]
        public string LastMoveChooseType { get; set; }
        [NotMapped]
        public int? LastMovePower { get; set; }
    }
}
