using ChatHexagone.Models;
using ApiCore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ChatHexagone.Adapters.LeftSide;

namespace ApiCore.Controllers
{
    public class PushSubscriptionController : Controller
    {
        private readonly ISubscriptionAdapter _adapter = Dependencies.Resolve<ISubscriptionAdapter>();
        
        [HttpPost][Route("subscribe")]
        public IActionResult Post([FromBody]PushSubscription subscription)
        {
            _adapter.AddNewPushSuscription(subscription);
            return Accepted();
        }

    }
}
