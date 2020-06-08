using System;
using System.Collections.Generic;

namespace GC_Capstone5.Models
{
    public partial class Favorite
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public int RunTime { get; set; }
        public string PosterPath { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
