using NUnit.Framework;
using PasswordLibrary;

namespace TestPassword;

public class PasswordsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void StrongPasswordTest()
    {
        Assert.AreEqual(new StrongPassword("").CheckPasswordOnStrong(), PasswordType.Bad);
        Assert.AreEqual(new StrongPassword("12345678").CheckPasswordOnStrong(), PasswordType.Weak);
        Assert.AreEqual(new StrongPassword("ASDasdasd").CheckPasswordOnStrong(), PasswordType.Medium);
        Assert.AreEqual(new StrongPassword("ASDasdasd1").CheckPasswordOnStrong(), PasswordType.Good);
        Assert.AreEqual(new StrongPassword("ASDasdasd1!").CheckPasswordOnStrong(), PasswordType.Strong);
    }

    [Test]
    public void PasswordTest()
    {
        Assert.IsTrue(new Password("ok").CheckPassword("ok"));
        Assert.IsFalse(new Password("123").CheckPassword("ok"));
    }
}