﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GC_Capstone5.Models
{


    public class Movie
    {
        //public bool adult { get; set; }
        //public string backdrop_path { get; set; }
        //public object belongs_to_collection { get; set; }
        //public int budget { get; set; }
        public Genre[] genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        //public string imdb_id { get; set; }
        //public string original_language { get; set; }
        //public string original_title { get; set; }
        public string overview { get; set; }
        //public float popularity { get; set; }
        public string poster_path { get; set; }
        //public Production_Companies[] production_companies { get; set; }
        //public Production_Countries[] production_countries { get; set; }
        public string release_date { get; set; }
        //public int revenue { get; set; }
        public int runtime { get; set; }
        //public Spoken_Languages[] spoken_languages { get; set; }
        //public string status { get; set; }
        //public string tagline { get; set; }
        public string title { get; set; }
        //public bool video { get; set; }
        public float vote_average { get; set; }
        //public int vote_count { get; set; }

        public Movie()
        {

        }
    }

     public class GenreRoot
    {
        public Genre[] genres { get; set; }
    }


    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    //public class Production_Companies
    //{
    //    public int id { get; set; }
    //    public string logo_path { get; set; }
    //    public string name { get; set; }
    //    public string origin_country { get; set; }
    //}

    //public class Production_Countries
    //{
    //    public string iso_3166_1 { get; set; }
    //    public string name { get; set; }
    //}

    //public class Spoken_Languages
    //{
    //    public string iso_639_1 { get; set; }
    //    public string name { get; set; }
    //}


    /*********************************************************************************************/
    // model list from search

    public class SearchRootobject
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public SearchResult[] results { get; set; }

        //[JsonProperty(PropertyName = "results")]
        //public List<SearchResult> results { get; set; }
    }

    public class SearchResult
    {
        public float popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public string poster_path { get; set; }
        public int id { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public int[] genre_ids { get; set; }
        public string title { get; set; }
        public float vote_average { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
    }

    public class MovieConfigurationRoot
    {
        public Images images { get; set; }
        public string[] change_keys { get; set; }
    }

    public class Images
    {
        public string base_url { get; set; }
        public string secure_base_url { get; set; }
        public string[] backdrop_sizes { get; set; }
        public string[] logo_sizes { get; set; }
        public string[] poster_sizes { get; set; }
        public string[] profile_sizes { get; set; }
        public string[] still_sizes { get; set; }
    }
}
