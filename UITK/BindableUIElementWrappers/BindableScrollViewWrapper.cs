using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements;
using UnityEngine.Pool;
using UnityEngine.UIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElementWrappers
{
	public abstract class BindableScrollViewWrapper<TItem, TData> : IBindableElement
		where TData : ICollectionItemData
	{
		private readonly VisualTreeAsset itemAsset;
		private readonly IReadOnlyProperty<ObservableCollection<TData>> itemsCollectionProperty;
		private Dictionary<Guid, VisualElement> itemAssets;
		private ObjectPool<VisualElement> itemAssetsPool;

		protected BindableScrollViewWrapper(BindableScrollView scrollView, VisualTreeAsset itemAsset,
			IObjectProvider objectProvider)
		{
			ScrollView = scrollView;
			this.itemAsset = itemAsset;

			itemsCollectionProperty =
				objectProvider.GetReadOnlyProperty<ObservableCollection<TData>>(scrollView.BindingItemsSourcePath,
					ReadOnlyMemory<char>.Empty);
		}

		protected BindableScrollView ScrollView { get; }

		protected ObservableCollection<TData> ItemsCollection { get; private set; }

		public virtual void Dispose()
		{
			itemAssets.Clear();
			itemAssetsPool.Dispose();
			ItemsCollection.CollectionChanged -= OnItemsCollectionChanged;
		}

		public bool CanInitialize => itemsCollectionProperty != null;

		public virtual void Initialize()
		{
			itemAssets = new Dictionary<Guid, VisualElement>();
			itemAssetsPool = new ObjectPool<VisualElement>(OnAssetsPoolCreateItem,
				actionOnRelease: OnAssetsPoolReleaseItem, actionOnDestroy: OnAssetsPoolDestroyItem);

			ItemsCollection = itemsCollectionProperty.Value;
			ItemsCollection.CollectionChanged += OnItemsCollectionChanged;

			AddItems(ItemsCollection);
		}

		protected virtual VisualElement MakeItem(out TItem item)
		{
			var itemAsset = itemAssetsPool.Get();
			if (itemAsset.userData != null)
			{
				item = (TItem)itemAsset.userData;
				return itemAsset;
			}

			item = OnMakeItem(itemAsset);
			itemAsset.userData = item;

			return itemAsset;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract TItem OnMakeItem(VisualElement itemAsset);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract void OnBindItem(TItem item, TData data);

		protected virtual void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
				foreach (var newItem in e.NewItems)
					AddItem((TData)newItem);
			else if (e.Action == NotifyCollectionChangedAction.Remove)
				foreach (var oldItem in e.OldItems)
					RemoveItem((TData)oldItem);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void AddItems(IEnumerable<TData> items)
		{
			foreach (var itemData in items) AddItem(itemData);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void AddItem(TData itemData)
		{
			var itemAsset = MakeItem(out var item);

			itemAssets.Add(itemData.Id, itemAsset);
			ScrollView.contentContainer.Add(itemAsset);

			OnBindItem(item, itemData);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void RemoveItems(IEnumerable<TData> items)
		{
			foreach (var itemData in items) RemoveItem(itemData);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void RemoveItem(TData itemData)
		{
			itemAssetsPool.Release(itemAssets[itemData.Id]);
		}

		private VisualElement OnAssetsPoolCreateItem()
		{
			return itemAsset.Instantiate();
		}

		private void OnAssetsPoolReleaseItem(VisualElement itemAsset)
		{
			itemAsset.RemoveFromHierarchy();
		}

		private void OnAssetsPoolDestroyItem(VisualElement itemAsset)
		{
			if (itemAsset.userData is IDisposable disposable) disposable.Dispose();
		}
	}
}