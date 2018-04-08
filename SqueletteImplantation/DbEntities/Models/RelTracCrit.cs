using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqueletteImplantation.DbEntities.Models
{
    public class RelTracCrit
    {
        public int CritId { get; set; }

        public int TracId { get; set; }

        public Critere criteres { get; set; }

        public Trace trace { get; set; }
    }
}
