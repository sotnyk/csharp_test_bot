using System;
using Xunit;

namespace csharp_test_bot.Tests
{
    public class CharsFlipperTest
    {
        [Fact]
        public void SimpleCheck()
        {
            var orig = "мир сошел с ума опять";
            var expected = "qшʁuо ɐwʎ ɔ vǝmоɔ dиw";
            var actual = CharsFlipper.Flip(orig);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UpperCaseCheck()
        {
            var orig = "М";
            var expected = "w";
            var actual = CharsFlipper.Flip(orig);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PunctuationsCheck()
        {
            var orig = ".,'!?";
            var expected = "¿¡,'˙";
            var actual = CharsFlipper.Flip(orig);

            Assert.Equal(expected, actual);
        }
    }
}
