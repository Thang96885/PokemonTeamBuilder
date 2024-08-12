using Shared_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Library.Dto
{
	public class TeamCreateRequestDto
	{
        public string UserName { get; set; }
		public string TeamName { get; set; }
    }
}
