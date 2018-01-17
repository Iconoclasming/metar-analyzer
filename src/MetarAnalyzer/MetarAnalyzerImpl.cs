using System;
using System.Text.RegularExpressions;
using Antlr4.Runtime;

namespace MetarAnalyzer
{
    public static class MetarAnalyzerImpl
    {
        public static MetarAnalysisResult Analyze(string message)
        {
            return Analyze(message, out ValidationResult _);
        }

        public static MetarAnalysisResult Analyze(string message, out ValidationResult validation)
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
            var observationDateTimeStr = context.header().DATETIME().GetText();
            var observationDay = int.Parse(observationDateTimeStr.Substring(0, 2));
            var observationHour = int.Parse(observationDateTimeStr.Substring(2, 2));
            var observationMinute = int.Parse(observationDateTimeStr.Substring(4, 2));
            var observationDateTime = new DateTime(1, 1, observationDay, observationHour, observationMinute, 0,
                DateTimeKind.Utc);
            var index = context.header().WORD_ONLY_WITH_CHARS().GetText();
            var result = new MetarAnalysisResult(observationDateTime, index);
            if (context.body_opt().body() != null)
            {
                result.Wind = ParseWindObservation(context.body_opt().body().wind());
                result.Visibility = ParseVisibilityObservation(context.body_opt().body().visibility_opt().visibility());
                result.CurrentWeather = ParseCurrentWeather(context.body_opt().body().current_weather_opt().current_weather());
            }
            return result;
        }

        private static WindObservation ParseWindObservation(MetarParser.WindContext windContext)
        {
            if (windContext == null) return null;

            var regex = new Regex("(?<dir>[0-9]{3,3})(?<speed>[0-9]{2,2})(G(?<gust>[0-9]{2,2}))?(?<unit>(MPS|KT))[\\s]?((?<change_from>[0-9]{3,3})V(?<change_to>[0-9]{3,3}))?");
            var wind = windContext.WIND_WITH_CHANGE().GetText();
            var match = regex.Match(windContext.WIND_WITH_CHANGE().GetText());

            if (!match.Success) return null;

            var windDirection = int.Parse(match.Groups["dir"].Value);
            var windSpeed = int.Parse(match.Groups["speed"].Value);
            var windUnit = (SpeedUnit)Enum.Parse(typeof(SpeedUnit), match.Groups["unit"].Value, true);
            var gust = 0;
            if (match.Groups["gust"].Success)
            {
                gust = int.Parse(match.Groups["gust"].Value);
            }
            var directionChange = new WindObservation.DirectionChangeValues();
            if (match.Groups["change_from"].Success && match.Groups["change_to"].Success)
            {
                var directionChangeFrom = int.Parse(match.Groups["change_from"].Value);
                var directionChangeTo = int.Parse(match.Groups["change_to"].Value);
                directionChange = new WindObservation.DirectionChangeValues(directionChangeFrom,
                    directionChangeTo);
            }
            return new WindObservation(windDirection, new Speed(windSpeed, windUnit), gust, directionChange);
        }

        private static VisibilityObservation ParseVisibilityObservation(MetarParser.VisibilityContext visibilityContext)
        {
            if (visibilityContext == null) return null;

            int value = 0;
            value = int.Parse(visibilityContext.VISIBILITY().GetText());
            var height = 0;
            var direction = string.Empty;
            if (visibilityContext.visibility_direction_opt() != null)
            {
                var regex = new Regex("(?<height>[0-9]{4,4})(?<direction>[A-Z]{2,2})");
                var match = regex.Match(visibilityContext.visibility_direction_opt().VISIBILITY_DIRECTION().GetText());
                if (match.Success)
                {
                    height = int.Parse(match.Groups["height"].Value);
                    direction = match.Groups["direction"].Value;
                }
            }
            return new VisibilityObservation(value, height, direction);
        }

        private static string ParseCurrentWeather(MetarParser.Current_weatherContext currentWeatherContext)
        {
            return currentWeatherContext?.GetText() ?? string.Empty;
        }
    }
}
