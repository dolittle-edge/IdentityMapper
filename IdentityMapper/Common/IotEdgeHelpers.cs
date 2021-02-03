using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common
{
    public class IotEdgeHelpers
    {
        /// <summary>
        /// Check if we're running in IoT Edge context or not
        /// </summary>
        /// <returns>True if we are running in IoT Edge context, false if not</returns>
        public static bool IsRunningInIotEdge()
        {
            return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("IOTEDGE_MODULEID"));
        }

    }
}
