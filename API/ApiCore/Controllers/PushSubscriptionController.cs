using ChatHexagone.Models;
using ApiCore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ChatHexagone.Adapters.LeftSide;
using Newtonsoft.Json;

namespace ApiCore.Controllers
{
    public class PushSubscriptionController : Controller
    {
        private readonly ISubscriptionAdapter _adapter = Dependencies.Resolve<ISubscriptionAdapter>();
        
        [HttpPost][Route("subscribe")]
        public string Post([FromBody]PushSubscription subscription)
        {
            var clientId = _adapter.AddNewPushSuscription(subscription);
            return JsonConvert.SerializeObject(new { clientId = clientId });
        }

    }
}
