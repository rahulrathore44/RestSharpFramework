using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper
{
    public interface IJiraClient : IClient
    {
        void Login(IJiraUser user);
        void Logout();
    }

    public interface IJiraUser { }
}
