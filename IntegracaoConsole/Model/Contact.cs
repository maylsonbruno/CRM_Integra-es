using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoConsole.Model
{
   public class Contact
    {

        public IOrganizationService Service { get; set; }

        public string TableName = "contact";
        public Contact(IOrganizationService service)
        {
            this.Service = service;

        }
        public EntityCollection RetrieveMultipleContactsByAccount(Guid accontId)
        {
            QueryExpression queryContacts = new QueryExpression(this.TableName);
            queryContacts.ColumnSet.AddColumns("emailaddress1", "parentcustomerid");
            queryContacts.ColumnSet.AddColumns("fullname");
            queryContacts.Criteria.AddCondition("parentcustomerid",ConditionOperator.Equal,accontId);

            queryContacts.AddLink("account", "parentcustomerid", "accountid", JoinOperator.Inner);
            queryContacts.LinkEntities[0].Columns.AddColumns("telephone1","new_tipodaconta","new_totaldeoportunidades","new_valortotaldeoportunidades");
            queryContacts.LinkEntities[0].EntityAlias = "conta";
            return this.Service.RetrieveMultiple(queryContacts);
        }

        public EntityCollection RetrieveMultipleContactsByAccountFetch(Guid accountId)
        {
        string fetchXML = $@"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
  <entity name='contact'>
    <attribute name='fullname' />
    <attribute name='contactid' />
    <order attribute='fullname' descending='false' />
    <filter type='and'>
      <condition attribute='parentcustomerid' operator='eq' uiname='FYI Treinamentos' uitype='account' value='{accountId}' />
    </filter>
  </entity>
</fetch>";

            return this.Service.RetrieveMultiple(new FetchExpression(fetchXML));
        }



        public object RetrieveMultipleContactsByAccountLinq(Guid accountid)
        {
            var context = new OrganizationServiceContext(this.Service);

            var resultado = (from contact in context.CreateQuery("contact")
                             join account in context.CreateQuery("account")
                             on contact["parentcustomerid"] equals account["accountid"]
                             where ((EntityReference)contact["parentcustomerid"]).Id == accountid
                             select new  { 
                                 ContactName = contact["fullname"].ToString(),
                                 ContactEmail = contact.Contains("emailaddress1") ? contact["emailaddress1"].ToString() : string.Empty
                             }).ToList();
            foreach(var contact in resultado)
            {
                Console.WriteLine(contact.ContactName);
                Console.WriteLine(contact.ContactEmail);
            }
            return resultado;
        }
    }
}
