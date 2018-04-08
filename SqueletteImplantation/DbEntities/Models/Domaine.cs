using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqueletteImplantation.DbEntities.Models
{
    public class Domaine
    {
        public int DomId { get; set; }
        public string DomNom { get; set; }

        public List<Categorie> categories { get; set; }

    }
}
