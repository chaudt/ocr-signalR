using Autofac;
using Microsoft.AspNetCore.Builder;
using RCMS.Commons.Utils;
using System.Threading.Tasks;

namespace OCR_SignalR.Registration
{

    public class ServiceConsumer : Singleton<ServiceConsumer>
    {
        public ContainerBuilder Alls(ContainerBuilder services)
        {
            

            #region V2
            //services.RegisterConsumer<MemoryTaskChanged>();
            
            #endregion
            return services;

        }

        public Task Listeners(IApplicationBuilder app)
        {
            
            
            #region V2
            //app.Listening<TaskGuidCollectionClosed>().Run();
            
            #endregion
            return Task.CompletedTask;
        }
    }
}
