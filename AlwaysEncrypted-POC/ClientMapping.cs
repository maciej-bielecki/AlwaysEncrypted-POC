using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AlwaysEncrypted_POC
{
    internal class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable(name: "client");
            builder.Property<Guid>(nameof(Client.Id)).HasColumnName("id");
            builder.HasKey(x => x.Id);

            builder.Property<string>(nameof(Client.Name)).HasColumnName("name").HasMaxLength(250).IsRequired();
            builder.Property<string>(nameof(Client.CreditCardNumber)).HasColumnName("ccn").HasMaxLength(50).IsRequired();
            builder.Property<byte>(nameof(Client.Rank)).HasColumnName("rank").IsRequired();
            builder.Property<DateTime>(nameof(Client.Birthdate)).HasColumnName("birthdate").IsRequired();
        }

    }
}