using System;
using System.Collections.Generic;
using System.Text;

namespace TextGenerator
{
    interface ITextAnalyzer
    {
        void SetText(string text);

        List<List<string>> GetParagraphs();

        List<(string, List<string>)> GetSentences();

        List<string> GetWords();
    }
}
