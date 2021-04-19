#nullable disable

namespace MemorizingWordsV2.Domain.Models
{
    public partial class EnglishRussianWord
    {
        public int EnglishId { get; set; }
        public int RussianId { get; set; }

        public virtual EnglishWord English { get; set; }
        public virtual RussianWord Russian { get; set; }
    }
}
