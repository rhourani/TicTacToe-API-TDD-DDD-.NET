using SSHTicTacToe.DTO;

namespace SSHTicTacToe.Services.AuthorizedKeysParserServices
{
    public interface IAuthorizedKeysParserService
    {
        public Task<List<AuthorizedKeysResponseDto>> GetAuthorizedKeysList();
        public Task<CustomError> InsertAuthorizedKeys(string filePath, string [] supportedKeyTypes);
    }
}
