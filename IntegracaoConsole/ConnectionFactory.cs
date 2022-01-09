using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoConsole
{
     public class ConnectionFactory
    {

        public static IOrganizationService GetCrmService()
        {
            string connectionString =
                "AuthType=OAuth;" +
                "Username=adm@adm012720.onmicrosoft.com;" +
                "Password=teste@123;" +
                "Url=https://org21e7dc4b.crm2.dynamics.com/;" +
                "AppId=41ecdcc1-83fc-46d1-8d58-080f5adcf884;" +
                "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            return crmServiceClient.OrganizationWebProxyClient;
        }
    }
}
