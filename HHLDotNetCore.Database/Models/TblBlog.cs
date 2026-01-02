using System;
using System.Collections.Generic;

namespace HHLDotNetCore.Database.Models;

public partial class TblBlog
{
    public int BlogId { get; set; }

    public string? BlogAuthor { get; set; }

    public string? BlogTitle { get; set; }

    public int? DeleteFlag { get; set; }

    public string? BlogContent { get; set; }
}
