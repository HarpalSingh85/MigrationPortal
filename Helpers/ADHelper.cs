using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MigrationPortal.Models;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace MigrationPortal.Helpers
{
    public class ADHelper
    {
        public UserModel GetUserDetails(string _userID, string _hostname, List<Adapter> adapters)
        {
            UserModel oPerson = new UserModel();

            try
            {
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    using (var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, _userID))
                    {
                        if (user != null)
                        {
                            oPerson.UserID = _userID;
                            oPerson.NetworkAdapters = new List<Adapter>();
                            oPerson.NetworkAdapters.AddRange(adapters);
                            oPerson.Hostname = _hostname;
                            DirectoryEntry dirEntry = new DirectoryEntry("LDAP://" + user.DistinguishedName);
                            oPerson.UserName = dirEntry?.InvokeGet("Name").ToString().Substring(3);
                            var _company = dirEntry?.InvokeGet("COMPANY");
                            if (_company != null)
                            {
                                oPerson.UserCompany = dirEntry?.InvokeGet("COMPANY").ToString();
                            }

                            var _userDept = dirEntry?.InvokeGet("department");
                            if (_userDept != null)
                            {
                                oPerson.UserDept = dirEntry?.InvokeGet("department").ToString();
                            }

                            dirEntry.Close();
                            user.Dispose();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oPerson;
            
        }
    }
}