
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace MemorizingWordsV2.Domain.Models
{
    public partial class EnglishWord : Model
    {
        public EnglishWord()
        {
            EnglishRussianWords = new HashSet<EnglishRussianWord>();
        }
        
        public string Word { get; set; }
        public int? PartOfSpeechId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual PartOfSpeech PartOfSpeech { get; set; }
        public virtual ICollection<EnglishRussianWord> EnglishRussianWords { get; set; }
    }
}
