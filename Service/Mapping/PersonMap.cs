
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Entity;

namespace Service.Mapping
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).IsRequired();
            Property(p => p.City).IsRequired();
            Property(p => p.Country).IsRequired();
            Property(p => p.ContactNumber).IsRequired();
            ToTable("Person");
        }
    }
}
