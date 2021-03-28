using System;
using System.Collections.Generic;
using System.Text;

namespace TextGenerator
{
    interface ITextGenerator
    {
        void SetData(string path);

        string GetSentence(string punctuation);

        string GetParagraph();

        string GetText(int paragraphs);
    }
}
