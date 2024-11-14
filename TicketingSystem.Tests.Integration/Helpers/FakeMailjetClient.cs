using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client.TransactionalEmails.Response;

namespace TicketingSystem.Tests.Integration.Helpers
{
    public class FakeMailjetClient : IMailjetClient
    {
        public List<TransactionalEmail> Emails { get; set; } = [];
        public FakeMailjetClient(HttpClient httpClient)
        {
        }

        public Task<MailjetResponse> DeleteAsync(MailjetRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<MailjetResponse> GetAsync(MailjetRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<MailjetResponse> PostAsync(MailjetRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<MailjetResponse> PutAsync(MailjetRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionalEmailResponse> SendTransactionalEmailAsync(TransactionalEmail transactionalEmail, bool isSandboxMode = false, bool advanceErrorHandling = true)
        {
            await Task.Delay(0);
            Emails.Add(transactionalEmail);
            return new TransactionalEmailResponse();
        }

        public Task<TransactionalEmailResponse> SendTransactionalEmailsAsync(IEnumerable<TransactionalEmail> transactionalEmails, bool isSandboxMode = false, bool advanceErrorHandling = true)
        {
            throw new NotImplementedException();
        }
    }
}
