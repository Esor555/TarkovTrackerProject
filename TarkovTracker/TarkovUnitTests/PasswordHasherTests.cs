using Microsoft.VisualStudio.TestTools.UnitTesting;
using TarkovTrackerBLL.Service;

namespace TarkovUnitTests
{
    [TestClass]
    public class PasswordHasherTests
    {
        [TestMethod]
        public void HashPassword_ValidPassword_ReturnsHashedPassword()
        {
            var password = "testpassword123";
            var hashedPassword = PasswordHasher.HashPassword(password);
            Assert.IsNotNull(hashedPassword);
            Assert.AreNotEqual(password, hashedPassword);
        }

        [TestMethod]
        public void HashPassword_EmptyPassword_ReturnsHashedPassword()
        {
            var password = "";
            var hashedPassword = PasswordHasher.HashPassword(password);
            Assert.IsNotNull(hashedPassword);
            Assert.AreNotEqual(password, hashedPassword);
        }

        [TestMethod]
        public void HashPassword_SamePassword_DifferentHashes()
        {
            var password = "testpassword123";
            var hash1 = PasswordHasher.HashPassword(password);
            var hash2 = PasswordHasher.HashPassword(password);
            Assert.AreNotEqual(hash1, hash2); // Should be different due to salt
        }

        [TestMethod]
        public void VerifyPassword_CorrectPassword_ReturnsTrue()
        {
            var password = "testpassword123";
            var hashedPassword = PasswordHasher.HashPassword(password);
            var result = PasswordHasher.VerifyPassword(password, hashedPassword);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VerifyPassword_WrongPassword_ReturnsFalse()
        {
            var password = "testpassword123";
            var wrongPassword = "wrongpassword";
            var hashedPassword = PasswordHasher.HashPassword(password);
            var result = PasswordHasher.VerifyPassword(wrongPassword, hashedPassword);
            Assert.IsFalse(result);
        }
    }
} 