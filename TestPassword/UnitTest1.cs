using NUnit.Framework;
using PasswordLibrary;

namespace TestPassword;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.AreEqual(new StrongPassword().CheckPasswordOnStrong("asDadasd"), PasswordType.Medium);
    }
}