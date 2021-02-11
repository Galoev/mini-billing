using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Billing.WebApi.Repositories.Models.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerDbo>
    {
        public void Configure(EntityTypeBuilder<CustomerDbo> builder)
        {
            builder.HasData
                (
                    new CustomerDbo
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sergey",
                        Phone = "8(800)535-35-35",
                        AdditionalInfo = "22 years old guy"
                    },
                    new CustomerDbo
                    {
                        Id = Guid.NewGuid(),
                        Name = "Ilkin",
                        Phone = "8(900)737-37-37",
                        AdditionalInfo = "I don't know how many years old guy"
                    }
                );
        }
    }
}
