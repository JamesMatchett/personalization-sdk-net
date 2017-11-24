using NUnit.Framework;

namespace KenticoCloud.Personalization.Tests
{
    [TestFixture]
    public class RandomIdGeneratorTests
    {
        [Test]
        public void GenerateRandomRawId_GeneratesCorrectId()
        {
            var generator = new RandomIdGenerator();

            StringAssert.IsMatch("^[A-Za-z0-9]{16}$", generator.Generate());
        }
    }
}
