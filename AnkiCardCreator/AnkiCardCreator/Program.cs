using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnkiCardCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = string.Empty;

            var path = args.Single();
            var files = Directory.EnumerateFiles(path);
            foreach (var filePath in files)
            {
                var lines = File.ReadAllLines(filePath).Skip(1);

                var xs = GetQuestionsAndAnswers(lines).ToList();

                foreach (var x in xs)
                {
                    result += x.Item1 + "\t" + x.Item2 + Environment.NewLine;
                }
            }
        }

        private static IEnumerable<Tuple<string, string>> GetQuestionsAndAnswers(IEnumerable<string> lines)
        {
            var question = string.Empty;
            var answer = string.Empty;
            foreach (var l in lines)
            {
                if (!l.Any() && !question.Any())
                {
                    continue;
                }
                else if (!l.Any() && question.Any() && answer.Any())
                {
                    yield return Tuple.Create(question, answer);
                    question = string.Empty;
                    answer = string.Empty;
                }
                else if (!l.Any())
                {
                    throw new ArgumentException();
                }
                else if (!question.Any())
                {
                    question = l;
                }
                else if (!answer.Any())
                {
                    answer = l;
                }
                else
                {
                    answer += "<br />" + l;
                }
            }

            if (question.Any() && answer.Any())
            {
                yield return Tuple.Create(question, answer);
            }
        }
    }
}
