using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Threading.Tasks;
using Dental_Clinic.Models;

namespace Dental_Clinic.Core
{
    public class SendGridSendler
    {
            public SendGridSendler(IConfiguration configuration)
            {
                Environment.SetEnvironmentVariable("SENDGRID_API_KEY", configuration["Email:ApiKey"] );
            }

            public async void SendRegistrationEmail(EntryFormModel record)
            {
            string message = $"A new entry has been created with id : {record.ID} \n by : {record.UserName} \n to : {record.Doctor} \n at : {record.Procedure} procedure \n  at : {record.StartOfProcedure} \n  patient Phone Number : {record.UserPhoneNumber} \n";

                await Execute("New Record", message,"testsend.nik@inbox.ru");

            }

            public async Task Execute(string subject, string message, string toEmail)
            {
                var client = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_API_KEY"));
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("testsend.nik@inbox.ru", "Password Recovery"),
                    Subject = subject,
                    PlainTextContent = message,
                    HtmlContent = message
                };
                msg.AddTo(new EmailAddress(toEmail));

               

                msg.SetClickTracking(false, false);
                var response = await client.SendEmailAsync(msg);
            }
    }
}

