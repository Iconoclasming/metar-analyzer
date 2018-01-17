using System;
using System.Text;
using Antlr4.Runtime;
using MetarAnalyzer;

namespace MetarAnalyzerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            var stringBuilder = new StringBuilder();
            Console.WriteLine("Input a METAR message:");
            while ((input = Console.ReadLine()) != "\u0004")
            {
                stringBuilder.AppendLine(input);
            }
            var inputStream = new AntlrInputStream(stringBuilder.ToString());
            var lexer = new MetarLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new MetarParser(commonTokenStream);
            var context = parser.message();
            Console.WriteLine(context.GetText());
            Console.Read();
        }
    }
}
