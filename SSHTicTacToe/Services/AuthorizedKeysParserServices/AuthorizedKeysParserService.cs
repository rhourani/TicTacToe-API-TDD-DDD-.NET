
using SSHTicTacToe.DTO;
using SSHTicTacToe.Models;
using System.Security.Cryptography;
using System.Text;

namespace SSHTicTacToe.Services.AuthorizedKeysParserServices
{
    public class AuthorizedKeysParserService : IAuthorizedKeysParserService
    {
        public AuthorizedKeysParserService()
        {
            //TODO convert the db connection to dependency injection
        }

        public Task<CustomError> InsertAuthorizedKeys(string filePath, string[] supportedKeyTypes)
        {
            //File not found expetion and different errors, is handled in ErrorHandlerMiddleware class
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (supportedKeyTypes.Any(kt => line.Contains(kt)) && !line.StartsWith("#"))
                {
                    string[] elements = line.Split();

                    Span<byte> buffer = new Span<byte>(new byte[elements[elements.Length - 2].Length]);
                    bool isBase64 = Convert.TryFromBase64String(elements[elements.Length - 2], buffer, out int bytesParsed);
                    if (isBase64)
                    {
                        //Create a fingerpriont for the key
                        string fingerPrint = CreateFingerPrint(elements[elements.Length - 2]);

                        string[] optionss = new string[elements.Length - 3];
                        Array.Copy(elements, 0, optionss, 0, elements.Length - 3);
                        using (var client = new SshContext())
                        {
                            client.Add(new AuthorizedKey
                            {
                                Options = string.Join(", ", optionss),
                                KeyType = elements[elements.Length - 3],
                                KeyData = elements[elements.Length - 2],
                                Comment = elements[elements.Length - 1],
                                Status = (int)KeyStatus.Active,
                                CreatedAt = DateTime.UtcNow,
                                FingerPrint = fingerPrint,

                            });
                            client.SaveChanges();
                        }
                    }
                }
            }

            return Task.FromResult(new CustomError
            {
                isError = false,
                Message = "Keys imported from Authorized_keys file. File path: " + filePath //TODO Get rid of hard codded messages by putting them in resource file
            });
        }



        public Task<List<AuthorizedKeysResponseDto>> GetAuthorizedKeysList()
        {
            List<AuthorizedKeysResponseDto> authorizedKeysDto = new List<AuthorizedKeysResponseDto>();
            List<AuthorizedKey> authorizedKeys = new List<AuthorizedKey>();

            using (var client = new SshContext())
            {
                authorizedKeys = client.AuthorizedKeys.ToList();
            }

            foreach (var key in authorizedKeys)
            {
                authorizedKeysDto.Add(new AuthorizedKeysResponseDto
                {
                    KeyId = key.KeyId,
                    UserId = key.UserId,
                    Options = key.Options,
                    KeyType = key.KeyType,
                    KeyData = key.KeyData,
                    Comment = key.Comment,
                    FingerPrint = key.FingerPrint,
                    Status = key.Status,
                    LastUsed = key.LastUsed,
                    CreatedAt = key.CreatedAt,
                    UpdatedAt = key.UpdatedAt,
                    ValidFrom = key.ValidFrom,
                    ValidTo = key.ValidTo,
                    HostId = key.HostId,
                    Ipadresses = key.Ipadresses
                });
            }
            return Task.FromResult(authorizedKeysDto);
        }

        /// <summary>
        /// Return finger print for a key
        /// Copied from here https://www.techiedelight.com/generate-md5-hash-of-string-csharp/
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string CreateFingerPrint(string key)
        {
            StringBuilder sb = new StringBuilder();

            // Initialize a MD5 hash object
            using (MD5 md5 = MD5.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(key));

                // Convert the byte array to string format
                foreach (byte b in hashValue)
                {
                    sb.Append($"{b:X2}"); //X2 is the formatting of two hexdecimal
                }
            }

            return sb.ToString();
        }
    }
}