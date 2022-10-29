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

    [Test]
    public void EncryptTest()
    {
        Assert.AreEqual(new EncryptPassword("ok").Encrypt(3), "rn");
        Assert.AreEqual(new EncryptPassword("OK").Encrypt(3), "RN");

        Assert.AreEqual(new EncryptPassword("ok1").Encrypt(3), "rn1");
        Assert.AreEqual(new EncryptPassword("okв").Encrypt(3), "rnв");
        Assert.AreEqual(new EncryptPassword("ok").Encrypt(29), "rn");
        Assert.AreEqual(new EncryptPassword("z").Encrypt(1), "a");
        Assert.AreEqual(new EncryptPassword("Z").Encrypt(1), "A");
        Assert.AreEqual(new EncryptPassword("Z").Encrypt(2), "B");
        Assert.AreEqual(new EncryptPassword("a").Decode(1), "z");
        Assert.AreEqual(new EncryptPassword("A").Decode(1), "Z");


        Assert.AreEqual(new EncryptPassword("rn").Decode(29), "ok");
        Assert.AreEqual(new EncryptPassword("A").Decode(0), "A");
        Assert.AreEqual(new EncryptPassword("A").Decode(0), "A");


        // Assert.AreEqual(new EncryptPassword("a").Decode(1), "z");
    }
}