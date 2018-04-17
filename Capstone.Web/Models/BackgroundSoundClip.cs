using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class BackgroundSoundClip
    {
        public string AudioAddress { get; set; }
        public int AudioClipLengthInSeconds
        {
            get
            {
                return 60;
            }
        }
        public int MaxVolume { get; set; }
        public int MinVolume { get; set; }
        public int AudioId { get; set; }
    }
}