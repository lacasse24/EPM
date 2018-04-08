using SqueletteImplantation.DbEntities.Models;

namespace SqueletteImplantation.DbEntities.DTOs
{
    public class CritereDTO
    {
        public string NomCrit { get; set; }
        public int IdCat { get; set; }

        public Critere CreateCritere()
        {
            return new Critere{ CritNom = NomCrit, CatId = IdCat };
        }


    }
}
