using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqueletteImplantation.DbEntities.Models;

namespace SqueletteImplantation.DbEntities.Mappers
{
    public class RelTracUsagMap
    {
        public RelTracUsagMap(EntityTypeBuilder<RelTracUsag> entityBuilder)
        {
            entityBuilder.Property(rtu => rtu.TracId).IsRequired();
            entityBuilder.Property(rtu => rtu.UtilId).IsRequired();
            entityBuilder.Property(rtu => rtu.DateTelechargement).IsRequired();
        }
    }
}
