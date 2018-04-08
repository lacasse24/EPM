using SqueletteImplantation.DbEntities.Models;

namespace SqueletteImplantation.DbEntities.DTOs
{
    public class CategorieDTO
    {
        public string NomCat { get; set; }
        public int IdDom { get; set; }

        public Categorie CreateCategorie()
        {
            return new Categorie { CatNom = NomCat, DomId = IdDom };
        }


    }
}
