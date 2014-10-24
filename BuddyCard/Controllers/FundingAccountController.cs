using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuddyCard.Providers;

namespace BuddyCard.Controllers
{
	using BuddyCard.Models;
    public class FundingAccountController : ApiController
    {
		public FundingAccountProvider provider = new FundingAccountProvider();
		public IEnumerable<FundingAccount> GetAllFundingAccounts()
		{
			return provider.GetAllFundingAccounts();
		}
    }
}
