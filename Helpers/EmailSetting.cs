using Re.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Re.PL.Helpers
{
    public class EmailSetting
    {

        public static void SendEmail (Email email)
        {

            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("fryaljaber12@gmail.com", "jflo kkri ytam tlzl");
            client.Send("fryaljaber12@gmail.com", email.Recivers, email.Subject, email.Body);
        }
    }
}
