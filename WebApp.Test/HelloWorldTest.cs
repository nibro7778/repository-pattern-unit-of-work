using NUnit.Framework;

namespace WebApp.Test
{
    [TestFixture]
    public class HelloWorldTest
    {
        [Test]
        public void String_must_be_same()
        {
            const int sum = 5;
            Assert.AreEqual(2 + 3,sum);
        }
    }
}
