using System.Collections.Generic;

namespace VINES.Services
{
    public class MailClass
    {
        public string FromMailId { get; set; } = "christapsss@gmail.com";
        public string FromMailPassword { get; set; } = "mvsmnlcitaiqytfi";
        public List<string> ToMailIds { get; set; } = new List<string>();

        public string Subject { get; set; } = "";
        public string Body { get; set; } = "";
        public bool IsBodyHtml { get; set; } = true;
        public List<string> Attachments { get; set; } = new List<string>();
    }
}
