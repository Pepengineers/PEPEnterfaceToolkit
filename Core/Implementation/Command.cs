using System;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Implementation
{
	public class Command : BaseCommand, ICommand
	{
		private readonly Action action;

		public Command(Action action, Func<bool> canExecute = null) : base(canExecute)
		{
			this.action = action;
		}

		public void Execute()
		{
			action?.Invoke();
		}
	}

	public class Command<T> : BaseCommand, ICommand<T>
	{
		private readonly Action<T> action;

		public Command(Action<T> action, Func<bool> canExecute = null) : base(canExecute)
		{
			this.action = action;
		}

		public void Execute(T parameter)
		{
			action?.Invoke(parameter);
		}
	}
}