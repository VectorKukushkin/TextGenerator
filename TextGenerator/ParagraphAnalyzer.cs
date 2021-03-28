using System;
using System.Collections.Generic;
using System.Text;

namespace TextGenerator
{
    class ParagraphAnalyzer
    {
        public ParagraphAnalyzer(string paragraph)
        {
            text = paragraph;
            sentences = new List<(string, List<string>)>();
            words = new List<string>();
            word = "";
            for (i = 0; i < text.Length; i++)
            {
                AnalyzeSymbol(text[i]);
            }
        }

        public List<(string, List<string>)> AnylyzeParagraph()
        {
            sentences = new List<(string, List<string>)>();
            words = new List<string>();
            word = "";
            for (i = 0; i < text.Length; i++)
            {
                AnalyzeSymbol(text[i]);
            }
            return sentences;
        }

        private string text = "";
        private List<(string, List<string>)> sentences;
        private List<string> words;
        private string word;

        private int i;

        private bool IsSpace(int num)
        {
            if (num >= 0 && num < text.Length)
                if (text[num] == ' ' || text[num] == '\t')
                    return true;
            return false;
        }

        private bool IsPunctuation(int num)
        {
            if (num >= 0 && num < text.Length)
                if (text[num] == '.' || text[num] == '!' || text[num] == '?')
                    return true;
            return false;
        }

        private void AddWord()
        {
            if (word.Replace(" ", "").Replace("\t", "") != "")
                words.Add(word);
            word = "";
        }

        private void GetSpace()
        {
            AddWord();
            for (; i + 1 < text.Length && IsSpace(i + 1); i++) ;
            if (words.Count > 0)
                words.Add(" ");
        }

        private void GetPunctuation()
        {
            AddWord();
            for (; i < text.Length && IsPunctuation(i); i++)
                word += text[i];
            sentences.Add((word, words));
            words = new List<string>();
            word = "";
            i--;
        }

        private void AnalyzeSymbol(char symbol)
        {
            switch (symbol)
            {
                case '\t':
                case ' ':
                    GetSpace();
                    break;
                case ',':
                case ';':
                case '(':
                case '{':
                case '[':
                case ')':
                case '}':
                case ']':
                case ':':
                case '\'':
                case '"':
                    AddWord();
                    words.Add(symbol.ToString());
                    break;
                case '.':
                case '!':
                case '?':
                    GetPunctuation();
                    break;
                case '\r':
                case '\n':
                    break;
                default:
                    word += symbol;
                    break;
            }
        }
    }
}
