using Mablo.Models;

namespace Mablo.Services
{
    public class ContactService
    {
        private readonly HttpClient _httpClient;

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Contact[]> GetContactsAsync()
        {
            return  await _httpClient.GetFromJsonAsync<Contact[]>("https://localhost:7148/api/Contacts");
        }

        public async Task<Contact> GetContactAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Contact>($"https://localhost:7148/api/Contacts/{id}");
        }

        public async Task<HttpResponseMessage> AddContactAsync(Contact contact)
        {
            return await _httpClient.PostAsJsonAsync("https://localhost:7148/api/Contacts/",contact);
        }

        public async Task<HttpResponseMessage> UpdateContactAsync(Guid id,Contact contact)
        {
            return await _httpClient.PutAsJsonAsync($"https://localhost:7148/api/Contacts/{id}", contact);
        }

        public async Task<HttpResponseMessage> DeleteContactAsync(Guid id)
        {
            return await _httpClient.DeleteAsync($"https://localhost:7148/api/Contacts/{id}");
        }

    }
}
