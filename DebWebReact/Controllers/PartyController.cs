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

        // TODO REST API
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var party = await _db.GetParty(name);
            if (party == null)
                return NotFound();
            return View("Index", party);
        }

        [HttpGet("save/{name}")]
        public async Task<IActionResult> Save(string name)
        {
            //todo as post and get model
            var party = new Party(name).Testify();
            var res = await _db.SaveParty(party);
            return View("Index", res);
        }
    }
}