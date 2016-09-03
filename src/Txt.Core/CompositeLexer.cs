using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class CompositeLexer<TInner, TElement> : Lexer<TElement>
        where TInner : Element
        where TElement : Element
    {
        private readonly ILexer<TInner> innerLexer;

        private readonly Lazy<Func<TInner, TElement>> lazyFactory = new Lazy<Func<TInner, TElement>>(FactoryFactory);

        public CompositeLexer([NotNull] ILexer<TInner> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        protected override IReadResult<TElement> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var result = innerLexer.Read(scanner);
            if (!result.Success)
            {
                return new ReadResult<TElement>(SyntaxError.FromReadResult(result, context));
            }
            return new ReadResult<TElement>(lazyFactory.Value.Invoke(result.Element));
        }

        private static Func<TInner, TElement> FactoryFactory()
        {
            var elementType = typeof(TElement).GetTypeInfo();
            var innerType = typeof(TInner);
            var ctor = elementType.DeclaredConstructors.First(
                                      x =>
                                      {
                                          var parameters = x.GetParameters();
                                          if (parameters.Length != 1)
                                          {
                                              return false;
                                          }
                                          return parameters[0].ParameterType == innerType;
                                      });
            var parameterExpression = Expression.Parameter(innerType);
            var newExpression = Expression.New(ctor, parameterExpression);
            var lambda = Expression.Lambda<Func<TInner, TElement>>(newExpression, parameterExpression);
            return lambda.Compile();
        }
    }
}
