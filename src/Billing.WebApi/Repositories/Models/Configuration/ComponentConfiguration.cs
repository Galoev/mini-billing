using Billing.WebApi.Client.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Billing.WebApi.Repositories.Models.Configuration
{
    public class ComponentConfiguration : IEntityTypeConfiguration<ComponentDbo>
    {
        public void Configure(EntityTypeBuilder<ComponentDbo> builder)
        {
            builder.HasData
                (
                    new ComponentDbo
                    {
                        Id = Guid.NewGuid(),
                        QuantityType = QuantityType.Kilogram,
                        UnitPrice = 800,
                        Description = "Curd"
                    },
                    new ComponentDbo
                    {
                        Id = Guid.NewGuid(),
                        QuantityType = QuantityType.Litre,
                        UnitPrice = 50,
                        Description = "Milk"
                    },
                    new ComponentDbo
                    {
                        Id = Guid.NewGuid(),
                        QuantityType = QuantityType.Piece,
                        UnitPrice = 6,
                        Description = "Egg"
                    },
                    new ComponentDbo
                    {
                        Id = Guid.NewGuid(),
                        QuantityType = QuantityType.Kilogram,
                        UnitPrice = 1000,
                        Description = "Butter"
                    },
                    new ComponentDbo
                    {
                        Id = Guid.NewGuid(),
                        QuantityType = QuantityType.Kilogram,
                        UnitPrice = 40,
                        Description = "Flour"
                    },
                    new ComponentDbo
                    {
                        Id = Guid.NewGuid(),
                        QuantityType = QuantityType.Kilogram,
                        UnitPrice = 200,
                        Description = "Yeast"
                    },
                    new ComponentDbo
                    {
                        Id = Guid.NewGuid(),
                        QuantityType = QuantityType.Kilogram,
                        UnitPrice = 35,
                        Description = "Sugar"
                    },
                    new ComponentDbo
                    {
                        Id = Guid.NewGuid(),
                        QuantityType = QuantityType.Kilogram,
                        UnitPrice = 40,
                        Description = "Salt"
                    }
                );
        }
    }
}
