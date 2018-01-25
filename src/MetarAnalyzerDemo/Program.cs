using System;
using System.Text;
using MetarAnalyzer;

namespace MetarAnalyzerDemo
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var stringBuilder = new StringBuilder();
            Console.WriteLine("Input a METAR message:");
            stringBuilder.AppendLine(Console.ReadLine());;
            var analysisResult = MetarAnalyzerImpl.Analyze(stringBuilder.ToString());
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (analysisResult != null)
            {
                Console.WriteLine(analysisResult.ToString());
            }
            Console.Read();
        }
    }
}
