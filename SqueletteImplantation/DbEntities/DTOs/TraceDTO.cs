using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SqueletteImplantation.DbEntities.Models;
using System.IO;
using SqueletteImplantation.Controllers;

namespace SqueletteImplantation.DbEntities.DTOs
{
    public class TraceDTO
    {
        public int[] Id { get; set; }
        public string Nomfich { get; set; }
        public string chemin { get; set; }



        public Trace CreateTrace()
        {
            return new Trace { TraceNom = Nomfich, TracLogi = IdentificationLogiciel(), TracUrl = chemin};
        }

        private string IdentificationLogiciel()
        {
            string extension;

            if (chemin == null || chemin == "")
            {
                return "Aucun fichier";
            }
            extension = Path.GetExtension(chemin);
            switch (extension)
            {
                case ".pdf":
                    return "Adobe Reader";
                default:
                    return "Autre logiciel";
            }  
        }
    }
}
