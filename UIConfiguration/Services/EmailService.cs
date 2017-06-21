using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using UIConfiguration.Models;

namespace UIConfiguration.Services
{
    public class EmailService
    {
        public SmtpClient smtpClient;

        public EmailService()
        {
            smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailAddress"].ToString(), ConfigurationManager.AppSettings["EmailPW"].ToString());
            smtpClient.Host = "smtp.1and1.com";
        }
    }
}