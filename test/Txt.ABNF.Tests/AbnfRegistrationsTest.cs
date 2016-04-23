using System;
using SimpleInjector;
using Xunit;

namespace Txt.ABNF
{
    public class AbnfRegistrationsTest
    {
        private readonly Container container = new Container();

        [Fact]
        public void Test()
        {
            foreach (var registration in AbnfRegistrations.GetRegistrations(GetInstance))
            {
                if (registration.Implementation != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Implementation);
                }
                if (registration.Instance != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Instance);
                }
                if (registration.Factory != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Factory);
                }
            }
            container.Verify();
        }

        private object GetInstance(Type service)
        {
            return container.GetInstance(service);
        }

        private void Register(Type service, Type implementation)
        {
            container.Register(service, implementation, Lifestyle.Singleton);
        }

        private void RegisterFactory(Type service, Func<object> implementationFactory)
        {
            container.Register(service, implementationFactory, Lifestyle.Singleton);
        }
    }
}
