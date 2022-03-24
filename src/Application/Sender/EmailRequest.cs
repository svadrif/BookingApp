using Application.DTOs.AppUserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sender
{
    public class EmailRequest
    {
        public GetAppUserDTO GetAppUserDTO { get; set; }
        public List<string> ToMail { get; set; } = new List<string> { "hotko.artem@mail.ru" };
        public string Subject { get; set; } = "BookingApp";
        public string Body { get; set; } = "<b>Test Mail</b><br>using <b>HTML</b>";
        public bool IsHtml { get; set; } = true;
    }
}
