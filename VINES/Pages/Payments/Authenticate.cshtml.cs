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
using System.Security.Claims;

namespace VINES.Pages.Payments
{
    public class SuccessModel : PageModel
    {

        public IConfiguration configuration { get; }
        private DatabaseContext db;
        public SuccessModel(IConfiguration _configuration, DatabaseContext _db)
        {
            configuration = _configuration;
            db = _db;
        }
        public string paymentId { get; set; }

        public string payerID { get; set; }




        public async Task<RedirectResult> OnGetAsync(string paymentId, string payerID)
        {

            this.paymentId = paymentId;
            this.payerID = payerID;
            try
            {
                var name = User.FindFirstValue(ClaimTypes.Role);
                Debug.WriteLine(paymentId);
                Debug.WriteLine(name);
                if (!name.Equals("Patient"))
                {
                    return Redirect("/Index");
                }
                if (paymentId != null && payerID != null)
                {
                    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var uid = int.Parse(id);

                    var payPalAPI = new PayPalAPI(configuration);
                    PayPalPaymentExcecutedResponse result = await payPalAPI.executedPayment(paymentId, payerID);
                    Debug.WriteLine(payPalAPI.paypalID);
                    Debug.WriteLine("Transaction Details");
                    Debug.WriteLine("cart: " + result.cart);
                    Debug.WriteLine("payment method: " + result.payer.payment_method);
                    Debug.WriteLine("create_time: " + result.create_time.ToLongDateString());
                    Debug.WriteLine("id: " + result.id);
                    Debug.WriteLine("intent: " + result.intent);
                    Debug.WriteLine("link 0 - href: " + result.links[0].href);
                    Debug.WriteLine("link 0 - method: " + result.links[0].method);
                    Debug.WriteLine("link 0 - rel: " + result.links[0].rel);
                    Debug.WriteLine("payer_info - first_name: " + result.payer.payer_info.first_name);
                    Debug.WriteLine("payer_info - last_name: " + result.payer.payer_info.last_name);
                    Debug.WriteLine("payer_info - email: " + result.payer.payer_info.email);
                    Debug.WriteLine("payer_info - street_address: " + result.payer.payer_info.shipping_address);
                    Debug.WriteLine("payer_info - country_code: " + result.payer.payer_info.country_code);
                    Debug.WriteLine("payer_info - payer_id: " + result.payer.payer_info.payer_id);
                    Debug.WriteLine("amount:" + result.transactions[0].amount.total);
                    Debug.WriteLine("state: " + result.state);
                    var total = decimal.Parse(result.transactions[0].amount.total);
                    if (total == 200)
                    {
                        Debug.WriteLine("User Monthly");
                        var patient = db.Patients.Where(p => p.userID == uid).FirstOrDefault();
                        patient.isSubscribed = true;
                        patient.showAds = false;
                        patient.subStart = DateTime.UtcNow;
                        var future = DateTime.UtcNow.AddMonths(1);
                        patient.subEnd = future;
                        await db.SaveChangesAsync();
                    }
                    else if (total == 1299)
                    {
                        Debug.WriteLine("User Yearly");
                    }

                }

                return Redirect("/Index");
            } catch(Exception e)
            {
                return Redirect("https://www.facebook.com");
            }
            
            






        }
    }
}