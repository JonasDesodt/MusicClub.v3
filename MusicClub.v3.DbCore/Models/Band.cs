﻿using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated), nameof(TenantId))]
    public class Band : IBand
    {
        public int Id { get; set; }


        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public required int TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        public IList<Bandname> Bandnames { get; set; } = [];
    }
}