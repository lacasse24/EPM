using System.Linq;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using SqueletteImplantation.DbEntities;
using SqueletteImplantation.DbEntities.Models;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.RegularExpressions;



namespace SqueletteImplantation.Controllers
{
    public class UtilisateurController: Controller
    {
        private readonly BD_EPM _maBd;
        private Courriel courriel = new Courriel();

        public UtilisateurController(BD_EPM maBd)
        {
            _maBd = maBd;
        }

        private static Random random = new Random();
        public static string GetRandomString(int length)
        {
            const string chars = "qwertyupasdfghjkzxcvbnmABCDEFGHJKMNPQRSTUVWXYZ123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool FormatEmailValide(string Email)
        {
            bool CharactereValide;
            string[] tPartieEmail = null;

            CharactereValide = Regex.IsMatch(Email,
                            @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                            RegexOptions.IgnoreCase);

            if(CharactereValide == true)
                tPartieEmail = Email.Split('@');


            if (CharactereValide == true && Email.Length < 255 && tPartieEmail[0].Length < 65)
                return true;
            else
                return false;

        }

        
        [HttpPost]
        [Route("api/utilisateur/login")]
        public IActionResult ConnexionUser([FromBody]Utilisateur util)
        {
            var compteUtilisateur = _maBd.Utilisateur.FirstOrDefault(retour => retour.UtilUserName == util.UtilUserName && retour.UtilPWD == Hash.GetHash(util.UtilPWD));
            object[] tInfoUtil = new object[2];

            if (compteUtilisateur == null)
            {
                return new OkObjectResult(null);
            }

            tInfoUtil[0] = compteUtilisateur.UtilType;
            tInfoUtil[1] = compteUtilisateur.UtilId;

            return new OkObjectResult(tInfoUtil);
        }

        [HttpPost]
        [Route("api/utilisateur/reset/{email}")]
        public IActionResult ReinitialisatioMDP(String email)
        {

            var comptereset = _maBd.Utilisateur.SingleOrDefault(u => u.UtilEmail == email);

            if (comptereset != null)
            {                
                String PWD = GetRandomString(8);
                comptereset.UtilPWD = Hash.GetHash(PWD);
                courriel.setDestination(email);
                courriel.setSender("electrophysiologiemedicale@gmail.com", "noreplyEPM");
                courriel.SetHTMLMessage("<h1>Bonjour " + comptereset.UtilUserName + "</h1><br>Voici le nouveau mot de passe à utiliser lors de votre prochaine connexion : <b>" + 
                    PWD + 
                    "</b><br>Vous pouvez vous connecter à l'adresse suivante : <b><a href='https://epm.dinf.cll.qc.ca'>epm.dinf.cll.qc.ca</a></b>" +
                    "<br><p>Nous vous recommandons de le changer à l'aide de la page de modification du profil le plus tôt possible.<p><br><h2>Merci et bonne journée.");
                courriel.setSubject("Réinitialisation du mot de passe.");
                courriel.sendMessage();


                _maBd.Utilisateur.Attach(comptereset);

                var entry = _maBd.Entry(comptereset);
                entry.Property(e => e.UtilPWD).IsModified = true;
                _maBd.SaveChanges();
            }
            else
            {
                return new ObjectResult(null);
            }
            return new OkObjectResult(true);
        }

       [HttpPatch]
        [Route("api/utilisateur/modifiernomutil/")]
        public IActionResult PatchNomUtilisateur([FromBody]Utilisateur Util)
        {
            OkObjectResult ResultatOk;
            var UtilCorrespondantAuNomUtil = _maBd.Utilisateur.SingleOrDefault(Retour => Retour.UtilUserName == Util.UtilUserName);
            Utilisateur UtilConnecte;
            EntityEntry<Utilisateur> Changement;

            if (UtilCorrespondantAuNomUtil == null)
            {
                UtilConnecte = _maBd.Utilisateur.SingleOrDefault(Retour => Retour.UtilId == Util.UtilId);

                if (UtilConnecte != null)
                {
                    UtilConnecte.UtilUserName = Util.UtilUserName;
                    _maBd.Attach(UtilConnecte);
                    Changement = _maBd.Entry(UtilConnecte);
                    Changement.Property(e => e.UtilUserName).IsModified = true;
                    _maBd.SaveChanges();
                    ResultatOk = new OkObjectResult("Fait");
                }
                else
                    ResultatOk = new OkObjectResult("Erreur d\'authentification");
            }
            else
                ResultatOk = new OkObjectResult("Nom d\'utilisateur déjà utilisé");

            return ResultatOk;
        }

        [HttpPatch]
        [Route("api/utilisateur/modifieremail/")]
        public IActionResult PatchEmail([FromBody]Utilisateur Util)
        {
            OkObjectResult ResultatOk;
            var UtilCorrespondantAuEmail = _maBd.Utilisateur.SingleOrDefault(Retour => Retour.UtilEmail == Util.UtilEmail);
            Utilisateur UtilConnecte;
            EntityEntry<Utilisateur> Changement;

            if (UtilCorrespondantAuEmail == null)
            {
                UtilConnecte = _maBd.Utilisateur.SingleOrDefault(Retour => Retour.UtilId == Util.UtilId);

                if (UtilConnecte != null)
                {

                    if (FormatEmailValide(Util.UtilEmail) == true)
                    {
                        UtilConnecte.UtilEmail = Util.UtilEmail;
                        _maBd.Utilisateur.Attach(UtilConnecte);
                        Changement = _maBd.Entry(UtilConnecte);
                        Changement.Property(e => e.UtilEmail).IsModified = true;
                        _maBd.SaveChanges();
                        ResultatOk = new OkObjectResult("Fait");
                    }
                    else
                        ResultatOk = new OkObjectResult("Email invalide");
                }
                else
                    ResultatOk = new OkObjectResult("Erreur d\'authentification");
            }
            else
                ResultatOk = new OkObjectResult("Email déjà utilisé");

            return ResultatOk;
        }

        [HttpPatch]
        [Route("api/utilisateur/modifiermotdepasse/")]
        public IActionResult PatchMotDePasse([FromBody]Utilisateur Util)
        {
            OkObjectResult ResultatOk;
            Utilisateur UtilConnecte;
            EntityEntry<Utilisateur> Changement;

            UtilConnecte = _maBd.Utilisateur.SingleOrDefault(Retour => Retour.UtilId == Util.UtilId);

            if (UtilConnecte != null)
            {

                UtilConnecte.UtilPWD = Hash.GetHash(Util.UtilPWD);
                _maBd.Utilisateur.Attach(UtilConnecte);
                Changement = _maBd.Entry(UtilConnecte);
                Changement.Property(e => e.UtilPWD).IsModified = true;
                _maBd.SaveChanges();
                ResultatOk = new OkObjectResult("Fait");

            }
            else
                ResultatOk = new OkObjectResult("Erreur d\'authentification");

            return ResultatOk;
        }
    
        [HttpPatch]
        [Route("api/utilisateur/modifierdroituser/")]
        public IActionResult PostDroitUser([FromBody]Utilisateur util)
        {
            OkObjectResult ResultatOk;
            Utilisateur UtilConnecte;
            EntityEntry<Utilisateur> Changement;

            UtilConnecte = _maBd.Utilisateur.SingleOrDefault(Retour => Retour.UtilId == util.UtilId);

            if (UtilConnecte != null)
            {
                UtilConnecte.UtilType = util.UtilType;
                _maBd.Utilisateur.Attach(UtilConnecte);
                Changement = _maBd.Entry(UtilConnecte);
                Changement.Property(e => e.UtilType).IsModified = true;
                _maBd.SaveChanges();
                ResultatOk = new OkObjectResult("Fait");
            }
            else
                ResultatOk = new OkObjectResult("Erreur d\'authentification");
            
            return ResultatOk;
        }

        [HttpPost]
        [Route("api/utilisateur/creationutilisateur")]
        public IActionResult CreationNouvelUtilisateur([FromBody]Utilisateur nouveauutilisateur)
        {
            Utilisateur VerificationUtilExistant = _maBd.Utilisateur.SingleOrDefault(Retour => Retour.UtilUserName == nouveauutilisateur.UtilUserName);

            if (VerificationUtilExistant == null)
            {
                String PWD = GetRandomString(8);
                nouveauutilisateur.UtilPWD = Hash.GetHash(PWD);
                
                courriel.setDestination(nouveauutilisateur.UtilEmail);
                courriel.setSender("electrophysiologiemedicale@gmail.com", "noreplyEPM");
                courriel.SetHTMLMessage(
                    "<h1>Bonjour " + nouveauutilisateur.UtilUserName + "," +
                    "</h1><br>Bienvenue sur le site d'électrophysiologie médicale<br>" + 
                    "<br>Vous pouvez vous connectez à l'adresse suivante : <a href='https://epm.dinf.cll.qc.ca'>epm.dinf.cll.qc.ca</a><br><br>" +
                    "Votre nom d'utilisateur est : <b>" + nouveauutilisateur.UtilUserName + "</b>" +
                    "<br>Votre mot de passe est : <b>" + PWD + 
                    "</b><br><p>Nous vous recommandons de le changer à l'aide de la page de modification du profil lors de votre première connexion.<p>" +
                    "<br><h2>Merci et bonne journée.</h2>");
                courriel.setSubject("Nouveau compte utilisateur");
                courriel.sendMessage();             
                _maBd.Add(nouveauutilisateur);
                _maBd.SaveChanges();
                return new OkObjectResult(true);
            }
            return new OkObjectResult(false);
        }


        [HttpGet]
        [Route("api/utilisateur/liste")]
        public IEnumerable GetListeUtilisateur()
        {
            return _maBd.Utilisateur.ToList();
        }


        
        [HttpDelete]
        [Route("api/delutil/{id}")]
        public IActionResult DeleteUtilSelonId(int id)
        {
            var utilisateur = _maBd.Utilisateur.FirstOrDefault(ca => ca.UtilId== id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            _maBd.Remove(utilisateur);
            _maBd.SaveChanges();

            return new OkResult();
        }

    }
}