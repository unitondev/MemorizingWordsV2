using System.Collections.Generic;

#nullable disable

namespace MemorizingWordsV2.Domain.Models
{
    public partial class RussianWord
    {
        public RussianWord()
        {
            EnglishRussianWords = new HashSet<EnglishRussianWord>();
        }

        public int Id { get; set; }
        public string Word { get; set; }

        public virtual ICollection<EnglishRussianWord> EnglishRussianWords { get; set; }
    }
}
