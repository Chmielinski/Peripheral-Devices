using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;

namespace scanner
{
    class ScannerClass
    {


        public DeviceInfo _deviceInfo;
        public ScannerClass(DeviceInfo info)
        {
            _deviceInfo = info;
        }


        public override string ToString()
        {
            return (string)_deviceInfo.Properties["Name"].get_Value();
        }
    }
}
