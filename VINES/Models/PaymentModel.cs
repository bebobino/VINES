using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace VINES.Models
{
    public class PaymentModel
    {
        public List<Payment> FindAll()
        {
            return new List<Payment>
            {
                new Payment
                {
                    Id = "1",
                    Name = "Premium Subscription",
                    Price = 200,
                    Quantity = 1
                },
                
            };
        }
    }
}
