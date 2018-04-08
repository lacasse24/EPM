using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqueletteImplantation.DbEntities.Models;


namespace SqueletteImplantation.DbEntities.Mappers
{
    public class RelTracCritMap
    {
        public RelTracCritMap(EntityTypeBuilder<RelTracCrit> entityBuilder)
        {
            entityBuilder.Property(rtc => rtc.CritId).IsRequired();
            entityBuilder.Property(rtc => rtc.TracId).IsRequired();
        }
    }
}
