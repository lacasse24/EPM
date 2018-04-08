using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SqueletteImplantation.Controllers;
using SqueletteImplantation.DbEntities;
using SqueletteImplantation.DbEntities.DTOs;
using SqueletteImplantation.DbEntities.Models;
using Xunit;


namespace SqueletteTests
{

    public class TraceControllerTest
    {
        private readonly TraceController _traceController;
        private const string NomFich = "Test.pdf";
        private TraceDTO nouvTrac = new TraceDTO {Id=new int[] { 1,2,3,4} , Nomfich= NomFich, chemin = RealUpload.Chemin + NomFich };

        public TraceControllerTest()
        {
            var options = new DbContextOptionsBuilder<BD_EPM>()
                .UseInMemoryDatabase("DatabaseTrace-" + $"{Guid.NewGuid()}")
                .Options;

            var bdEnMemoire = new BD_EPM(options);

            _traceController = new TraceController(bdEnMemoire, new FakeUpload());
        }

        [Fact]
        public void TraceAjoute()
        {
            var created = _traceController.AjoutTraceDansBd(nouvTrac);
            Assert.Equal(200, (created as OkObjectResult).StatusCode);
        }

 
        
        [Fact]
        public void TestInMemoryAddRetrieveTrace()
        {
            var created = _traceController.AjoutTraceDansBd(nouvTrac);
            var resultat = _traceController.GetTraceSelonId(((created as OkObjectResult).Value as Trace).TracId);
            Assert.Equal(NomFich, ((resultat as OkObjectResult).Value as Trace).TraceNom);

        }
        
        
    }


}
