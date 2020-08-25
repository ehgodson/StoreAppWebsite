using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StoreApp.Services
{
    public interface IEmailSender
    {
        Task SendProspectMessage(ProspectRequest prospect, bool recordSaved, string errorMsg);
        Task SendContactMessage(string name, string email, string mobile, string subject, string message);
        Task SendEmailAsync(MailMessage mailMessage);
    }
    public class EmailSender : IEmailSender
    {
        private MailAddress EmailAddress
        {
            get
            {
                return new MailAddress("storeapp@clearwox.com", "Clearwox Systems");
            }
        }

        private string GetContactHtmlMessage(string name, string email, string mobile, string subject, string message)
        {
            return
                "<div>" +
                "   <p>Hello,</p>" +
                "   <h4>" + subject + "</h2>" +
                "   <p>" + message + "</p>" +
                "   <hr />" +
                "   <p>" + name + ",<br/>" + email + ",<br/>" + mobile + "</p>" +
                "</div>";
        }

        private string GetProspectHtmlMessage(ProspectRequest prospect, bool recordSaved, string errorMsg)
        {
            return
                $"<div>" +
                $"   <p>Hello,</p>" +
                $"   <p>Here's a new prospect who would like to learn more about StoreApp. Can you please contact them immediately?</p>" +
                $"   <h4>Prospect details:</h2>" +
                $"   <p>" +
                $"       <strong>Name</strong> {prospect.name}<br /></p>" +
                $"       <strong>Email address:</strong> {prospect.email}<br /></p>" +
                $"       <strong>Mobile no:</strong> {prospect.phone}<br /></p>" +
                $"       <strong>Company name:</strong> {prospect.company}<br /></p>" +
                $"       <strong>Location:</strong> {prospect.location}<br /></p>" +
                $"   </p>" +
                $"   <p>Thank you!</p>" +
                $"   <p>Record saved: {(recordSaved ? "Yes" : "No")} {(string.IsNullOrEmpty(errorMsg) ? string.Empty : errorMsg)}</p>" +
                $"</div>";
        }

        public Task SendProspectMessage(ProspectRequest prospect, bool recordSaved, string errorMsg)
        {
            MailMessage msg = new MailMessage
            {
                From = EmailAddress //new MailAddress(model.Email, model.Name)
            };

            msg.To.Add(new MailAddress("hello@clearwox.com", "Clearwox Systems"));
            msg.To.Add(new MailAddress("storeapp@clearwox.com", "StoreApp"));
            msg.To.Add(new MailAddress("godwin@clearwox.com", "Godwin Ehichoya"));
            msg.To.Add(new MailAddress("sunday@clearwox.com", "Sunday Ehichoya"));

            msg.ReplyToList.Add(new MailAddress(prospect.email, prospect.name));

            msg.Subject = "Tell me more about StoreApp";
            msg.Body = GetProspectHtmlMessage(prospect, recordSaved, errorMsg);
            msg.IsBodyHtml = true;

            return SendEmailAsync(msg);
        }

        public Task SendContactMessage(string name, string email, string mobile, string subject, string message)
        {
            MailMessage msg = new MailMessage
            {
                From = EmailAddress //new MailAddress(model.Email, model.Name)
            };

            msg.To.Add(new MailAddress("hello@clearwox.com", "Clearwox Systems"));
            msg.To.Add(new MailAddress("storeapp@clearwox.com", "StoreApp"));
            msg.To.Add(new MailAddress("godwin@clearwox.com", "Godwin Ehichoya"));
            msg.To.Add(new MailAddress("sunday@clearwox.com", "Sunday Ehichoya"));

            msg.ReplyToList.Add(new MailAddress(email, name));

            msg.Subject = "StoreApp Website: " + subject;
            msg.Body = GetContactHtmlMessage(name, email, mobile, subject, message);
            msg.IsBodyHtml = true;

            return SendEmailAsync(msg);
        }

        public Task SendAuthMail(string email, string subject, string message)
        {
            MailMessage msg = new MailMessage
            {
                From = EmailAddress //new MailAddress(model.Email, model.Name)
            };

            msg.To.Add(new MailAddress("hello@clearwox.com", "Clearwox Systems"));
            msg.To.Add(new MailAddress("godwin@clearwox.com", "Godwin Ehichoya"));
            msg.To.Add(new MailAddress("sunday@clearwox.com", "Sunday Ehichoya"));

            msg.ReplyToList.Add(new MailAddress(email));

            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            return SendEmailAsync(msg);
        }

        // ================================ >>

        public Task SendEmailAsync(MailMessage message)
        {
            SmtpClient smtp = new SmtpClient("mail.clearwox.com")
            {
                Credentials = new NetworkCredential(EmailAddress.Address, "@storeapp")
            };

            //smtp.EnableSsl = true;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress("storeapp@clearwox.com", "StoreApp"));
            smtp.Send(message);

            return Task.CompletedTask;
        }
    }
}
