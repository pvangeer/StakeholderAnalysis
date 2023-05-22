using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;

namespace StakeholderAnalysis.Storage
{
    public class VersionInfo
    {
        public static string CurrentDateTime => DateTime.Now.ToString("yyyy-MM-dd : hh:mm:ss");

        public static string CurrentUser
        {
            get
            {
                UserPrincipal userPrincipal = null;
                try
                {
                    userPrincipal = UserPrincipal.Current;
                }
                catch
                {
                    // No user principal. Not possible to retrieve the display name of the user.
                }

                return userPrincipal == null ?
                    $"({WindowsIdentity.GetCurrent().Name.Split('\\').Last()})"
                    : $"{UserPrincipal.Current.DisplayName} ({WindowsIdentity.GetCurrent().Name.Split('\\').Last()})";
            }
        }

        public static int Year => 23;
        
        public static int MajorVersion => 1;

        public VersionInfo()
        {
            DateCreated = CurrentDateTime;
            AuthorCreated = CurrentUser;
        }

        public string DateCreated { get; set; }

        public string AuthorCreated { get; set; }
    }
}