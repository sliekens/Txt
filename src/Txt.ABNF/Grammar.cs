using System;
using System.Collections.Generic;
using System.Linq;
using Txt.Core;

namespace Txt.ABNF
{
    public class Grammar : IInitializable
    {
        private readonly IDictionary<string, ILexer<Element>> rules = new Dictionary<string, ILexer<Element>>();

        private bool initialized;

        public void AddRule<T>(string ruleName, ILexer<T> rule)
            where T : Element
        {
            if (initialized)
            {
                throw new InvalidOperationException();
            }
            rules.Add(ruleName, rule);
        }

        public void Initialize()
        {
            if (initialized)
            {
                throw new InvalidOperationException();
            }
            foreach (var rule in rules.Values.OfType<IInitializable>())
            {
                rule.Initialize();
            }
            initialized = true;
        }

        public ILexer<Element> Rule(string ruleName)
        {
            return rules[ruleName];
        }

        public ILexer<T> Rule<T>(string ruleName)
            where T : Element
        {
            return rules[ruleName] as ILexer<T>;
        }
    }
}
