﻿using PEPEngineers.PEPEnterfaceToolkit.Core;
using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElementWrappers
{
    public class BindableLabelWrapper : BindablePropertyElement
    {
        private readonly BindableLabel _label;
        private readonly IReadOnlyProperty<string> _textProperty;

        public BindableLabelWrapper(BindableLabel label, IObjectProvider objectProvider) : base(objectProvider)
        {
            _label = label;
            _textProperty = GetReadOnlyProperty<string>(label.BindingTextPath);
        }

        public override void UpdateValues()
        {
            _label.text = _textProperty.Value;
        }
    }
}