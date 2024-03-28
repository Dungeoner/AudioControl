using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioDeviceManager.DllImport
{
    internal static class ComProvider
    {
        public static object GetPolicyConfig()
        {
            return Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid(Guids.POLICY_CONFIG_CID)));
        }
    }
}
