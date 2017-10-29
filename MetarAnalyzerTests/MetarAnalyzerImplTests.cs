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
        [TestCase("METAR ABC 141310Z=")]
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
        #endregion
        public void Analyze_MessageContainsError_ReturnsNull(string message)
        {
            var result = MetarAnalyzerImpl.Analyze(message);
            Assert.IsNull(result);
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

        [TestCase("SPECI KYLT 200455Z 20010MPS=", 200, 10, SpeedUnit.Mps)]
        [TestCase("METAR JNSS 021220Z 12005KT=", 120, 05, SpeedUnit.Kt)]
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
    }
}
