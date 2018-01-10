using System.Threading.Tasks;
using Data;
using Debcore.Model;
using Microsoft.AspNetCore.Mvc;

namespace Debweb.Controllers
{
    [Route("party")]
    public class PartyController : Controller
    {
        private readonly IMongoDb _db;

        // GET
        public PartyController(IMongoDb db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var party = new Party("test");

            party.Testify();
            
            return View(party);
        }

        [HttpGet("save")]
        public async Task<IActionResult> Save()
        {
            //todo as post and get model
            var party = new Party("NewParty").Testify();
            var res = await _db.SaveParty(party);
            return View("Index", res);
        }
    }
}