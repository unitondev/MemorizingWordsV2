using System.Collections.Generic;

#nullable disable

namespace MemorizingWordsV2.Domain.Models
{
    public partial class RussianWord : Model
    {
        public RussianWord()
        {
            EnglishRussianWords = new HashSet<EnglishRussianWord>();
        }
        
        public string Word { get; set; }

        public virtual ICollection<EnglishRussianWord> EnglishRussianWords { get; set; }
    }
}
