using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SqueletteImplantation.DbEntities;
using SqueletteImplantation.DbEntities.DTOs;

namespace SqueletteImplantation.Controllers
{
    public class CritereController: Controller
    {
        private readonly BD_EPM _maBd;

        public CritereController(BD_EPM maBd)
        {
            _maBd = maBd;
        }

     
        [HttpGet]
        [Route("api/CritereCat/{CatId}")]
        public IEnumerable GetListeCritereSelonCategorie(int catId)
        {
            return _maBd.Critere.Where(cr => cr.CatId == catId).ToList();
        }



        [HttpGet]
        [Route("api/Critere/{id}")]
        public IActionResult GetCritereSolonId(int id)
        {
            var critere = _maBd.Critere.FirstOrDefault(c => c.CritId == id);

            if (critere == null)
            {
                return NotFound();
            }

            return new OkObjectResult(critere);
        }



        [HttpDelete]
        [Route("api/delcrite/{id}")]
        public IActionResult DeleteCritereSelonId(int id)
        {
            var critere = _maBd.Critere.FirstOrDefault(c => c.CritId== id);

            if (critere == null)
            {
                return NotFound();
            }

            _maBd.Remove(critere);
            _maBd.SaveChanges();

            return new OkResult();
        }

        

        [HttpPost]
        [Route("api/ajoutcrite")]
        public IActionResult AddCritere([FromBody]CritereDTO critdto)
        {
            var Crit = critdto.CreateCritere();
            _maBd.Add(Crit);
            _maBd.SaveChanges();

            return new OkObjectResult(Crit);
        }


    }
}
