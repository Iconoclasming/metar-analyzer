using System.Collections.Generic;

namespace MetarAnalyzer
{
    public class ValidationResult
    {
        public List<string> Errors { get; } = new List<string>();

        public override string ToString()
        {
            return $"{nameof(Errors)}:\n{Concat(Errors, '\n')}";
        }

        private static string Concat(IEnumerable<string> strings, char separator)
        {
            var str = string.Empty;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in strings)
            {
                str += s + separator;
            }
            str = str.TrimEnd(separator);
            return str;
        }
    }
}
