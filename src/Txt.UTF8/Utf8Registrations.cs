using System.Collections.Generic;
using System.Reflection;
using Txt.Core;

namespace Txt.UTF8
{
    public class Utf8Registrations : Registrations
    {
        public static IEnumerable<Registration> GetRegistrations(GetInstanceDelegate getInstance)
        {
            return GetRegistrations(typeof(Utf8Registrations).GetTypeInfo().Assembly, getInstance);
        }
    }
}
