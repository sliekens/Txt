namespace SLANG
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.ServiceLocation;

    internal class FakeServiceLocator : ServiceLocatorImplBase
    {
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return null;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return new List<object>(0);
        }
    }
}