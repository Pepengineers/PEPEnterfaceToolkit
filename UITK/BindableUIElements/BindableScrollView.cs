﻿using UnityEngine.UIElements;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements
{
    public class BindableScrollView : ScrollView, IBindableUIElement
    {
        public string BindingItemsSourcePath { get; set; }

        public new class UxmlFactory : UxmlFactory<BindableScrollView, UxmlTraits>
        {
        }

        public new class UxmlTraits : ScrollView.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _bindingItemsSourceAttribute = new()
                { name = "binding-items-source-path", defaultValue = "" };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                ((BindableScrollView) visualElement).BindingItemsSourcePath =
                    _bindingItemsSourceAttribute.GetValueFromBag(bag, context);
            }
        }
    }
}