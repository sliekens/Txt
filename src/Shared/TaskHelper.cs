namespace TextFx
{
    using System.Threading.Tasks;

    internal sealed class TaskHelper
    {
        internal static readonly Task CompletedTask = Task.FromResult<object>(null);
    }
}