using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movington.Database
{
    internal static class NpgsqlModelBuilderExtensions
    {
        public static PropertyBuilder<Guid> HasPgGeneratedValueOnAdd(this PropertyBuilder<Guid> builder)
            => builder
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("uuid_generate_v4()");
    }
}