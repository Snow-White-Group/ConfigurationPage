using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace UIConfiguration.Services
{
    public class EmailService
    {
        public SmtpClient smtpClient;

        public EmailService()
        {
            smtpClient = new SmtpClient("smtp.web.de", 587);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailAddress"].ToString(), ConfigurationManager.AppSettings["EmailPW"].ToString());
        }
    }
}