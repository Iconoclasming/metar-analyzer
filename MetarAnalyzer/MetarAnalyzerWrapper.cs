using System;
using Antlr4.Runtime;

namespace MetarAnalyzer
{
    public static class MetarAnalyzerWrapper
    {
        public static MetarAnalysisResult Analyze(string message)
        {
            return Analyze(message, out ValidationResult _);
        }

        private static MetarAnalysisResult Analyze(string message, out ValidationResult validation)
        {
            var errorListener = new MetarErrorListener();
            validation = errorListener.ValidationResult;

            var inputStream = new AntlrInputStream(message);
            var lexer = new MetarLexer(inputStream);
            lexer.AddErrorListener(errorListener);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new MetarParser(commonTokenStream);
            parser.AddErrorListener(errorListener);

            var context = parser.message();
            if (errorListener.ValidationResult.Errors.Count > 0)
            {
                return null;
            }
            var obsDateTimeStr = context.header().DATETIME().GetText();
            var obsDateTime = new DateTime(1, 1, int.Parse(obsDateTimeStr.Substring(0, 2)),
                int.Parse(obsDateTimeStr.Substring(2, 2)), int.Parse(obsDateTimeStr.Substring(4, 2)), 0,
                DateTimeKind.Utc);
            WindObservation windObservation = null;
            if (context.body_opt().body() != null)
            {
                var windDirection = int.Parse(context.body_opt().body().wind().WIND_DIRECTION().GetText());
                var windSpeed = int.Parse(context.body_opt().body().wind().WIND_SPEED().GetText());
                var windUnit = (SpeedUnit)Enum.Parse(typeof(SpeedUnit),
                    context.body_opt().body().wind().WIND_SPEED_UNIT().GetText(), true);
                var gust = 0;
                if (context.body_opt().body().wind().wind_gust_opt().WIND_GUST() != null)
                {
                    gust = int.Parse(context.body_opt().body().wind().wind_gust_opt().WIND_GUST().GetText()
                        .Substring(1, 2));
                }
                var directionChange = new WindObservation.DirectionChangeValues();
                if (context.body_opt().body().wind().wind_change_opt().wind_change() != null)
                {
                    var directionChangeFrom = int.Parse(context.body_opt().body().wind().wind_change_opt().wind_change()
                        .GetText().Substring(1, 3));
                    var directionChangeTo = int.Parse(context.body_opt().body().wind().wind_change_opt().wind_change()
                        .GetText().Substring(5, 3));
                    directionChange = new WindObservation.DirectionChangeValues(directionChangeFrom, directionChangeTo);
                }
                windObservation = new WindObservation(windDirection, new Speed(windSpeed, windUnit), gust,
                    directionChange);
            }
            var result = new MetarAnalysisResult(obsDateTime)
            {
                Wind = windObservation
            };
            return result;
        }
    }
}
