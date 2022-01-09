using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoConsole.Model
{
   public class ConviteDoEvento
    {
        public IOrganizationService Service { get; set; }

        public ConviteDoEvento(IOrganizationService Service)
        {
            this.Service = Service;
        }


        public void ExecuteMultipleRequestConviteDoEvento()
        {
            ExecuteMultipleRequest executeMultipleResquest = new ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = false,
                    ReturnResponses = false
                }
            };
            for (int i = 0; i < 10; i++)
            {
                Entity conviteDoEvento = new Entity("new_convitedoevento");
                conviteDoEvento["new_evento"] = new EntityReference("new_evento", new Guid("255afa4c-314f-ec11-8f8e-000d3ac1044f"));
                conviteDoEvento["new_cliente"] = new EntityReference("account", new Guid("a84978ab-214e-ec11-8f8e-000d3ac1044f"));

                CreateRequest createRequest = new CreateRequest()
                {
                    Target = conviteDoEvento

                };
                executeMultipleResquest.Requests.Add(createRequest);
            }

            ExecuteMultipleResponse executeMultipleResponse = (ExecuteMultipleResponse)this.Service.Execute(executeMultipleResquest);

            foreach (var responses in executeMultipleResponse.Responses)
            {
                if (responses.Fault != null)
                {
                    Console.WriteLine(responses.Fault.ToString());

                }

            }

        }
    }
}
