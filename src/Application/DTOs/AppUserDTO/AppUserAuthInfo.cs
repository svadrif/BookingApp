using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AppUserDTO
{
    public class AppUserAuthInfo
    {
        public GetAppUserDTO GetAppUserDTO { get; set; }
        public string Token { get; set; }
    }
}
