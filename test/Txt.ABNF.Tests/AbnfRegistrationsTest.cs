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
            foreach (var registration in AbnfRegistrations.GetRegistrations(container.GetInstance))
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
    }
}
