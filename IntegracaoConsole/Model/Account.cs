using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoConsole.Model
{
   public class Account
    {
        public string TableName = "account";

        public IOrganizationService Service { get; set; }
        public object ExecuteMultipleResquest { get; private set; }

        public Account(IOrganizationService service)
        {
            this.Service = service;
        }
        public void CreateAccount()
        {

            Entity account = new Entity(this.TableName);
            account["new_cnpj"] = "75.850.368/0001-48";
            account["name"] = "Conta criada na Model";
            account["new_tipodaconta"] = new OptionSetValue(100000000);
            account["new_totaldeoportunidades"] = 1;
            account["new_valortotaldeoportunidades"] = new Money(5000);
            account["primarycontactid"] = new EntityReference("contact", new Guid("a74978ab-214e-ec11-8f8e-000d3ac1044f"));
            Guid accountId = this.Service.Create(account);

            Console.WriteLine($"https://org21e7dc4b.crm2.dynamics.com/main.aspx?appid=26be5446-644d-ec11-8f8e-0022483848af&cmdbar=true&forceUCI=1&pagetype=entityrecord&etn=account&id={accountId}");
            Console.ReadKey();
        }
        public void UpdateAccount()
        {
            Entity account = new Entity(this.TableName);
            account.Id = new Guid("32d84913-fa4e-ec11-8f8e-000d3ac1044f");
           /* account["new_cnpj"] = null;
            account["new_tipodaconta"] = new OptionSetValue(100000001);
            account["new_valortotaldeoportunidades"] = new Money(15000);*/
            account["primarycontactid"] = new EntityReference("contact", new Guid("79ae8582-84bb-ea11-a812-000d3a8b3ec6"));
            this.Service.Update(account);
        }
        public void DeleteAccount()
        {
            this.Service.Delete(this.TableName,new Guid("161f170e-ea4e-ec11-8f8e-000d3ac1044f"));
        }

      
    }
}
