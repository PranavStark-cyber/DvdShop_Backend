using System.Text.Json;
using System.Text;
using DvdShop.Interface.IServices;

namespace DvdShop.Services
{
    public class WhatsAppServices: IWhatsAppServices
    {
        public async Task SendWhatsAppNotification(string customerPhoneNumber, string message)
        {
            // WhatsApp Cloud API credentials
            const string accessToken = "EAANWI2ErdZAoBO3RmgE9pLXxFbgpOy1UiKOwqZBRGswSlPl3fe9Dx1vqxqK5QaWW3rwHHsiXWw8nXFdeHJRJRs5rGDqmMfaZBZB9I8e00UGGFX2M2lbvtI68EJA7K5HWmLZCJFVLEPOTP3kgqTATIuYpumrPAuzBoO6oZA0EDoUvr7Phxnq4ad3sQWcZB4jeiAxHM0RVP1oi2ZC2BQOJTV3xxRCNa0vHUmIZBVWsW";
            const string businessId = "534810439705657"; // Replace with your WhatsApp Business ID

            using var client = new HttpClient();
            var url = $"https://graph.facebook.com/v21.0/{businessId}/messages";

            // Payload for sending the WhatsApp message
            var payload = new
            {
                messaging_product = "whatsapp",
                to = customerPhoneNumber,
                type = "text",
                text = new { body = message }
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Add the Authorization header with the access token
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            try
            {
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("WhatsApp message sent successfully.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to send WhatsApp message: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending WhatsApp message: {ex.Message}");
            }
        }
    }
}
