using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqueletteImplantation.DbEntities.Models
{
    public class Categorie
    {
        public int CatId { get; set; }
        public string CatNom { get; set; }
        public int DomId { get; set; }
        public Domaine domaine { get; set; }

        public List<Critere> criteres { get; set; }
    }
}

