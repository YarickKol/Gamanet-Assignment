using Xunit;

namespace CommandDecoder.Test
{
    public class DecoderTest
    {
        [Theory]
        [InlineData("PT:Enter:E")]
        [InlineData("PS:1000, 1000:E")]
        [InlineData("PT::E")]
        [InlineData("PT:::E")]
        public void WhenCommandIsFull_Should_ReturnTrue(string command)
        {
            Assert.True(Decoder.CommandIsFull(command));
        }

        [Theory]
        [InlineData("")]
        [InlineData("T:Enter:E")]
        [InlineData("PT:Enter:")]
        [InlineData("PTEnter:E")]
        [InlineData("PT:EnterE")]       
        [InlineData("PTS:Enter:E")]
        public void WhenCommandIsNotFull_Should_ReturnFalse(string command)
        {
            Assert.False(Decoder.CommandIsFull(command));
        }
    }
}
