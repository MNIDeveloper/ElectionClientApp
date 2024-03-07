﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace ElectionApp.Models
{
     public class CandidateDisplay
    {
       
        public string FullName { get; set; }
        
        public int CandidateId { get; set; }
       
        public int PersonId { get; set; }
        
        public string Party { get; set; }
       
        public string Constituancy { get; set; }
        
        public string CandidateImage { get; set; }
        
        public string CandidateImageWeb { get; set; }
        
        public bool IsCurrent { get; set; }
    }
}
