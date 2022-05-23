using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class UserInPolicy
    {
        public string Name { get; private set; }
        public int RightFileServer { get; private set; }
        public int RightDatabaseServer { get; private set; }
        public int RightDocumentServer { get; private set; }
        public int RightWebServer { get; private set; }
        public string Password { get; private set; }
        public string PasswordSha256 { get; private set; }
        public string PasswordMD5 { get; private set; }
        public UserInPolicy(string name, int rightFileServer, int rightDatabaseServer, int
rightDocumentServer, int rightWebServer, string password, string passwordSha256, string passwordMD5)
        {
            Name = name;
            RightFileServer = rightFileServer;
            RightDatabaseServer = rightDatabaseServer;
            RightDocumentServer = rightDocumentServer;
            RightWebServer = rightWebServer;
            Password = password;
            PasswordSha256 = passwordSha256;
            PasswordMD5 = passwordMD5;
        }

    }
}
