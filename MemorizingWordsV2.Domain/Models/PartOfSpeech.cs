using System.Collections.Generic;

#nullable disable

namespace MemorizingWordsV2.Domain.Models
{
    public partial class PartOfSpeech : Model
    {
        public PartOfSpeech()
        {
            EnglishWords = new HashSet<EnglishWord>();
        }
        
        public string PartName { get; set; }

        public virtual ICollection<EnglishWord> EnglishWords { get; set; }
    }
}
