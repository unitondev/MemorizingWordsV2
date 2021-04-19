using System.Collections.Generic;

#nullable disable

namespace MemorizingWordsV2.Domain.Models
{
    public partial class PartOfSpeech
    {
        public PartOfSpeech()
        {
            EnglishWords = new HashSet<EnglishWord>();
        }

        public int Id { get; set; }
        public string PartName { get; set; }

        public virtual ICollection<EnglishWord> EnglishWords { get; set; }
    }
}
