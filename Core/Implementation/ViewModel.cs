using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Implementation
{
	public abstract class ViewModel : IViewModel
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected bool Set<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = default)
		{
			if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) return false;

			oldValue = newValue;
			OnPropertyChanged(propertyName);

			return true;
		}

		protected bool Set<TModel, T>(T oldValue, T newValue, TModel model, Action<TModel, T> callback,
			[CallerMemberName] string propertyName = default) where TModel : class
		{
			if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) return false;

			callback(model, newValue);
			OnPropertyChanged(propertyName);

			return true;
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}