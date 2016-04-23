using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Txt
{
    public class Registrations
    {
        public delegate object GetInstanceDelegate(Type service);

        public static IEnumerable<Registration> GetRegistrations(
            Assembly assembly,
            GetInstanceDelegate getInstance)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            if (getInstance == null)
            {
                throw new ArgumentNullException(nameof(getInstance));
            }
            var genericLexerType = typeof(ILexer<>);
            var genericLexerFactoryType = typeof(ILexerFactory<>).GetTypeInfo();
            var exportedTypes = assembly.ExportedTypes;
            var registrations = (from type in exportedTypes
                                 let typeInfo = type.GetTypeInfo()
                                 where !typeInfo.IsAbstract
                                 let lexerFactoryType =
                                     typeInfo.ImplementedInterfaces.SingleOrDefault(
                                         o =>
                                             o.IsConstructedGenericType &&
                                             Equals(o.GetGenericTypeDefinition().GetTypeInfo(), genericLexerFactoryType))
                                 where lexerFactoryType != null
                                 let factoryMethod = lexerFactoryType.GetTypeInfo().DeclaredMethods.Single()
                                 let lexerType =
                                     genericLexerType.MakeGenericType(lexerFactoryType.GenericTypeArguments[0])
                                 select
                                     new
                                     {
                                         FactoryService = lexerFactoryType,
                                         FactoryImplementation = type,
                                         LexerService = lexerType,
                                         FactoryMethod = factoryMethod
                                     }).ToList();
            foreach (var registration in registrations)
            {
                yield return new Registration(registration.FactoryService, registration.FactoryImplementation);
                yield return
                    new Registration(
                        registration.LexerService,
                        () => registration.FactoryMethod.Invoke(getInstance(registration.FactoryService), null));
            }
        }
    }
}
