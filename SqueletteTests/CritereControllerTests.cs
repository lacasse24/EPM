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
    public class CritereControllerTests
    {
       
        private const int CatId = 1;
        private const string CritNom = "truc";
      


        private readonly CritereController _CritereController;
        public CritereControllerTests()
        {

            var options = new DbContextOptionsBuilder<BD_EPM>()
                .UseInMemoryDatabase("DatabaseTrace-" + $"{Guid.NewGuid()}")
                .Options;

            var bdEnMemoire = new BD_EPM(options);

            _CritereController = new CritereController(bdEnMemoire);
        }


        [Fact]
        public void NoCritNotFound()
        {
            var result = _CritereController.GetCritereSolonId(10000000);

            Assert.Equal(404, ((NotFoundResult)result).StatusCode);
        }

        [Fact]
        public void AjoutCritereTest()
        {
            var created = _CritereController.AddCritere(new CritereDTO { NomCrit = CritNom, IdCat = CatId });
            var resultat = _CritereController.GetCritereSolonId(((created as OkObjectResult).Value as Critere).CritId);
            Assert.Equal(CritNom, ((resultat as OkObjectResult).Value as Critere).CritNom);
            Assert.Equal(CatId, ((resultat as OkObjectResult).Value as Critere).CatId);
        }

        [Fact]
        public void TestCreateDelete()
        {
            var created = _CritereController.AddCritere(new CritereDTO{ NomCrit = CritNom, IdCat = CatId });

            var resultat = _CritereController.DeleteCritereSelonId(((created as OkObjectResult).Value as Critere).CritId);

            Assert.Equal(200, (resultat as OkResult).StatusCode);

            var entityNotFound = _CritereController.GetCritereSolonId(((created as OkObjectResult).Value as Critere).CritId);

            Assert.Equal(404, ((NotFoundResult)entityNotFound).StatusCode);
        }

    }
}
