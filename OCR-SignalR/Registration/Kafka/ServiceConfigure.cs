using Autofac;
using RCMS.Commons.Utils;

namespace OCR_SignalR.Registration
{
    public class ServiceConfigure : Singleton<ServiceConfigure>
    {
        public ContainerBuilder Alls(ContainerBuilder services)
        {

            #region V2
           
            //services.RegisterConfigure<TaskGuidCollectionClosed>();
            #endregion
            return services;
        }
    }
}
