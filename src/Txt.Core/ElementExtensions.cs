using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Txt.Core
{
    /// <summary>
    ///     Contains <see cref="Element" /> extension methods that don't seem like they should be declared on the Element
    ///     abstract class.
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        ///     Gets a collection of terminal elements by recursively evaluating <see cref="GetTerminals" />.
        /// </summary>
        /// <returns>A collection of terminal elements.</returns>
        [NotNull]
        public static IEnumerable<Element> GetTerminals([NotNull] this Element instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            if (instance.Count == 0)
            {
                yield return instance;
            }
            else
            {
                foreach (var terminal in instance.SelectMany(t => t.GetTerminals()))
                {
                    yield return terminal;
                }
            }
        }
    }
}
