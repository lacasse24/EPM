using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqueletteImplantation.DbEntities.Models;

namespace SqueletteImplantation.DbEntities.Mappers
{
    public class DomaineMap
    {
        public DomaineMap(EntityTypeBuilder<Domaine> entityBuilder)
        {

            entityBuilder.HasKey(d => d.DomId);
            entityBuilder.Property(d => d.DomNom).IsRequired();

        }
    }
}
