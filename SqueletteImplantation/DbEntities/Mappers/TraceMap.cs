using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqueletteImplantation.DbEntities.Models;

namespace SqueletteImplantation.DbEntities.Mappers
{
    public class TraceMap
    {

        public TraceMap(EntityTypeBuilder<Trace> entityBuilder)
        {
            entityBuilder.HasKey(tr => tr.TracId);
            entityBuilder.Property(tr => tr.TraceNom).IsRequired();
            entityBuilder.Property(tr => tr.TracUrl).IsRequired();

        }


    }
}
