using System;
using System.Collections.Generic;
using System.Text;

namespace TextGenerator
{
    class TextAnalyzer : ITextAnalyzer
    {
        public void SetText(string _text)
        {
            string[] paragraphs = _text.Split("\r\n");
            foreach (string paragraph in paragraphs)
                text.Add(new ParagraphAnalyzer(paragraph.ToLower()).AnylyzeParagraph());
        }

        private List<List<(string punctuation, List<string> words)>> text = new List<List<(string, List<string>)>>();

        public List<List<string>> GetParagraphs()
        {
            List<List<string>> paragraphs = new List<List<string>>();
            foreach (List<(string punctuation, List<string> words)> paragraph in text)
            {
                paragraphs.Add(new List<string>());
                for (int i = 0; i < paragraph.Count; i++)
                    paragraphs[^1].Add(paragraph[i].punctuation);
            }
            return paragraphs;
        }

        public List<(string, List<string>)> GetSentences()
        {
            List<(string, List<string>)> sentences = new List<(string, List<string>)>();
            foreach (List<(string punctuation, List<string> words)> paragraph in text)
                sentences.AddRange(paragraph);
            return sentences;
        }

        public List<string> GetWords()
        {
            List<string> words = new List<string>();
            foreach (List<(string punctuation, List<string> words)> paragraph in text)
                foreach ((string punctuation, List<string> words) sentence in paragraph)
                    foreach (string word in sentence.words)
                        switch (word)
                        {
                            case " ":
                            case ",":
                            case ";":
                            case "(":
                            case "{":
                            case "[":
                            case ")":
                            case "}":
                            case "]":
                            case "-":
                            case ":":
                            case "'":
                            case "\"":
                                break;
                            default:
                                words.Add(word);
                                break;
                        }
            return words;
        }
    }
}
