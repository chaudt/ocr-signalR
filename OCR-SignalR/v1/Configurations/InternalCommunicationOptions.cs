using Autofac;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace OCR_SignalR.Configurations
{
    /// <summary>
    /// InternalCommunicationOptions
    /// </summary>
    public class InternalCommunicationOptions
    {
        /// <summary>
        /// SectionName
        /// </summary>
        public const string SectionName = "InternalComunication";
        /// <summary>
        /// ServiceUrls
        /// </summary>
        public Dictionary<string, string> ServiceUrls { get; set; }

    }
    /// <summary>
    /// InternalCommunicationOptionsExtensions
    /// </summary>
    public static class InternalCommunicationOptionsExtensions
    {
        /// <summary>
        /// Đăng ký các dịch vụ gRPC dành cho giao tiếp internal giữa các services.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder AddInternalCommunication(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = new InternalCommunicationOptions();
                options.ServiceUrls = configuration.GetSection(InternalCommunicationOptions.SectionName).Get<Dictionary<string, string>>();
                return options;
            }).SingleInstance();

            return builder;
        }
    }
}
