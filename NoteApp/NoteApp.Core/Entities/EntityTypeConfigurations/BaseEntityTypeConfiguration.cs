﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteApp.Core.Entities.Base;

namespace NoteApp.Core.Entities.EntityTypeConfigurations
{
    //public class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    //{
    //    public virtual void Configure(EntityTypeBuilder<T> builder)
    //    {
    //        builder.HasKey(x => x.Id);

    //        builder.Property(x => x.Id).ValueGeneratedOnAdd();

    //        builder.Property(x => x.Status).IsRequired();
    //        builder.Property(x => x.CreatedBy).HasMaxLength(128).IsRequired();
    //        builder.Property(x => x.CreatedDate).IsRequired();

    //    }
    //}
}
