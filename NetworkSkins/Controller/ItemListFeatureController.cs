﻿using System.Collections.Generic;

namespace NetworkSkins.Controller
{
    public abstract class ItemListFeatureController<T> : FeatureController
    {
        public override bool Enabled => Items.Count > 1;

        /// <summary>
        /// Items available for selection.
        /// This can include a "None" item, simple items and variant items.
        /// </summary>
        public List<Item> Items { get; private set; } = new List<Item>();

        /// <summary>
        /// Default item (usually the list of modifiers will be empty when this item is selected).
        /// </summary>
        public Item DefaultItem { get; private set; } = null;

        /// <summary>
        /// Currently selected item.
        /// </summary>
        public Item SelectedItem { get; private set; } = null;

        public void OnSelectedItemChanged(Item selected)
        {
            if (SelectedItem == selected) return;

            SelectedItem = selected;

            SaveSelectedItem();

            OnChanged();
        }

        protected override void Build()
        {
            Items = BuildItems(out var defaultItem);
            DefaultItem = defaultItem;
            SelectedItem = LoadSelectedItem() ?? defaultItem;
        }

        protected abstract List<Item> BuildItems(out Item defaultItem);

        #region Active Selection Data
        protected abstract string SelectedItemKey { get; }

        private Item LoadSelectedItem()
        {
            var selectedId = ActiveSelectionData.Instance.GetValue(Prefab, SelectedItemKey);
            if (selectedId == null) return null;

            foreach (var item in Items)
            {
                if (item.Id == selectedId)
                {
                    return item;
                }
            }

            return null;
        }

        private void SaveSelectedItem()
        {
            if (SelectedItem != DefaultItem)
            {
                ActiveSelectionData.Instance.SetValue(Prefab, SelectedItemKey, SelectedItem.Id);
            }
            else
            {
                ActiveSelectionData.Instance.ClearValue(Prefab, SelectedItemKey);
            }
        }
        #endregion

        #region Items
        public abstract class Item
        {
            public readonly string Id;

            protected Item(string id)
            {
                Id = id;
            }
        }

        public class SimpleItem : Item
        {
            public T Value { get; set; }

            public SimpleItem(string id, T value) : base(id)
            {
                Value = value;
            }
        }

        public class DefaultVariantItem : Item
        {
            public DefaultVariantItem() : base("#DEFAULT") {}
        }
        #endregion
    }
}