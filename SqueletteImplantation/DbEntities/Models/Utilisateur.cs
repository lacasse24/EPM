using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqueletteImplantation.DbEntities.Models
{
    public class Utilisateur
    {
        //Nom, Prénom, MDP, ID, Username, courriel, type user (Admin/User)
        public int UtilId { get; set; }
        public string UtilPren { get; set; }
        public string UtilNom { get; set; }
        public string UtilPWD { get; set; }
        public string UtilUserName { get; set; }
        public string UtilEmail { get; set; }
        public int UtilType { get; set; }

        public List<RelTracUsag> reltracusag { get; set; }
    }
}
