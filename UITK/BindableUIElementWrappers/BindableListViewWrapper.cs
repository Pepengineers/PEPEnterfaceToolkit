using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements;
using UnityEngine.UIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElementWrappers
{
	public abstract class BindableListViewWrapper<TItem, TData> : IBindableElement, IDisposable
	{
		private readonly VisualTreeAsset itemAsset;
		private readonly IReadOnlyProperty<ObservableCollection<TData>> itemsCollectionProperty;

		protected BindableListViewWrapper(BindableListView listView, VisualTreeAsset itemAsset,
			IObjectProvider objectProvider)
		{
			this.ListView = listView;
			this.itemAsset = itemAsset;
			itemsCollectionProperty =
				objectProvider.GetReadOnlyProperty<ObservableCollection<TData>>(listView.BindingItemsSourcePath,
					ReadOnlyMemory<char>.Empty);
		}

		protected BindableListView ListView { get; }

		protected ObservableCollection<TData> ItemsCollection { get; private set; }

		public virtual void Dispose()
		{
			ItemsCollection.CollectionChanged -= OnItemsCollectionChanged;

			ListView.makeItem -= MakeItem;
			ListView.bindItem -= BindItem;
		}

		public bool CanInitialize => itemsCollectionProperty != null;

		public virtual void Initialize()
		{
			ItemsCollection = itemsCollectionProperty.Value;
			ItemsCollection.CollectionChanged += OnItemsCollectionChanged;

			ListView.itemsSource = ItemsCollection;
			ListView.makeItem += MakeItem;
			ListView.bindItem += BindItem;
		}

		protected virtual VisualElement MakeItem()
		{
			var itemAsset = this.itemAsset.Instantiate();
			itemAsset.userData = OnMakeItem(itemAsset);

			return itemAsset;
		}

		protected virtual void BindItem(VisualElement itemAsset, int index)
		{
			if (index >= 0 && index < ItemsCollection.Count)
				OnBindItem((TItem)itemAsset.userData, ItemsCollection[index]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract TItem OnMakeItem(VisualElement itemAsset);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract void OnBindItem(TItem item, TData data);

		protected virtual void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			ListView.RefreshItems();
		}
	}
}