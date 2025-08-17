using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Services
{
    public class SsrsReportCredential : IReportServerCredentials
    {

        // local variable fornetwork credential.
        private string _userName;
        private string _password;
        private string _domainName;
        public SsrsReportCredential(string userName, string password, string domain)
        {
            _userName = userName;
            _password = password;
            _domainName = domain;
        }

        public WindowsIdentity ImpersonationUser => null;

        public ICredentials NetworkCredentials => string.IsNullOrEmpty(_domainName)
            ? new NetworkCredential(_userName, _password)
            : new NetworkCredential(_userName, _password, _domainName);

        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
        {
            // not use FormsCredentials unless you have implements acustom autentication.
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
}

