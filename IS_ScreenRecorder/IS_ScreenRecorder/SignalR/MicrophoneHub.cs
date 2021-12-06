using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS_ScreenRecorder.Global;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace IS_ScreenRecorder.SignalR
{
    [HubName("MicrophoneHub")]
    public class MicrophoneHub : Hub
    {
        public void SendMicrophoneStatus(string status)
        {
            //await Clients.All.SendAsync("ReceivedMicrophoneStatus", status);
            Clients.All.ReceivedMicrophoneStatus(status);
        }
    }
}
