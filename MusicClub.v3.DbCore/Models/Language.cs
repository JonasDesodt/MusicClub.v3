﻿using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated))]
    public class Language : ILanguage
    {
        public int Id { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public required string Identifier { get; set; }

        public IList<DescriptionTranslation> DescriptionTranslations { get; set; } = [];
    }
}
