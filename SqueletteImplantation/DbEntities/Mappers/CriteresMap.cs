using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqueletteImplantation.DbEntities.Models;

namespace SqueletteImplantation.DbEntities.Mappers
{
    public class CriteresMap
    {
        public CriteresMap(EntityTypeBuilder<Critere> entityBuilder)
        {
            
            entityBuilder.HasKey(cr => cr.CritId);
            entityBuilder.Property(cr => cr.CritNom).IsRequired();
            entityBuilder.Property(cr => cr.CatId).IsRequired();

        }
    }
}
