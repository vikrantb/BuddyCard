using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyCard.Providers
{
	using BuddyCard.Models;
	using PayPal;
	using PayPal.Api.Payments;

	public class FundingAccountProvider
	{
		public IEnumerable<FundingAccount> GetAllFundingAccounts()
		{
			CreateCreditCard();
			// Get a reference to the config
			var config = PayPal.Manager.ConfigManager.Instance.GetProperties();

			// Read the clientId and clientSecret stored in the config
			var clientId = config[BaseConstants.ClientId];
			var clientSecret = config[BaseConstants.ClientSecret];

			// Use OAuthTokenCredential to request an access token from PayPal
			var accessToken = new OAuthTokenCredential(clientId, clientSecret, config).GetAccessToken();

			var apiContext = new APIContext(accessToken);

			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters.Add("count", "1");
			parameters.Add("startIndex", "1");

			PaymentHistory payHistory = Payment.List(apiContext, parameters);

			
			List<FundingAccount> fundingAccounts = new List<FundingAccount>();
			for (int i = 0; i < 12; i++)
			{
				FundingAccount fa = new FundingAccount()
				{
					Id = i,
					Name = "x" + i.ToString(),
				};
				fundingAccounts.Add(fa);
			}

			return fundingAccounts;
		}

		public void CreateCreditCard()
		{
			
            // ###CreditCard
            // A resource representing a credit card that can be
            // used to fund a payment.
            CreditCard credtCard = new CreditCard();
            credtCard.expire_month = 11;
            credtCard.expire_year = 2018;
            credtCard.number = "4417119669820331";
            credtCard.type = "visa";

                 // ### Api Context
                 // Pass in a `APIContext` object to authenticate 
                 // the call and to send a unique request id 
                 // (that ensures idempotency). The SDK generates
                 // a request id if you do not pass one explicitly. 
                  // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext..
                APIContext apiContext = Configuration.GetAPIContext();

                // ###Save
                // Creates the credit card as a resource
                // in the PayPal vault. The response contains
                // an 'id' that you can use to refer to it
                // in the future payments.
                CreditCard createdCreditCard = credtCard.Create(apiContext);
		}

	}
}