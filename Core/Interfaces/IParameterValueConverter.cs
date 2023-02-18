using System;
using System.Runtime.CompilerServices;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces
{
    public interface IParameterValueConverter : IValueConverter
    {
        Type TargetType { get; }
    }

    public interface IParameterValueConverter<out TTargetType> : IParameterValueConverter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TTargetType Convert(ReadOnlyMemory<char> parameter);
    }
}