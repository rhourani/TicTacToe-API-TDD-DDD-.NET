using SSHTicTacToe.DTO;
using SSHTicTacToe.Services.AuthorizedKeysParserServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TicTacToe.TEST.System.Services
{
    public class TestAuthorizedKeysParserService
    {
        [Fact]
        public async Task TestInsertAuthorizedKeys()
        {
            // Arrange
            string filePath = "filePath.txt";
            string[] supportedKeyTypes = {
                "sk-ecdsa-sha2-nistp256@openssh.com",
                "ecdsa-sha2-nistp256",
                "ecdsa-sha2-nistp384",
                "ecdsa-sha2-nistp521",
                "sk-ssh-ed25519@openssh.com",
                "ssh-ed25519",
                "ssh-dss",
                "ssh-rsa"
            };

            string[] lines = new string[] { 
                "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQDBgMMrt8B4WmHv2C/EtYFVt9rzr0A2QJ37zjdgL11pJGpHg0+hKolJf+gY9M+B6ZDzPpAO6hJzAs+57bPECoOoyR5d6J5Uf9lNz6ZvtuHPb/zN3q/cU5mk6w/R6d5C6U5r9mHnx2a/zFmKdglhHpM/Cf24Dn++gLh01YFjTKuTZGzvB1IzOdz+lt8Wjy1QvL4t0sPtClX8WzHx7VuRUzE1QVu1fopOy7LnaMdwnGq3q/uCkUtlbR7zXJLwKiuPOpzLzldbNT7N/lu/quAsx7jKIVzfrN1BM+ZgHNQKjwZtL5QV7yDiX9qnVxEjMv26bzmZnDnKiR7wpmhvj dummy"
                ,"ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAIGwAkUgB6jKAZsXOQ2Uxo6PvT6WcKjCgT6M7w6vUJKnKU"
                ,"\"Ridvan is cool y'know!\" ssh-dss AAAAC3NzaC1lZDI1NTE5AAAAIGwAkUgB6jKAZsXOQ2Uxo6PvT6WcKjCgT6M7w6vUJKnKU"
            };

            File.WriteAllLines(filePath, lines);

            // Act
            var sut = new AuthorizedKeysParserService();

            var result = await sut.InsertAuthorizedKeys(filePath, supportedKeyTypes);

            // Assert
            Assert.False(result.isError);
            Assert.Equal("Keys imported from Authorized_keys file. File path: " + filePath, result.Message);

            // Clean up
            File.Delete(filePath);
        }

        [Fact]
        public async Task TestGetAuthorizedKeysList()
        {
            // Arrange
            string[] supportedKeyTypes = {
                "sk-ecdsa-sha2-nistp256@openssh.com",
                "ecdsa-sha2-nistp256",
                "ecdsa-sha2-nistp384",
                "ecdsa-sha2-nistp521",
                "sk-ssh-ed25519@openssh.com",
                "ssh-ed25519",
                "ssh-dss",
                "ssh-rsa"
            };

            // Act
            var sut = new AuthorizedKeysParserService();

            var result = await sut.GetAuthorizedKeysList();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<List<AuthorizedKeysResponseDto>>(result);
            Assert.All(result, key =>
            {
                Assert.NotNull(key.KeyType);
                Assert.NotNull(key.KeyData);
                Assert.NotNull(key.Comment);
                Assert.Contains(key.KeyType, supportedKeyTypes);
            });
        }
    }
}
