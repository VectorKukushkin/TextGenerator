using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextGenerator
{
    class BaseTextGenerator : ITextGenerator
    {
        public BaseTextGenerator(ITextAnalyzer textAnalyzer, Random _random)
        {
            TextAnalyzer = textAnalyzer;
            paragraphs = new List<List<string>>();
            sentences = new Dictionary<string, List<List<string>>>();
            words = new Dictionary<string, List<string>>();
            random = _random;
        }

        protected ITextAnalyzer TextAnalyzer { get; set; }

        protected const int suffix = 3;

        protected string path;

        protected List<List<string>> paragraphs;
        protected Dictionary<string, List<List<string>>> sentences;
        protected Dictionary<string, List<string>> words;

        protected Random random = new Random();

        public void SetData(string _path)
        {
            path = _path;
            using StreamReader sr = new StreamReader(path);
            string text = sr.ReadToEnd();
            TextAnalyzer.SetText(text);
            paragraphs.AddRange(TextAnalyzer.GetParagraphs());
            List<(string, List<string>)> textSentences = TextAnalyzer.GetSentences();
            foreach ((string punctuation, List<string> words) sentence in textSentences)
            {
                if (!sentences.ContainsKey(sentence.punctuation))
                    sentences.Add(sentence.punctuation, new List<List<string>>());
                sentences[sentence.punctuation].Add(sentence.words);
            }
            List<string> textWords = TextAnalyzer.GetWords();
            foreach (string word in textWords)
            {
                string wordSuffix = GetSuffix(word);
                if (!words.ContainsKey(wordSuffix))
                    words.Add(wordSuffix, new List<string>());
                words[wordSuffix].Add(word);
            }
        }

        protected string GetSuffix(string word)
        {
            while (word.Length < suffix)
                word = " " + word;
            string wordSuffix = "";
            for (int i = suffix; i > 0; i--)
                wordSuffix += word[^i];
            return wordSuffix;
        }

        protected string GetWord(string word)
        {
            string wordSuffix = GetSuffix(word);
            if (words.ContainsKey(wordSuffix))
                if (words[wordSuffix].Count > 0)
                    return words[wordSuffix][random.Next(words[wordSuffix].Count)];
            return word;
        }

        protected List<string> GetSentenceTemplate(string punctuation)
        {
            if (sentences.ContainsKey(punctuation))
                if (sentences[punctuation].Count > 0)
                    return sentences[punctuation][random.Next(sentences[punctuation].Count)];
            return new List<string>();
        }

        protected string FirstSymbolToUpper(string sentence)
        {
            return sentence.Length > 0 ? (sentence.Substring(0, 1).ToUpper() + (sentence.Length > 1 ? sentence.Substring(1) : "")) : "";
        }

        public string GetSentence(string punctuation)
        {
            List<string> template = GetSentenceTemplate(punctuation);
            string sentence = "";
            for (int i = 0; i < template.Count; i++)
                sentence += GetWord(template[i]);
            return FirstSymbolToUpper(sentence + punctuation);
        }

        protected List<string> GetParagraphTemplate()
        {
            if (paragraphs.Count > 0)
                return paragraphs[random.Next(paragraphs.Count)];
            return new List<string>();
        }

        public string GetParagraph()
        {
            List<string> template = GetParagraphTemplate();
            string paragraph = "";
            for (int i = 0; i < template.Count; i++)
            {
                paragraph += GetSentence(template[i]) + " ";
            }
            return paragraph;
        }

        public string GetText(int paragraphs)
        {
            string text = "";
            for (int i = 0; i < paragraphs; i++)
                text += GetParagraph() + "\r\n";
            return text;
        }
    }
}
