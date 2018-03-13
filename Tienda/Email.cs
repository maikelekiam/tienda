using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace Tienda
{
    public class Email
    {
        MailMessage m = new MailMessage();
        SmtpClient smtp = new SmtpClient();

        public bool enviarCorreo(string from, string password, string to, string mensaje)
        {
            try
            {
                m.From = new MailAddress(from);
                m.To.Add(new MailAddress(to));
                m.Subject = "Mensaje desde Sistema Informatico";
                m.Body = mensaje;
                m.IsBodyHtml = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential(from, password);
                smtp.EnableSsl = true;
                smtp.Send(m);

                

                

                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }
    }
}