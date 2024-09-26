using Company.Hossam.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.Hossam.PL.Helper
{
    public static class EmailSettings
    {
        public static void SentEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("hossamdev199@gmail.com","");
            Client.Send("hossamdev199@gmail.com", email.To, email.Subject, email.Body);
        }

    }
}
