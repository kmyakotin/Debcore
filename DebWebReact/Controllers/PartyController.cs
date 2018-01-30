using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Data;
using Debcore.Model;
using Microsoft.AspNetCore.Mvc;

namespace Debweb.Controllers
{
    //TODO add swagger 
    [Route("api/[controller]")]
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

        [HttpGet("my")]
        public async Task<IActionResult> Get()
        {
            var res = await _db.GetParties(whereExpression => true);
            return Json(res);
        }

        // TODO REST API
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var party = await _db.GetParty(name);
            return Json(party);
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> Save(string name)
        {
            //todo as post and get model
            var party = new Party(name).Testify();
            var res = await _db.SaveParty(party);
            return View("Index", res);
        }

        [HttpPost("{partyName}/buyProduct")]
        public async Task<IActionResult> BuyProduct(string partyName, [FromBody] BuyProductModel model)
        {
            var party = await _db.GetParty(partyName);
            if (party == null)
                return NotFound("party not found");

            var person = party.Participants.SingleOrDefault(x => x.Id == model.PersonId);

            if (person == null)
                return NotFound("Person not found");

            person.Buy(new Product(model.ProductName, model.Price));
            var res = await _db.SaveParty(party);
            return Json(party);
        }
    }

    public class BuyProductModel
    {
        public Guid PersonId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public BuyProductModel()
        {
        }
    }
}