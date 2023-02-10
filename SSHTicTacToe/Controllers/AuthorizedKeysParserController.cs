using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SSHTicTacToe.DTO;
using SSHTicTacToe.Services.AuthorizedKeysParserServices;

namespace SSHTicTacToe.Controllers
{
    [ApiController]
    [Route("api/v1/authorizedKeys")]
    public class AuthorizedKeysParserController : ControllerBase
    {
        private readonly IAuthorizedKeysParserService _authorizedKeysParserService;

        public AuthorizedKeysParserController(
            IAuthorizedKeysParserService authorizedKeysParserService)
        {
            _authorizedKeysParserService = authorizedKeysParserService;
        }


        [HttpGet]
        public async Task<IActionResult> GetKeys()
        {
            List<AuthorizedKeysResponseDto> AuthorizedKeys = await _authorizedKeysParserService.GetAuthorizedKeysList();

            List<AuthorizedKeysResponseDto> AuthorizedKeyDTO = new();

            foreach (var item in AuthorizedKeys)
            {
                AuthorizedKeyDTO.Add(new AuthorizedKeysResponseDto
                {
                    KeyId = item.KeyId,
                    UserId = item.UserId,
                    Options = item.Options,
                    KeyType = item.KeyType,
                    KeyData = item.KeyData,
                    Comment = item.Comment,
                    FingerPrint = item.FingerPrint,
                    Status = item.Status,
                    LastUsed = item.LastUsed,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                    ValidFrom = item.ValidFrom,
                    ValidTo = item.ValidTo,
                    HostId = item.HostId,
                    Ipadresses = item.Ipadresses

                });
            }

            return Ok(AuthorizedKeyDTO);
        }

        [HttpPost]
        [Route("ReadKeysFromFile")]
        public async Task<IActionResult> ReadKeysFromFile()
        {
            string filePath = @"C:\Users\xxx\Documents\GitHub\SSHTicTacToe\authorized_keys.txt";
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

            var res = await _authorizedKeysParserService.InsertAuthorizedKeys(filePath, supportedKeyTypes);
            return Ok(res);
        }



    }
}
