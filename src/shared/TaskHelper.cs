using System.Threading.Tasks;

namespace Txt
{
    internal sealed class TaskHelper
    {
        internal static readonly Task CompletedTask = Task.FromResult<object>(null);
    }
}
