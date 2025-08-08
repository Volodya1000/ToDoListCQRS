using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListCQRS.Persistence.Entities;

namespace ToDoListCQRS.Persistence.Configurations;

public class ToDoItemEntityConfiguration: IEntityTypeConfiguration<ToDoItemEntity>
{
    public void Configure(EntityTypeBuilder<ToDoItemEntity> builder)
    {
        builder.HasKey(td => td.Id);

        builder.Property(td => td.Id).ValueGeneratedNever();

        builder.Property(e => e.Title)
            .IsRequired();
    }
}
