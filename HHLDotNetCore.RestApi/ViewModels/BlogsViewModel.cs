using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHLDotNetCore.RestApi.ViewModels
{
    public class BlogsViewModel
    {
        public int Id {get; set;}

        public String? Title {get; set;}
        
        public String? Author {get; set;}

        public String? Content {get; set;}

        public bool DeleteFlag {get; set;}
    }
}