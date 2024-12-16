using System;
using System.Net;
using System.Net.Mail;

public class EmailSender
{
    public static void SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            // Настройки SMTP-сервера
            string smtpServer = "smtp.yourmailserver.com"; // Укажите SMTP сервер
            int smtpPort = 587; // Порт для SMTP, обычно 587 для TLS
            string fromEmail = "youremail@example.com"; // Ваш email
            string password = "yourpassword"; // Ваш пароль

            // Создание SMTP клиента
            SmtpClient smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true // Использование SSL
            };

            // Создание сообщения
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = false // Если письмо в HTML, установите true
            };

            mailMessage.To.Add(toEmail); // Добавление получателя

            // Отправка письма
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email successfully sent.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
        }
    }
}

class Program
{
    static void Main()
    {
        string recipient = "sdarendthefood.com";
        string subject = "Test Email";
        string body = "This is a test email.";

        EmailSender.SendEmail(recipient, subject, body);
    }
}
