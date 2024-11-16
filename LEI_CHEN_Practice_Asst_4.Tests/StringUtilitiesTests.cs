using Xunit;
using Asst4.Utilities;

namespace Asst4.Tests
{
    public class StringUtilitiesTests
    {
        [Theory]
        [InlineData("A1A 1A1", "A1A1A1")]  // Standard format with space
        [InlineData("a1a1a1", "A1A1A1")]   // All lowercase
        [InlineData("A1A  1A1", "A1A1A1")] // Multiple spaces
        [InlineData("  A1A 1A1  ", "A1A1A1")] // Leading/trailing spaces
        [InlineData("a1a 1a1", "A1A1A1")]  // Mixed case with space
        [InlineData("A1A1A1", "A1A1A1")]   // Already correct format
        public void PostalCode_ValidInput_ReturnsFormattedPostalCode(string input, string expected)
        {
            var result = StringUtilities.PostalCode(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("")]             // Empty string
        [InlineData(null)]          // Null input
        [InlineData("123456")]      // All numbers
        [InlineData("ABCDEF")]      // All letters
        [InlineData("A1A1A")]       // Too short
        [InlineData("A1A1A1A")]     // Too long
        public void PostalCode_InvalidInput_ReturnsEmptyString(string input)
        {
            var result = StringUtilities.PostalCode(input);
            Assert.Equal(string.Empty, result);
        }
    }
}