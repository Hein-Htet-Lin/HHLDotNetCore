using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHLDotNetCore.RestApi.DataModels
{
    public class BlogsDataModel
    {
        public int BlogId {get; set;}

        public String? BlogTitle {get; set;}
        
        public String? BlogAuthor {get; set;}

        public String? BlogContent {get; set;}

        public bool DeleteFlag {get; set;}
    }
}