using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectiveManagerApp.UI.Services.Abstract
{
    public interface IHashService
    {
        string GetHash(string stringToHash, out byte[] salt);
        bool VerifyHash(string hashedString, string hash, byte[] salt);
    }
}
