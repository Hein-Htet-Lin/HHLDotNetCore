using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HHLDotNetCore.ConsoleApp.Models
{
    public class BlogDapperDataModels
    {
        public int blogId{get;set;}

        public string blogTitle{get;set;}

        public string blogAuthor{get;set;}

        public string blogContent{get;set;}
    }

    [Table("Tbl_Blog")]
    public class BlogDataModels
    {
        [Key]
        [Column("blogId")]
        public int blogId{get;set;}

        [Column("blogTitle")]
        public string blogTitle{get;set;}

        [Column("blogAuthor")]
        public string blogAuthor{get;set;}

        [Column("blogContent")]
        public string blogContent{get;set;}

        [Column("deleleFlag")]
        public bool deleleFlag{get;set;}
    }
}