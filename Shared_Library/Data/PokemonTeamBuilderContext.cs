
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Library.Data
{
    public class  PokemonTeamBuilderContext : IdentityDbContext<User, Role, int>
    {
        public PokemonTeamBuilderContext(DbContextOptions<PokemonTeamBuilderContext> options) : base(options)
        {

        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

            builder.Entity<Team>().Property(t => t.TeamName).HasDefaultValue("New Team");

            builder.Entity<TypeDto>(t =>
            {
                t.HasIndex(t => t.Name).IsUnique();
            });
		}

		public DbSet<PokemonSetUp> PokemonSetUps { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<PokemonMoveChoose> Moves { get; set; }
        public DbSet<TypeDto> Types { get; set; }
    }
}
