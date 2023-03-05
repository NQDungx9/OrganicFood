using System;
using System.Collections.Generic;
using System.Text;

namespace OrganicFoodMVC.Utility
{
    public class EmailOptions
    {
        public string ServerMail { get; set; }
        public int PortServerMail { get; set; }
        public string UsernameMail { get; set; }
        public string Password { get; set; }
    }
}
