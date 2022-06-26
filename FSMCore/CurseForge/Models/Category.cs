﻿using System;

namespace CurseForge.APIClient.Models
{
    public class Category
    {
        public uint Id { get; set; }
        public uint GameId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsClass { get; set; }
        public uint? ClassId { get; set; }
        public uint? ParentCategoryId { get; set; }
        public uint? DisplayIndex { get; set; }
    }
}
