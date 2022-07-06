using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using VINES.Models;
using VINES.Helper;

namespace VINES.Pages.Payments
{
    public class SuccessModel : PageModel
    {

        public IConfiguration configuration { get; }
        public SuccessModel(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string paymentId { get; set; }

        public string payerID { get; set; }

        public async Task OnGetAsync(string paymentId, string payerID)
        {

            this.paymentId = paymentId;
            this.payerID = payerID;
            Console.WriteLine(paymentId);
            if (paymentId != null && payerID != null)
            {

                var payPalAPI = new PayPalAPI(configuration);
                PayPalPaymentExcecutedResponse result = await payPalAPI.executedPayment(paymentId, payerID);

                Console.WriteLine("Transaction Details");
                Console.WriteLine("cart: " + result.cart);
                Console.WriteLine("payment method: " + result.payer.payment_method);
                Console.WriteLine("create_time: " + result.create_time.ToLongDateString());
                Console.WriteLine("id" + result.id);
                Console.WriteLine("intent: " + result.intent);
                Console.WriteLine("link 0 - href: " + result.links[0].href);
                Console.WriteLine("link 0 - method: " + result.links[0].method);
                Console.WriteLine("link 0 - rel: " + result.links[0].rel);
                Console.WriteLine("payer_info - first_name: " + result.payer.payer_info.first_name);
                Console.WriteLine("payer_info - last_name: " + result.payer.payer_info.last_name);
                Console.WriteLine("payer_info - email: " + result.payer.payer_info.email);
                Console.WriteLine("payer_info - street_address: " + result.payer.payer_info.shipping_address);
                Console.WriteLine("payer_info - country_code: " + result.payer.payer_info.country_code);
                Console.WriteLine("payer_info - payer_id: " + result.payer.payer_info.payer_id);
                Console.WriteLine("state: " + result.state);
            }








        }
    }
}