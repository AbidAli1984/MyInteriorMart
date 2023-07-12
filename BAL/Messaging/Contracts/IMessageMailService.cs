using BOL.SHARED;
using System.Threading.Tasks;
using Vonage.Messaging;

namespace BAL.Messaging.Contracts
{
    public interface IMessageMailService
    {
        void SendBoth(string mobile, string smsMessage, string email, string emailSubject, string emailMessage);
        Task<SendSmsResponse> SendSMS(string mobile, string smsMessage);

        void SendSMSInternational(string countryCode, string mobile, string smsMessage);
        void SendEmail(string emails, string subject, string message);
        public Task<Messages> MessagesDetailsAsync(string messageName);
    }
}
