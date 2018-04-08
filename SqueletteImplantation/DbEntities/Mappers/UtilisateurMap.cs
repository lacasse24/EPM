using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqueletteImplantation.DbEntities.Models;


namespace SqueletteImplantation.DbEntities.Mappers
{
    public class UtilisateurMap
    {
        public UtilisateurMap(EntityTypeBuilder<Utilisateur> entityBuilder)
        {
            entityBuilder.HasKey(u => u.UtilId);
            entityBuilder.Property(u => u.UtilPren).IsRequired();
            entityBuilder.Property(u => u.UtilNom).IsRequired();
            entityBuilder.Property(u => u.UtilPWD).IsRequired();
            entityBuilder.Property(u => u.UtilUserName).IsRequired();
            entityBuilder.Property(u => u.UtilEmail).IsRequired();
            entityBuilder.Property(u => u.UtilType).IsRequired();
        }

    }
}
