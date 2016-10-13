using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            GraphApiClient.CreateUser("your tenant", "your ClientId", "your client secret", "name of user");
        }
    }
}
