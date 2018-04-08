using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqueletteImplantation.DbEntities.Models;

namespace SqueletteImplantation.DbEntities.Mappers
{
    public class CategorieMap
    {
        public CategorieMap(EntityTypeBuilder<Categorie> entityBuilder)
        {
            entityBuilder.HasKey(ca => ca.CatId);
            entityBuilder.Property(ca => ca.CatNom).IsRequired();
            entityBuilder.Property(ca => ca.DomId).IsRequired();
        }
    }
}
