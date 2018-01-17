using System;
using MetarAnalyzer;
using NUnit.Framework;

namespace MetarAnalyzerTests
{
    [TestFixture]
    public class MetarAnalyzerImplTests
    {
        #region Test cases
        [TestCase("METAR K13 141310Z=")]
        [TestCase("")]
        [TestCase("=")]
        [TestCase("METAR")]
        [TestCase("METAR=")]
        [TestCase("METAR K13")]
        [TestCase("METAR K13=")]
        [TestCase("METAR 141310Z=")]
        [TestCase("METAR A1BC 141310Z=")]
        [TestCase("SPECI OBCS 141310=")]
        [TestCase("ABCDE UNKL 141310Z=")]
        [TestCase("METAR KKFF 021220Z 2010G10MPS=")]
        [TestCase("METAR KKFF 021220Z 201MPS=")]
        [TestCase("METAR KKFF 021220Z 21MPS=")]
        [TestCase("METAR KKFF 021220Z 20010G1MPS=")]
        [TestCase("METAR UNKL 141310Z 14004MPS120V150=")]
        [TestCase("SPECI LNBW 081030Z 13010KT 110V 150=")]
        [TestCase("SPECI LNBW 081030Z 13010KT 110 V 150=")]
        [TestCase("SPECI LNBW 081030Z 13010KT 110 V150=")]
        [TestCase("SPECI LNBW 081030Z 13010KT 110150=")]
        [TestCase("SPECI LNBW 081030Z 13010KT 10150=")]
        // TODO: visibility cannot be concatenated
        //[TestCase("METAR JASD 030304Z 02050MPS 30001200NW=")]
        // TODO: index must contain 4 chars total
        //[TestCase("METAR ABC 141310Z=")]
        #endregion
        public void Analyze_MessageContainsError_ReturnsNull(string message)
        {
            var validationResult = new ValidationResult();
            var result = MetarAnalyzerImpl.Analyze(message, out validationResult);
            Assert.IsNull(result);
            Assert.AreNotEqual(0, validationResult.Errors.Count);
        }

        [TestCase("METAR BMFF 200515Z=", "BMFF")]
        public void Analyze_ParsesIndexWithOnlyFourChars(string message, string index)
        {
            var result = MetarAnalyzerImpl.Analyze(message);
            Assert.AreEqual(index, result.Index);
        }

        [TestCase("METAR JASS 161540Z=", 16, 15, 40)]
        public void Analyze_ReturnsCorrectDateTime(string message, int expectedDay, int expectedHour,
            int expectedMinute)
        {
            var expectedObservationDateTime = new DateTime(1, 1, expectedDay, expectedHour, expectedMinute, 0,
                DateTimeKind.Utc);
            var result = MetarAnalyzerImpl.Analyze(message);
            Assert.AreEqual(expectedObservationDateTime, result.ObservationDateTime);
        }
        
        [TestCase("SPECI KYLT 200455Z NIL=")]
        [TestCase("SPECI KYLT 200455Z=")]
        public void Analyze_MessageIsNil_WindIsNull(string message)
        {
            var validationResult = new ValidationResult();
            var result = MetarAnalyzerImpl.Analyze(message, out validationResult);
            Assert.IsNull(result.Wind);
            Assert.AreEqual(0, validationResult.Errors.Count);
        }

        [TestCase("SPECI KYLT 200455Z 20010MPS=", 200, 10, SpeedUnit.Mps)]
        [TestCase("METAR JNSS 021220Z 12005KT=", 120, 05, SpeedUnit.Kt)]
        [TestCase("METAR COR JNSS 021220Z 12005KT=", 120, 05, SpeedUnit.Kt)]
        [TestCase("METAR KKFF 141310Z 05015G10MPS  =", 50, 15, SpeedUnit.Mps, 10)]
        [TestCase("METAR KKFF 141310Z 05015G10MPS=", 50, 15, SpeedUnit.Mps, 10)]
        [TestCase("SPECI LNBW 081030Z 13010KT 110V150=", 130, 10, SpeedUnit.Kt, 0, 110, 150)]
        public void Analyze_ReturnsCorrectWindObservation(string message, int direction, int speed,
            SpeedUnit speedUnit, int gust = 0, int directionChangeFrom = 0, int directionChangeTo = 0)
        {
            var expectedDirectionChange =
                new WindObservation.DirectionChangeValues(directionChangeFrom, directionChangeTo);
            var expectedWindObservation = new WindObservation(direction, new Speed(speed, speedUnit), gust,
                expectedDirectionChange);
            var result = MetarAnalyzerImpl.Analyze(message);
            Assert.AreEqual(expectedWindObservation, result.Wind);
        }

        [TestCase("METAR JASD 030304Z 02050MPS 3000 1200NW=", 3000, 1200, "NW")]
        public void Analyze_ReturnsCorrectVisibilityObservation(string message, int visibilityValue,
            int visibilityHeight, string visibilityDirection)
        {
            var expectedVisibilityObservation = new VisibilityObservation(visibilityValue, visibilityHeight,
                visibilityDirection);
            var result = MetarAnalyzerImpl.Analyze(message);
            Assert.AreEqual(expectedVisibilityObservation, result.Visibility);
        }

        [TestCase("METAR BRYR 041220Z 12005KT RA=", "RA")]
        [TestCase("METAR BRYR 041220Z 12005KT RASH=", "RASH")]
        // TODO: parse two words in "current weather" section
        //[TestCase("METAR BRYR 041220Z 12005KT RASH HZ=", "RASH HZ")]
        //[TestCase("METAR BRYR 041220Z 12005KT RASH FZDZ=", "RASH FZDZ")]
        public void Analyze_ParsesCurrentWeather(string message, string expectedCurrentWeather)
        {
            var result = MetarAnalyzerImpl.Analyze(message);
            Assert.AreEqual(expectedCurrentWeather, result.CurrentWeather);
        }
    }
}
