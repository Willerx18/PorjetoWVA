using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_projeto.EmailServer
{
    public abstract class MasterServerEmail
    {

        private SmtpClient smtpClient;
        protected string senderEmal { get; set; }
        protected string senha { get; set; }
        protected string host { get; set; }
        protected int port { get; set; }
        protected bool ssl { get; set; }

        protected void inicializarSmtCLient()
        {
            smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Timeout = 50000;
            smtpClient.Credentials = new NetworkCredential(senderEmal,senha);
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl= ssl;

        }
        protected void cargarDados(string setor)
        {
          
            DataTable dt = Banco.ObterTodosOnde("Emails", "Setor", "'"+setor+"'");
            senderEmal = dt.Rows[0].Field<string>("Email");
            senha = dt.Rows[0].Field<string>("Senha");
            host = dt.Rows[0].Field<string>("host");
            port = (int)dt.Rows[0].Field<Int64>("Port");
            ssl = dt.Rows[0].Field<bool>("ssl");
            inicializarSmtCLient();
        }

        public void EnviarEmail(string asunto, string cuerpo, List<string> destinatario)
        {
            var mailmessage= new MailMessage();
            try
            {
                mailmessage.From= new MailAddress(senderEmal);
                foreach (string mail in destinatario)
                {
                    mailmessage.To.Add(mail);
                }

                mailmessage.Subject = asunto;
                mailmessage.Body = cuerpo;
                mailmessage.Priority = MailPriority.Normal;
                smtpClient.Send(mailmessage);
            }
            catch (Exception ex){ }
            finally
            {
                mailmessage.Dispose();
                smtpClient.Dispose();
            }

        }
        
    }
}
