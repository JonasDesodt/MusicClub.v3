﻿using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IAct))]
    [GenerateFilterResponse(typeof(IAct))]
    [GenerateFilterMappers(typeof(IAct))]
    [GenerateFilterRequestExtensions(typeof(IAct))]
    public partial class ActFilterRequest
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}