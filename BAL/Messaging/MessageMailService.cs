using BOL.SHARED;
using DAL.LISTING;
using DAL.SHARED;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Web;
using DAL.Models;
using BAL.Messaging.Contracts;
using RestSharp;

namespace BAL.Messaging
{
    public class MessageMailService : IMessageMailService
    {
        private readonly ListingDbContext listingManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SharedDbContext sharedContext;

        public MessageMailService(ListingDbContext listingManager, UserManager<ApplicationUser> userManager, SharedDbContext sharedContext)
        {
            this.listingManager = listingManager;
            this.userManager = userManager;
            this.sharedContext = sharedContext;
        }

        public async Task<Messages> MessagesDetailsAsync(string messageName)
        {
            var message = await sharedContext.Messages.Where(m => m.Name == messageName).FirstOrDefaultAsync();
            return message;
        }

        public void SendBoth(string mobile, string smsMessage, string email, string emailSubject, string emailMessage)
        {

            // Shafi: Send SMS
            string route = "1";
            string userName = "myinteriormart";
            string apiKey = "8zrLdsv8GeTatccl32Er";
            string sender = "MyINTR";

            StringBuilder shafi = new StringBuilder();
            shafi.Append("http://5.9.77.40/api/http");
            shafi.AppendFormat("?route={0}", route);
            shafi.AppendFormat("&username={0}", userName);
            shafi.AppendFormat("&apikey={0}", apiKey);
            shafi.AppendFormat("&sender={0}", sender);
            shafi.AppendFormat("&mobile={0}", mobile);
            shafi.AppendFormat("&message={0}", smsMessage);

            //try
            //{

            //    //Call Send SMS API
            //    string sendSMSUri = shafi.ToString();

            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //    StreamReader stream = new StreamReader(response.GetResponseStream());
            //    string result = stream.ReadToEnd();
            //    stream.Close();
            //}
            //catch (SystemException ex)
            //{
            //    ex.Message.ToString();
            //}
            // End:

            // Shafi: Send Email
            //SendEmail(email, emailSubject, emailMessage);
            // End:
        }

        public async void SendSMS(string mobile, string smsMessage)
        {
            // Begin: Send Messaging Through Msg91.com
            string AuthKey = "126426AlvG4cN2Kc57e8e865";
            string SenderId = "UMARZO";
            string RealMessage = HttpUtility.UrlEncode(smsMessage);

            var options = new RestClientOptions("https://control.msg91.com/api/v5/otp?template_id=64a074f6d6fc05502c0f84c2&mobile=9833505109");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("authkey", "400157AC73pSfg64a06dccP1");
            request.AddJsonBody("{\"OTP\":\"631248\"}", false);
            var response = await client.PostAsync(request);

            string cont = response.Content;






            //// Send Message To Tempo Service Providers
            //StringBuilder sbPostData = new StringBuilder();
            //sbPostData.AppendFormat("authkey={0}", AuthKey);
            //sbPostData.AppendFormat("&mobiles={0}", mobile);
            //sbPostData.AppendFormat("&message={0}", RealMessage);
            //sbPostData.AppendFormat("&sender={0}", SenderId);
            //sbPostData.AppendFormat("&route={0}", "4");

            ////Call Send SMS API
            //string sendSMSUri = "https://control.msg91.com/api/sendhttp.php";
            ////Create HTTPWebrequest
            //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            ////Prepare and Add URL Encoded data
            //UTF8Encoding encoding = new UTF8Encoding();
            //byte[] data = encoding.GetBytes(sbPostData.ToString());
            //Specify post method
            //httpWReq.Method = "POST";
            //httpWReq.ContentType = "application/x-www-form-urlencoded";
            //httpWReq.ContentLength = data.Length;
            //using (Stream stream = httpWReq.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}
            //Get the response
            //HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            //StreamReader reader = new StreamReader(response.GetResponseStream());
            //string responseString = reader.ReadToEnd();

            //Close the response
            //reader.Close();
            //response.Close();


            // Shafi: Send SMS
            //string route = "1";
            //string userName = "myinteriormart";
            //string apiKey = "8zrLdsv8GeTatccl32Er";
            //string sender = "MyINTR";

            //StringBuilder shafi = new StringBuilder();
            //shafi.Append("http://5.9.77.40/api/http");
            //shafi.AppendFormat("?route={0}", route);
            //shafi.AppendFormat("&username={0}", userName);
            //shafi.AppendFormat("&apikey={0}", apiKey);
            //shafi.AppendFormat("&sender={0}", sender);
            //shafi.AppendFormat("&mobile={0}", mobile);
            //shafi.AppendFormat("&message={0}", smsMessage);

            //try
            //{
            //    //Call Send SMS API
            //    string sendSMSUri = shafi.ToString();

            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //    StreamReader stream = new StreamReader(response.GetResponseStream());
            //    string result = stream.ReadToEnd();
            //    stream.Close();
            //}
            //catch (SystemException ex)
            //{
            //    ex.Message.ToString();
            //}
            // End:
        }

        public  void SendSMSInternational(string countryCode, string mobile, string smsMessage)
        {
            // Begin: Send Messaging Through Msg91.com
            string AuthKey = "126426AlvG4cN2Kc57e8e865";
            string SenderId = "UMARZO";
            string RealMessage = HttpUtility.UrlEncode(smsMessage);

            // Send Message To Tempo Service Providers
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("authkey={0}", AuthKey);
            sbPostData.AppendFormat("&mobiles={0}", mobile);
            sbPostData.AppendFormat("&message={0}", RealMessage);
            sbPostData.AppendFormat("&sender={0}", SenderId);
            sbPostData.AppendFormat("&route={0}", "4");
            sbPostData.AppendFormat("&country={0}", countryCode);

            //Call Send SMS API
            string sendSMSUri = "https://control.msg91.com/api/sendhttp.php";
            //Create HTTPWebrequest
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            //Prepare and Add URL Encoded data
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(sbPostData.ToString());
            //Specify post method
            //httpWReq.Method = "POST";
            //httpWReq.ContentType = "application/x-www-form-urlencoded";
            //httpWReq.ContentLength = data.Length;
            //using (Stream stream = httpWReq.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}
            //Get the response
            //HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            //StreamReader reader = new StreamReader(response.GetResponseStream());
            //string responseString = reader.ReadToEnd();

            ////Close the response
            //reader.Close();
            //response.Close();
        }

        public void SendEmail(string emails, string subject, string messageBody)
        {
            // Shafi: Smtp Client
            MailMessage msg = new MailMessage();
            string UserName = "shafi@umarzone.com";
            string Password = "xsmtpsib-bace29d96f63b4ca28dd8b1f4b7be70e4e0d8eb96d99da02fa85c01321c96a8a-JCOHBwRyTX2AY7Ds";
            // SMTP Setup
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-relay.sendinblue.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(UserName, Password);
            smtp.EnableSsl = false;

            // Send Email
            //msg.To.Add(emails);
            //msg.Bcc.Add("shafi@umarzone.com" + "," + "myinteriormart@gmail.com");
            //msg.From = new MailAddress("shafi@umarzone.com");
            //msg.Subject = subject;
            //msg.Body = messageBody;
            //msg.IsBodyHtml = true;
            //smtp.Send(msg);
            // End: Send Email

            // Shafi: Sendgrid SMTP configuration ----------------------------------------
            //// Shafi: Smtp Client
            //MailMessage msg = new MailMessage();
            //string UserName = "apikey";
            //string Password = "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";
            //// SMTP Setup
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.sendgrid.net";
            //smtp.Port = 25;
            //smtp.Credentials = new NetworkCredential(UserName, Password);
            //smtp.EnableSsl = false;

            //// Send Email
            //msg.To.Add(emails);
            //msg.Bcc.Add("shafi@umarzone.com" + "," + "myinteriormart@gmail.com");
            //msg.From = new MailAddress("shafi@umarzone.com");
            //msg.Subject = subject;
            //msg.Body = messageBody;
            //msg.IsBodyHtml = true;
            //smtp.Send(msg);
            //// End: Send Email
            // End: -----------------------------------------------------------------------

        }
    }
}
