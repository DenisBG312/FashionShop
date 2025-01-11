using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services.Data
{
    public class EcontDeliveryService
    {
        private readonly HttpClient _httpClient;

        public EcontDeliveryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetShippingCostUrl(object shippingDetails)
        {
            // Build the URL with the parameters and return it
            var baseUrl = "https://delivery.demo.econt.com/customer_info.php"; // Example URL
            var queryString = new Dictionary<string, string>
            {
                { "id_shop", "8659979" },
                { "order_total", "23.99"},
                { "order_currency", "28.99" },
            };

            var fullUrl = baseUrl + "?" + string.Join("&", queryString.Select(p => $"{p.Key}={p.Value}"));
            return fullUrl;
        }

        public async Task<bool> FinalizeOrderWithEcont(int orderId, string econtAddressId)
        {
            // Call the Econt API to finalize the order
            var orderData = new
            {
                OrderId = orderId,
                EcontAddressId = econtAddressId
            };

            var response = await _httpClient.PostAsJsonAsync("https://delivery.econt.com/services/OrdersService.updateOrder.json", orderData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> TriggerDeliveryAsync(int orderId)
        {
            var orderDetails = new
            {
                OrderId = orderId,
                ShippingAddress = "Example address",
                Products = new[] { new { ProductId = 1, Quantity = 2 }, new { ProductId = 2, Quantity = 1 } }
            };

            var response = await _httpClient.PostAsJsonAsync("https://delivery.econt.com/api/trigger_delivery", orderDetails);

            return response.IsSuccessStatusCode;
        }
    }
}
