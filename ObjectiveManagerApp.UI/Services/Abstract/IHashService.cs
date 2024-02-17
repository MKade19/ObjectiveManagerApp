namespace ObjectiveManagerApp.UI.Services.Abstract
{
    public interface IHashService
    {
        string GenerateHash(string stringToHash, out byte[] salt);
        bool VerifyHash(string stringToCompare, string hashedString, byte[]? salt);
    }
}
