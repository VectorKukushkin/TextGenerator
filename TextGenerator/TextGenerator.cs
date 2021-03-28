using System;
using System.Collections.Generic;
using System.Text;

namespace TextGenerator
{
    class TextGenerator : BaseTextGenerator
    {
        public TextGenerator(Random random) : base(new TextAnalyzer(), random) { }

        public TextGenerator() : this(new Random()) { }

        public string GetSentence()
        {
            int num = random.Next(10);
            if (num < 7)
                return GetSentence(".");
            if (num < 9)
                return GetSentence("?");
            return GetSentence("!");
        }

        public string GetText()
        {
            return GetText(random.Next(5) + 1);
        }

        public string GetLebedevText()
        {
            return GetText().ToUpper();
        }
    }
}
