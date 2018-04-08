
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SqueletteImplantation.DbEntities;
using SqueletteImplantation.DbEntities.Models;
using SqueletteImplantation.DbEntities.DTOs;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System;

namespace SqueletteImplantation.Controllers
{
    public class TraceController: Controller
    {
        private readonly BD_EPM _maBd;

        private readonly UploadService _uploadService;

        public TraceController(BD_EPM maBd, UploadService uploadService)
        {
            _maBd = maBd;
            _uploadService = uploadService;
        }

       
        [HttpGet]
        [Route("api/Trace")]
        public IEnumerable GetListeTrace()
        {
            return _maBd.Trace.ToList();
        }

        

        [HttpGet]
        [Route("api/Trace/{id}")]
        public IActionResult GetTraceSelonId(int id)
        {
            var trace = _maBd.Trace.FirstOrDefault(t => t.TracId == id);

            if (trace == null)
            {
                return NotFound();
            }

            return new OkObjectResult(trace);
        }

   

        [HttpGet]
        [Route("api/TraceListe")]
        public IActionResult GetTracesSelonListeCriteres(int[] id)
        {
            var listeTrace = RechercheTraceSelonListeCriteres(id);

            if (listeTrace == null)
            {
                return NotFound();
            }

            return new OkObjectResult(listeTrace);
        }


        private IQueryable<object> RechercheTraceSelonListeCriteres(int[] id)
        {
            return from tr in _maBd.Trace
                join rl in _maBd.RelTracCrit on tr.TracId equals rl.TracId
                join cr in _maBd.Critere on rl.CritId equals cr.CritId
                where id.Contains(cr.CritId)
                group tr by new {tr.TracId, tr.TraceNom, tr.TracUrl,tr.TracLogi}
                into grp
                where grp.Count() == id.Length
                select grp.Key;
        }


       
        [HttpPost]
        [Route("api/ajoutfichier")]
        public IActionResult UploadFichierSurServeur(IList<IFormFile> traces)
        {
            string NomTrace;
            string Date = DateTime.Now.ToString("h_mm_ss_");

            if(traces.Count==1 && traces[0] != null)
            {
                NomTrace =  Date  + traces[0].FileName ;

                if (_uploadService.upload(traces[0], RealUpload.Chemin + NomTrace))
                {
                    return new OkObjectResult(RealUpload.Chemin + NomTrace);
                }
            }
            return new BadRequestResult();
        }

        [HttpPost]
        [Route("api/ajouttrace")]
        public IActionResult AjoutTraceDansBd([FromBody] TraceDTO nouvtrace)
        {
            if(nouvtrace.Id.Length > 0 && (nouvtrace.Nomfich != "" || nouvtrace.Nomfich != null) && (nouvtrace.chemin != null || nouvtrace.chemin != ""))
            {
                Trace trace;

                trace = nouvtrace.CreateTrace();
                _maBd.Add(trace);
                _maBd.SaveChanges();

                RelTracCrit relation;
                for (int i = 0; i < nouvtrace.Id.Length; i++)
                {
                    relation = new RelTracCrit { CritId = nouvtrace.Id[i], TracId = trace.TracId };
                    _maBd.Add(relation);
                }
                _maBd.SaveChanges();
                return new OkObjectResult(trace);
            }
            return new BadRequestResult();
        }


        [HttpDelete]
        [Route("api/TraceListe/{id}")]
        public IActionResult DeleteTraceSelonId(int id)
        {
            var trace = _maBd.Trace.FirstOrDefault(t => t.TracId == id);
            string CheminApp = "/home/ubuntu/EPM/implantation-a17-epm/SqueletteImplantation/wwwroot/Upload/";

            if (trace == null)
            {
                return NotFound();
            }

            try
            {
                FileInfo Fichier = new FileInfo(CheminApp + trace.TraceNom);
                Fichier.Delete();
            }
            catch (IOException)
            {
                return new BadRequestResult();
            }

            _maBd.Remove(trace);
            _maBd.SaveChanges();

            return new OkResult();

        }
    }
}
