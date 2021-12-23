using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyIslamWebApp.Filter;
using NLog;
using Stripe;

namespace MyIslamWebApp.Controllers
{
    [RoutePrefix("api/Payment")]
    public class PaymentController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        //private readonly IPaymentService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public PaymentController()
        {
            //_service = new PaymentService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods        

        /// <summary>
        /// Make new Payment
        /// </summary>
        /// <remarks>
        /// Make new Payment
        /// </remarks>
        /// <param name="stripeEmail">Stripe Email</param>
        /// <param name="stripeToken">Stripe Token</param>
        /// <returns></returns>
        /// <response code="201">Payment has been done.</response>
        //[Authorize]
        [HttpPost]
        [ActionName("MakePayment") Route("MakePayment")]
        [ModelValidator]
        public IHttpActionResult MakePayment(string stripeEmail, string stripeToken)
        {
            try
            {
                //Create Card Object to create Token  
                CreditCardOptions card = new CreditCardOptions();
                //card.Name = tParams.CardOwnerFirstName + " " + tParams.CardOwnerLastName;
                //card.Number = tParams.CardNumber;
                //card.ExpYear = tParams.ExpirationYear;
                //card.ExpMonth = tParams.ExpirationMonth;
                //card.Cvc = tParams.CVV2;
                card.Name = "Amit" + " " + "Singh";
                card.Number = "4000000760000002";
                card.ExpYear = 20;
                card.ExpMonth = 10;
                card.Cvc = "123";

                //Assign Card to Token Object and create Token  
                TokenCreateOptions token = new TokenCreateOptions();
                token.Card = card;
                TokenService serviceToken = new TokenService();
                Token newToken = serviceToken.Create(token);

                var customers = new CustomerService();
                var customer = customers.Create(new CustomerCreateOptions
                {
                    Email = stripeEmail,
                    SourceToken = newToken.Id.ToString()
                });

                //Create Customer Object and Register it on Stripe
                Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions();
                myCustomer.Email = stripeEmail;
                myCustomer.SourceToken = newToken.Id;
                var customerService = new Stripe.CustomerService();
                customerService.ApiKey = "Bearer sk_test_qGvshyvCdk64zO8SMVYEu0ib";
                Stripe.Customer stripeCustomer = customerService.Create(myCustomer);


                //Create Charge Object with details of Charge  
                var options = new Stripe.ChargeCreateOptions
                {
                    Amount = Convert.ToInt32("500"),
                    Currency = "USD",
                    ReceiptEmail = stripeEmail,
                    CustomerId = stripeCustomer.Id,
                    Description = Convert.ToString("Sample Charge"), //Optional  
                };
                //and Create Method of this object is doing the payment execution.  
                var service = new Stripe.ChargeService();
                Stripe.Charge charge = service.Create(options); // This will do the Payment


                //**********************

                //var customers = new CustomerService();
                //var charges = new ChargeService();

                //var customer = customers.Create(new CustomerCreateOptions
                //{
                //    Email = stripeEmail,
                //    SourceToken = stripeToken
                //});

                //var charge = charges.Create(new ChargeCreateOptions
                //{
                //    Amount = 500,
                //    Description = "Sample Charge",
                //    Currency = "usd",
                //    CustomerId = "cus_EcMGgE1mQ5h8nF"
                //});

                // further application specific code goes here
                return Ok("Payment has been done.");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }


        #endregion

    }
}
