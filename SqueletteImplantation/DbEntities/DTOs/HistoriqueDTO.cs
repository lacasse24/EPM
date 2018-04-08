using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqueletteImplantation.DbEntities.Models;

namespace SqueletteImplantation.DbEntities.DTOs
{
    public class HistoriqueDTO
    {
        public int tracId;

        public int utilId;

        public RelTracUsag CreationElementHistorique()
        {
            return new RelTracUsag { UtilId = utilId, TracId = tracId, DateTelechargement = DateTime.Now };
        }
    }
}
