using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Library.Dto
{
	public class LoginResultDto
	{
        public string Message { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsSuccess { get; set; }
    }
}
