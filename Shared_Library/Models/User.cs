
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Library.Models
{
	public class User : IdentityUser<int>
	{
		public string Name { get; set; } = "";
		public string? RefreshToken { get; set; }
		public DateTime? TimeExpireRefreshToken { get; set; }
		public ICollection<Team> Teams { get; set; }
    }
}
