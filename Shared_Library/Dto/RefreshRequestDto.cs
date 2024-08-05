using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Library.Dto
{
	public class RefreshRequestDto
	{
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
