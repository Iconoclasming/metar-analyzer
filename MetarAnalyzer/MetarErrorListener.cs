using Antlr4.Runtime;

namespace MetarAnalyzer
{
    public class MetarErrorListener : IAntlrErrorListener<object>, IAntlrErrorListener<int>
    {
        public ValidationResult ValidationResult { get; } = new ValidationResult();

        public void SyntaxError(IRecognizer recognizer, object offendingSymbol, int line, int charPositionInLine, string msg,
            RecognitionException e)
        {
            ValidationResult.Errors.Add(msg);
        }

        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg,
            RecognitionException e)
        {
            ValidationResult.Errors.Add(msg);
        }
    }
}
