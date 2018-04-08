using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SqueletteImplantation.DbEntities;
using SqueletteImplantation.DbEntities.DTOs;


namespace SqueletteImplantation.Controllers
{
    public class CategorieController: Controller
    {
        private readonly BD_EPM _maBd;

        public CategorieController(BD_EPM maBd)
        {
            _maBd = maBd;
        }

      
        [HttpGet]
        [Route("api/Categorie")]
        public IEnumerable GetListeCategorie()
        {
            return _maBd.Categorie.ToList();
        }


        

        [HttpGet]
        [Route("api/Categorie/{id}")]
        public IActionResult GetCategorieSelonId(int id)
        {
            var categorie = _maBd.Categorie.FirstOrDefault(ca => ca.CatId== id);

            if (categorie == null)
            {
                return NotFound();
            }

            return new OkObjectResult(categorie);
        }

      
        [HttpGet]
        [Route("api/CategorieDomaine/{DomId}")]
        public IEnumerable GetCategorieSelonDomaine(int domId)
        {
            return _maBd.Categorie.Where(ca => ca.DomId == domId).ToList();
        }



        [HttpDelete]
        [Route("api/delcat/{id}")]
        public IActionResult DeleteCategorieSelonId(int id)
        {
            var categorie = _maBd.Categorie.FirstOrDefault(ca => ca.CatId== id);

            if (categorie == null)
            {
                return NotFound();
            }

            _maBd.Remove(categorie);
            _maBd.SaveChanges();

            return new OkResult();
        }


        //ajouter une catégorie

        [HttpPost]
        [Route("api/ajoutcat")]
        public IActionResult AddCategorie([FromBody]CategorieDTO catdto)
        {
            var cate = catdto.CreateCategorie();
            _maBd.Add(cate);
            _maBd.SaveChanges();

            return new OkObjectResult(cate);
        }



    }
}
