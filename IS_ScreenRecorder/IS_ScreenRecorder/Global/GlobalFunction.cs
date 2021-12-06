using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_ScreenRecorder.Global
{
    public static class GlobalFunction
    {
        public static System.Drawing.Bitmap MicrophoneButtonIcon;

        public static event EventHandler MicrophoneButtonIconChanged;

        public static void ChangeMicrophoneButtonIcon(bool isMute)
        {
            if (isMute)
            {
                MicrophoneButtonIcon = IS_ScreenRecorder.Properties.Resources.icons8_mute_unmute_100;
            }
            else
            {
                MicrophoneButtonIcon = IS_ScreenRecorder.Properties.Resources.icons8_microphone_100;
            }
            if (MicrophoneButtonIconChanged != null)
                MicrophoneButtonIconChanged(null, EventArgs.Empty);
        }
    }
}
