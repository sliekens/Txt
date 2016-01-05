namespace TextFx
{
    using System.Diagnostics;

    /// <summary>Constains compile-time constants whose value is determined by the build configuration.</summary>
    internal sealed class SwitchOnBuild
    {
        /// <summary>
        /// DEBUG: <see cref="F:DebuggerBrowsableState.Collapsed"/>.
        /// Other: <see cref="F:DebuggerBrowsableState.Never"/>.
        /// </summary>
        internal const DebuggerBrowsableState DebuggerBrowsableState =
#if !DEBUG
            System.Diagnostics.DebuggerBrowsableState.Never;
#else
            System.Diagnostics.DebuggerBrowsableState.Collapsed;
#endif
    }
}
