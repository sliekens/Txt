using System;
using System.Linq;
using System.Reflection;

namespace Txt.ABNF
{
    public static class Registrations
    {
        public delegate void RegisterCallback(Type service, Type implementation, string lifestyle);

        public static void GetRegistrations(RegisterCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }
            var assembly = typeof(Registrations).GetTypeInfo().Assembly;
            var registrations = from type in assembly.ExportedTypes
                                where type.Name.EndsWith("LexerFactory", StringComparison.Ordinal)
                                let implementedInterfaces = type.GetTypeInfo().ImplementedInterfaces.ToList()
                                where implementedInterfaces.Count == 1
                                select new { Service = implementedInterfaces[01], Implementation = type };
            foreach (var registration in registrations)
            {
                callback(registration.Service, registration.Implementation, "singleton");
            }
        }
    }
}
