using IntegracaoConsole.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrganizationService service = ConnectionFactory.GetCrmService();
            // ConviteDoEvento convite = new ConviteDoEvento(service);
            // convite.ExecuteMultipleRequestConviteDoEvento();
            Contact contact = new Contact(service);

           EntityCollection resultado = contact.RetrieveMultipleContactsByAccountFetch(new Guid("a84978ab-214e-ec11-8f8e-000d3ac1044f"));
            foreach(Entity contactEn in resultado.Entities)
            {
                Console.WriteLine(contactEn["fullname"].ToString());
            }
            //foreach(string contactName in contactNames)
            //{
            //    Console.WriteLine( contactName );
            //}

            Console.ReadKey();


        }

        private static void ExampleRetrieveFromContact(Contact contact)
        {
            EntityCollection contactsCRM = contact.RetrieveMultipleContactsByAccount(new Guid("a84978ab-214e-ec11-8f8e-000d3ac1044f"));

            foreach (Entity contactCRM in contactsCRM.Entities)
            {
                string emailAddress = contactCRM.Contains("emailaddress1") ? contactCRM["emailaddress1"].ToString() : "Contato nao possui e-mail";
                string telephoneDaConta = ((AliasedValue)contactCRM["conta.telephone1"]).Value.ToString();
                EntityReference parentCustomerId = (EntityReference)contactCRM["parentcustomerid"];

                OptionSetValue tipoDaConta = (OptionSetValue)((AliasedValue)contactCRM["conta.new_tipodaconta"]).Value;
                int totalDeOportunidades = (int)((AliasedValue)contactCRM["conta.new_totaldeoportunidades"]).Value;
                Money valorTotalDeOportunidades = (Money)((AliasedValue)contactCRM["conta.new_valortotaldeoportunidades"]).Value;

                Console.WriteLine(telephoneDaConta);
                Console.WriteLine(contactCRM["fullname"].ToString());
                Console.WriteLine(emailAddress);

                Console.WriteLine(" o Nome da Conta e: ");
                Console.WriteLine(parentCustomerId.Name);
                Console.WriteLine(parentCustomerId.Id);
                Console.WriteLine(parentCustomerId.LogicalName);

                Console.WriteLine($" o tipo da conta e: {tipoDaConta.Value}");
                Console.WriteLine($" o total de oportunidades e: {totalDeOportunidades}");

                Console.WriteLine($"o valor total de oportunidades e : {valorTotalDeOportunidades.Value}");
            }
        }

    }
}
