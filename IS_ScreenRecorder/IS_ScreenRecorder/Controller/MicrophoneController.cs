using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using CoreAudioApi;
using IS_ScreenRecorder.Global;

namespace IS_ScreenRecorder.Controller
{
    public class MicrophoneController : ApiController
    {
        // GET api/microphone 
        private MMDevice micDevice;
        public MicrophoneController()
        {
            MMDeviceEnumerator mMDeviceEnumerator = new MMDeviceEnumerator();
            MMDeviceCollection mMDeviceCollection = mMDeviceEnumerator.EnumerateAudioEndPoints(EDataFlow.eCapture, EDeviceState.DEVICE_STATE_ACTIVE);
            for (int i = 0; i < mMDeviceCollection.Count; i++)
            {
                MMDevice mMDevice = mMDeviceCollection[i];
                if (mMDevice.FriendlyName.ToLower() == "microphone")
                {
                    micDevice = mMDevice;
                }
            }
        }
        //GET microphone/checkmicstatus 
        [HttpGet]
        [ActionName("getmicrophonestatus")]
        public string GetMicrophoneStatus()
        {
            string isMicOn = "0";
            try
            {
                if (micDevice == null)
                {
                    throw new InvalidOperationException("Microphone not found by MicMute Library!");
                }
                isMicOn = micDevice.AudioEndpointVolume.Mute == true ? "0" : "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isMicOn;
        }

        // POST microphone/microphone 
        [HttpPost]
        [ActionName("setmicrophonestatus")]
        public void SetMicrophoneStatus([FromBody] string status)
        {
            try
            {
                if (micDevice == null)
                {
                    throw new InvalidOperationException("Microphone not found by MicMute Library!");
                }

                bool muteMic = status == "0" ? true : false;

                if (muteMic)
                    micDevice.AudioEndpointVolume.Mute = true;
                else
                    micDevice.AudioEndpointVolume.Mute = false;

                GlobalFunction.ChangeMicrophoneButtonIcon(muteMic);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
