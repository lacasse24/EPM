using System.Collections.Generic;

namespace SqueletteImplantation.DbEntities.Models
{
    public class Critere
    {

        public int CritId { get; set; }
        public string CritNom { get; set; }
        public int CatId { get; set; }

        public Categorie categorie { get; set; }

        public List<RelTracCrit> reltraccrit { get; set; }
    }
}
