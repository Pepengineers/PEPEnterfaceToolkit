using System;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Implementation
{
	public abstract class BaseCommand : IBaseCommand
	{
		private readonly Func<bool> canExecute;
		private bool? previousCanExecuteState;

		protected BaseCommand(Func<bool> canExecute)
		{
			this.canExecute = canExecute;
		}

		public virtual event EventHandler<bool> CanExecuteChanged;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RaiseCanExecuteChanged()
		{
			var canExecute = CanExecute();
			if (previousCanExecuteState == canExecute) return;

			previousCanExecuteState = canExecute;
			CanExecuteChanged?.Invoke(this, canExecute);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual bool CanExecute()
		{
			return canExecute == null || canExecute();
		}
	}
}