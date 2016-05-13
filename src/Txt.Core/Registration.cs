using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class Registration
    {
        public Registration([NotNull] Type service, [NotNull] Type implementation)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }
            Service = service;
            Implementation = implementation;
        }

        public Registration([NotNull] Type service, [NotNull] object instance)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            Service = service;
            Instance = instance;
        }

        public Registration([NotNull] Type service, [NotNull] Func<object> factory)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            Service = service;
            Factory = factory;
        }

        public Func<object> Factory { get; }

        public Type Implementation { get; }

        public object Instance { get; }

        public Type Service { get; }
    }
}
