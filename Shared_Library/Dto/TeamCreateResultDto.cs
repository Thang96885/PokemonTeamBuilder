using Shared_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Library.Dto
{
	public class TeamCreateResultDto
	{
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Team Team { get; set; }
    }
}
