using System.ComponentModel.DataAnnotations;

namespace Long2shortWebClient.Models
{
    public class GenerateShortURLViewModel
    {
        [Range(1,long.MaxValue,ErrorMessage ="Your input id is out of range.")]
        public long? UserDefineId { get; set; }
        public bool Status { get; set; }
        public string ShortAddr { get; set; }
        [Url]
        [Required(ErrorMessage ="Long URL should not be null")]
        public string LongAddr { get; set; }
    }
}
