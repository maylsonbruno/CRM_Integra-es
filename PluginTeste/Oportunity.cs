using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginTeste
{
    public class Oportunity : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context =(IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity opportunity = (Entity)context.InputParameters["Target"];
            string contaDaOportunidade = ((EntityReference)opportunity["parentcustomerid"]).Name;

                throw new InvalidPluginExecutionException($"O nome da conta e {contaDaOportunidade}");

        }
    }
}
