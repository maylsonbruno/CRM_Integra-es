using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginAacademiaCrm
{
    public class OpportunityCustom : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)// no Execute começar  a rodar o  codigo igual  o main
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity opportunity = GetOpportunityEntity(context);
            try
            {
                if (opportunity.Contains("parentaccountid"))
                {
                     UpdateAccount(context, service, opportunity);
                }
                else
                {
                    throw new InvalidPluginExecutionException($"Por favor preencha a conta da oportunidade");
                }
            }
            catch(Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.ToString());
            }

        }

            private static void UpdateAccount(IPluginExecutionContext context, IOrganizationService service, Entity opportunity)
        {
            Guid contaDaOportunidade = ((EntityReference)opportunity["parentaccountid"]).Id;

            Entity account = RetrieveAccount(service, contaDaOportunidade);
            int totalDeOportunidades = GetTotalDeOportunidades(service, contaDaOportunidade, account);

            if (context.MessageName == "Create" || context.MessageName == "Update")
            {
                account["new_totaldeoportunidades"] = totalDeOportunidades + 1;

                if (context.MessageName == "Update")
                {
                    Entity preOpportunityImage = context.PreEntityImages["PreImage"];

                    Entity preAccount = RetrieveAccount(service, ((EntityReference)preOpportunityImage["parentaccountid"]).Id);
                    int totalPreAccount = GetTotalDeOportunidades(service, preAccount.Id, preAccount);
                    preAccount["new_totaldeoportunidades"] = totalDeOportunidades - 1;
                    service.Update(preAccount);

                }

            }
            else
            {
                if (context.MessageName == "Delete")
                {
                    account["new_totaldeoportunidades"] = totalDeOportunidades - 1;
                }
            }
            service.Update(account);
        }

        private static Entity RetrieveAccount(IOrganizationService service, Guid contaDaOportunidade)
        {
            return service.Retrieve("account", contaDaOportunidade, new ColumnSet("new_totaldeoportunidades"));
        }

        private static int GetTotalDeOportunidades(IOrganizationService service, Guid contaDaOportunidade,Entity account)
        {
           
            return account.Contains("new_totaldeoportunidades") ? (int)account["new_totaldeoportunidades"] : 0;
        }

        private static Entity GetOpportunityEntity(IPluginExecutionContext context)
        {
            Entity opportunity = new Entity();

            if (context.MessageName == "Create" || context.MessageName =="Update")
            {
                opportunity = (Entity)context.InputParameters["Target"];
            }

            else
            {
                if (context.MessageName == "Delete")
                {
                    opportunity = (Entity)context.PreEntityImages["PreImage"];

                }

            }
            return opportunity;
        }
            
    }
}
