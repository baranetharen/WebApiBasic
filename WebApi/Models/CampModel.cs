using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class CampModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Moniker { get; set; }
        [Required]
        public DateTime EventDate { get; set; } = DateTime.MinValue;
        [Range(1,10)]
        public int Length { get; set; } = 1;
        public ICollection<TalkModel> Talks { get; set; }
        public string LocationStateProvince { get; set; }  //because of Automaping it look for the classname 
        public string LocationPostalCode { get; set; }     //in the prifx of the property and bind it
        public string Country { get; set; }        //ex location in prefix bind with location class and controller to the variablr name
    }
}