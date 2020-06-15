using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class TalkModel
    {
        public int TalkId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(4500,MinimumLength =10)]
        public string Abstract { get; set; }
        [Required]
        [Range(1,1000)]
        public int Level { get; set; }
        public SpeakerModel Speaker { get; set; }
    }
}