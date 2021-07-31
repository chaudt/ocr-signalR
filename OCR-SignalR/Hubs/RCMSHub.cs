using Microsoft.AspNetCore.SignalR;
using OCR_SignalR.Services;
using OpenTracing;
using OpenTracing.Propagation;
using OpenTracing.Tag;
using RCMS.Commons.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OCR_SignalR.Hubs
{
    public class RCMSHub : Hub
    {
        #region Defines
        private readonly ISignalRAuthorization _signalRAuthorization;
        private readonly ITracer _tracer;
        private readonly IHubWrapper _hubWrapper;
        #endregion

        #region C'tor
        public RCMSHub(ISignalRAuthorization signalRAuthorization, ITracer tracer, IHubWrapper hubWrapper)
        {
            this._signalRAuthorization = signalRAuthorization;
            this._tracer = tracer;
            this._hubWrapper = hubWrapper;
        }
        #endregion

        public async Task InitializeAsync(string token)
        {
            //RCMS.Commons.Utils.Display.Print($"-->InitializeAsync:{token}");
            using (var scope = _tracer.BuildSpan("initialize-async").StartActive(finishSpanOnDispose: true))
            {
                var span = scope.Span.SetTag(Tags.SpanKind, Tags.SpanKindClient);

                var dictionary = new Dictionary<string, string>();
                _tracer.Inject(span.Context, BuiltinFormats.TextMap, new TextMapInjectAdapter(dictionary));

                if (string.IsNullOrWhiteSpace(token))
                {
                    await DisconnectAsync();
                }
                try
                {
                    var jwt = this._signalRAuthorization.Initialization(token);
                    //RCMS.Commons.Utils.Display.Print($"-->InitializeAsync:jwt{jwt.ToJsons()}");

                    if (jwt == null)
                    {
                        await DisconnectAsync();

                        return;
                    }
                    var group = jwt.GroupName;
                    await Groups.AddToGroupAsync(Context.ConnectionId, group);
                    await ConnectAsync();
                }
                catch
                {
                    await DisconnectAsync();
                }

                span.Log("kết nối tới signalR thành công");
                span.Finish();
            }
        }
        private async Task ConnectAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("connected", "Connect is success.");
        }

        private async Task DisconnectAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("disconnected", "Connect is failed.");
        }

       
    }
}
