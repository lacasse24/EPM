using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqueletteImplantation.DbEntities.Models
{
    public class RelTracUsag
    {
        public int TracId { get; set; }

        public int UtilId { get; set; }

        public Trace trace { get; set; }

        public Utilisateur utilisateur { get; set; }


        public DateTime DateTelechargement { get; set; }
    }
}
