using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteApp.Core.Entities.Base;
using NoteApp.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core.Entities.EntityTypeConfigurations
{
    public class BaseUserEntityTypeConfiguration<T> where T : BaseUser
    {
        //    public override void Configure(EntityTypeBuilder<T> builder)
        //    {
        //        base.Configure(builder);

        //        builder.Property(x => x.FirstName)
        //               .HasMaxLength(maxLength: 256)
        //               .IsRequired();

        //        builder.Property(x => x.LastName)
        //               .HasMaxLength(maxLength: 256)
        //               .IsRequired();

        //        builder.Property(x => x.Email)
        //               .HasMaxLength(maxLength: 256)
        //               .IsRequired();

        //        builder.Property(x => x.Gender)
        //               .IsRequired();

        //        builder.Property(x => x.PhoneNumber)
        //               .HasMaxLength(maxLength: 15)
        //               .IsRequired(false);

        //        builder.Property(x => x.Image)
        //               .IsRequired(false);

        //        builder.Property(x => x.Address)
        //               .HasMaxLength(maxLength: 256)
        //               .IsRequired(false);
        //    }
    }

}
