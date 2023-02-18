# PEPEnterfaceToolkit

A package that brings data-binding to your Unity project.

## :open_book: Table of Contents

- [About](#pencil-about)
  - [Samples](#samples)
- [Folder Structure](#cactus-folder-structure)
- [Installation](#gear-installation)
  - [IL2CPP restriction](#il2cpp-restriction)
- [Introduction](#ledger-introduction)
  - [ViewModel](#viewmodel)
  - [CanvasView](#canvasviewtbindingcontext)
  - [DocumentView](#documentviewtbindingcontext)
  - [Command](#command--commandt)
  - [AsyncCommand](#asynccommand--asynccommandt)
  - [AsyncLazyCommand](#asynclazycommand--asynclazycommandt)
  - [PropertyValueConverter](#propertyvalueconvertertsourcetype-ttargettype)
  - [ParameterValueConverter](#parametervalueconverterttargettype)
- [Quick start](#watch-quick-start)
- [How To Use](#rocket-how-to-use)
  - [Data-binding](#data-binding)
  - [Create custom control](#create-custom-control)
- [External Assets](#link-external-assets)
  - [UniTask](#unitask)
- [Benchmarks](#chart_with_upwards_trend-benchmarks)
- [Contributing](#bookmark_tabs-contributing)
  - [Discussions](#discussions)
  - [Report a bug](#report-a-bug)
  - [Request a feature](#request-a-feature)
  - [Show your support](#show-your-support)
- [License](#balance_scale-license)

## :pencil: About

The **PEPEnterfaceToolkit** allows you to use data binding to establish a connection between the app UI and the data it displays. This is a simple and consistent way to achieve clean separation of business logic from UI. Use the samples as a starting point for understanding how to utilize the package.

Key features:
- Runtime data-binding
- UI Toolkit & uGUI integration
- Multiple-properties binding
- Custom UI Elements support
- Compatible with [UniTask](https://github.com/Cysharp/UniTask)
- Mono & IL2CPP support

## :gear: Installation

You can install PEPEnterfaceToolkit in one of the following ways:

<summary>Install via Git URL</summary>

You need add `https://github.com/Pepengineers/PEPEnterfaceToolkit` to the Package Manager.

## :ledger: Introduction

The package contains a collection of standard, self-contained, lightweight types that provide a starting implementation for building apps using the MVVM pattern.

The included types are:
- [ViewModel](#viewmodel)
- [CanvasView\<TViewModel\>](#canvasviewtbindingcontext)
- [DocumentView\<TViewModel\>](#documentviewtbindingcontext)
- [Command & Command\<T\>](#command--commandt)
- [AsyncCommand & AsyncCommand\<T\>](#asynccommand--asynccommandt)
- [AsyncLazyCommand & AsyncLazyCommand\<T\>](#asynclazycommand--asynclazycommandt)
- [PropertyValueConverter\<TSourceType, TTargetType\>](#propertyvalueconvertertsourcetype-ttargettype)
- [ParameterValueConverter\<TTargetType\>](#parametervalueconverterttargettype)
- [ICommand & ICommand\<T\>](#command--commandt)
- [IAsyncCommand & IAsyncCommand\<T\>](#asynccommand--asynccommandt)
- [IPropertyValueConverter\<TSourceType, TTargetType\>](#propertyvalueconvertertsourcetype-ttargettype)
- [IParameterValueConverter\<TTargetType\>](#parametervalueconverterttargettype)

### ViewModel

The `ViewModel` is a base class for objects that are observable by implementing the `INotifyPropertyChanged` interface. It can be used as a starting point for all kinds of objects that need to support property change notification.

Key functionality:
- Provides a base implementation for `INotifyPropertyChanged`, exposing the `PropertyChanged` event
- Provides a series of `Set` methods that can be used to easily set property values from types inheriting from `ViewModel`, and to automatically raise the appropriate events

> **Note:** In case your viewmodel doesn't have a parameterless constructor, you need to override the `GetBindingContext` method in the view.

#### Simple property

Here's an example of how to implement notification support to a custom property.

```csharp
public class CounterViewModel : ViewModel
{
    private int _count;

    public int Count
    {
        get => _count;
        set => Set(ref _count, value);
    }
}
```

The provided `Set<T>(ref T, T, string)` method checks the current value of the property, and updates it if different, and then also raises the `PropertyChanged` event automatically. The property name is automatically captured through the use of the `[CallerMemberName]` attribute, so there's no need to manually specify which property is being updated.

#### Wrapping a model

To inject notification support to models, that don't implement the `INotifyPropertyChanged` interface, `ViewModel` provides a dedicated `Set<TModel, T>(T, T, TModel, Action<TModel, T>, string)` method for this.

```csharp
public class UserViewModel : ViewModel
{
    private readonly User _user;

    public UserViewModel(User user)
    {
        _user = user;
    }

    public string Name
    {
        get => _user.Name;
        set => Set(_user.Name, value, _user, (user, name) => user.Name = name);
    }
}
```

### CanvasView\<TViewModel\>

The `CanvasView<TViewModel>` is a base class for `uGUI` view's.

Key functionality:
- Provides a base implementation for `Canvas` based view
- Automatically searches for bindable UI elements on the `Canvas`
- Allows to override the base viewmodel instance creation
- Allows to define [property](#propertyvalueconvertertsourcetype-ttargettype) & [parameter](#parametervalueconverterttargettype) value converters
- Allows to provide a custom bindable elements factory

```csharp
public class CounterView : CanvasView<CounterViewModel>
{
    // Override the base viewmodel instance creation.
    // Required in case the viewmodel doesn't have a parameterless constructor.
    protected override CounterViewModel GetBindingContext()
    {
        return _appContext.Resolve<CounterViewModel>();
    }

    // Define 'property' & 'parameter' value converters.
    protected override IValueConverter[] GetValueConverters()
    {
        return _appContext.Resolve<IValueConverter[]>();
    }

    // Provide a custom bindable elements factory.
    protected override IBindableElementsFactory GetBindableElementsFactory()
    {
        return _appContext.Resolve<IBindableElementsFactory>();
    }
}
```

### DocumentView\<TViewModel\>

The `DocumentView<TViewModel>` is a base class for `UI Toolkit` view's.

Key functionality:
- Provides a base implementation for `UI Document` based view
- Automatically searches for bindable UI elements on the `UI Document`
- Allows to override the base viewmodel instance creation
- Allows to define [property](#propertyvalueconvertertsourcetype-ttargettype) & [parameter](#parametervalueconverterttargettype) value converters
- Allows to provide a custom bindable elements factory

```csharp
public class CounterView : DocumentView<CounterViewModel>
{
    // Override the base viewmodel instance creation.
    // Required in case the viewmodel doesn't have a parameterless constructor.
    protected override CounterViewModel GetBindingContext()
    {
        return _appContext.Resolve<CounterViewModel>();
    }

    // Define 'property' & 'parameter' value converters.
    protected override IValueConverter[] GetValueConverters()
    {
        return _appContext.Resolve<IValueConverter[]>();
    }

    // Provide a custom bindable elements factory.
    protected override IBindableElementsFactory GetBindableElementsFactory()
    {
        return _appContext.Resolve<IBindableElementsFactory>();
    }
}
```

### Command & Command\<T\>

The `Command` and `Command<T>` are `ICommand` implementations that can expose a method or delegate to the view. These types act as a way to bind commands between the viewmodel and UI elements.

Key functionality:
- Provide a base implementation of the `ICommand` interface
- Implement the `ICommand` & `ICommand<T>` interface, which exposes a `RaiseCanExecuteChanged` method to raise the `CanExecuteChanged` event
- Expose constructor taking delegates like `Action` and `Action<T>`, which allow the wrapping of standard methods and lambda expressions

The following shows how to set up a simple command.

```csharp
using Pepengineers.PEPEnterfaceToolkit.Core;
using Pepengineers.PEPEnterfaceToolkit.Core.Interfaces;

public class CounterViewModel : ViewModel
{
    private int _count;

    public CounterViewModel()
    {
        IncrementCommand = new Command(IncrementCount);
    }

    public int Count
    {
        get => _count;
        set => Set(ref _count, value);
    }

    public ICommand IncrementCommand { get; }

    private void IncrementCount() => Count++;
}
```

And the relative UI could then be.

```xml
<ui:UXML xmlns:uitk="Pepengineers.PEPEnterfaceToolkit.UITK.BindableUIElements" ...>
    <uitk:BindableLabel binding-text-path="Count" />
    <uitk:BindableButton command="IncrementCommand" />
</ui:UXML>
```

The `BindableButton` binds to the `ICommand` in the viewmodel, which wraps the private `IncrementCount` method. The `BindableLabel` displays the value of the `Count` property and is updated every time the property value changes.

> **Note:** You need to define `IntToStrConverter` to convert int to string. See the [PropertyValueConverter](#propertyvalueconvertertsourcetype-ttargettype) section for more information.

### AsyncCommand & AsyncCommand\<T\>

The `AsyncCommand` and `AsyncCommand<T>` are `ICommand` implementations that extend the functionalities offered by `Command`, with support for asynchronous operations.

Key functionality:
- Extend the functionalities of the synchronous commands included in the package, with support for UniTask-returning delegates
- Can wrap asynchronous functions with a `CancellationToken` parameter to support cancelation, and they expose a `DisableOnExecution` property, as well as a `Cancel` method
- Implement the `IAsyncCommand` & `IAsyncCommand<T>` interfaces, which allows to replace a command with a custom implementation, if needed

Let's say we want to download an image from the web and display it as soon as it downloads.

```csharp
public class ImageViewerViewModel : ViewModel
{
    private readonly IImageDownloader _imageDownloader;
    private Texture2D _texture;

    public ImageViewerViewModel(IImageDownloader imageDownloader)
    {
        _imageDownloader = imageDownloader;
        DownloadImageCommand = new AsyncCommand(DownloadImageAsync);
    }

    public Texture2D Image
    {
        get => _texture;
        private set => Set(ref _texture, value);
    }

    public IAsyncCommand DownloadImageCommand { get; }

    private async UniTask DownloadImageAsync(CancellationToken cancellationToken)
    {
        Image = await _imageDownloader.DownloadRandomImageAsync(cancellationToken);
    }
}
```

With the related UI code.

```xml
<ui:UXML xmlns:uitk="Pepengineers.PEPEnterfaceToolkit.UITK.BindableUIElements" ...>
    <BindableImage binding-image-path="Image" />
    <uitk:BindableButton command="DownloadImageCommand">
        <ui:Label text="Download Image" />
    </uitk:BindableButton>
</ui:UXML>
```

> **Note:** The `BindableImage` is a custom control from the [create custom control](#create-custom-control) section.

To disable the `BindableButton` while an async operation is running, simply set the `DisableOnExecution` property of the `AsyncCommand` to `true`.

```csharp
public class ImageViewerViewModel : ViewModel
{
    public ImageViewerViewModel(IImageDownloader imageDownloader)
    {
        ...
        DownloadImageCommand = new AsyncCommand(DownloadImageAsync) { DisableOnExecution = true };
    }
}
```

If you want to create an async command that supports cancellation, use the `WithCancellation` extension method.

```csharp
public class MyViewModel : ViewModel
{
    public MyViewModel()
    {
        MyAsyncCommand = new AsyncCommand(DoSomethingAsync).WithCancellation();
        CancelCommand = new Command(Cancel);
    }

    public IAsyncCommand MyAsyncCommand { get; }
    public ICommand CancelCommand { get; }
    
    private async UniTask DoSomethingAsync(CancellationToken cancellationToken)
    {
        ...
    }
    
    private void Cancel()
    {
        // If the underlying command is not running, or
        // if it does not support cancellation, this method will perform no action.
        MyAsyncCommand.Cancel();
    }
}
```

If the command supports cancellation, previous invocations will automatically be canceled if a new one is started.

> **Note:** You need to import the [UniTask](https://github.com/Cysharp/UniTask) package in order to use async commands.

### AsyncLazyCommand & AsyncLazyCommand\<T\>

The `AsyncLazyCommand` and `AsyncLazyCommand<T>` are have the same functionality as the `AsyncCommand`'s, except they prevent the same async command from being invoked concurrently multiple times.

Let's imagine a scenario similar to the one described in the `AsyncCommand` sample, but a user clicks the `Download Image` button several times while the async operation is running. In this case, `AsyncLazyCommand` will ignore all clicks until previous async operation has completed.

> **Note:** You need to import the [UniTask](https://github.com/Cysharp/UniTask) package in order to use async commands.

### PropertyValueConverter\<TSourceType, TTargetType\>

Property value converter provides a way to apply custom logic to a property binding.

Built-in property value converters:
- IntToStrConverter
- FloatToStrConverter

If you want to create your own property value converter, create a class that inherits the `PropertyValueConverter<TSourceType, TTargetType>` abstract class and then implement the `Convert` and `ConvertBack` methods.

```csharp
public enum ThemeMode
{
    Light = 0,
    Dark = 1
}

public class ThemeModeToBoolConverter : PropertyValueConverter<ThemeMode, bool>
{
    // From source to target. 
    public override bool Convert(ThemeMode value)
    {
        return (int) value == 1;
    }

    // From target to source.
    public override ThemeMode ConvertBack(bool value)
    {
        return (ThemeMode) (value ? 1 : 0);
    }
}
```
Don't forget to register the `ThemeModeToBoolConverter` in the view.

```csharp
public class MyView : DocumentView<MyViewModel>
{
    protected override IValueConverter[] GetValueConverters()
    {
        return new IValueConverter[] { new ThemeModeToBoolConverter() };
    }
}
```

Then you can use the `ThemeModeToBoolConverter` as in the following example.

```xml
<UXML>
    <!--Full expression-->
    <MyBindableElement binding-value-path="ThemeMode, Converter={ThemeModeToBoolConverter}" />
    <!--Short expression-->
    <MyBindableElement binding-value-path="ThemeMode, ThemeModeToBoolConverter" />
    <!--Minimal expression - the first appropriate converter will be used-->
    <MyBindableElement binding-value-path="ThemeMode" />
</UXML>
```

### ParameterValueConverter\<TTargetType\>

Parameter value converter allows to convert a command parameter.

Built-in parameter value converters:
- ParameterToStrConverter
- ParameterToIntConverter
- ParameterToFloatConverter

By default, the converter is not needed if your command has a `ReadOnlyMemory<char>` parameter type.

```csharp
public class MyViewModel : ViewModel
{
    public MyViewModel()
    {
        PrintParameterCommand = new Command<ReadOnlyMemory<char>>(PrintParameter);
    }

    public ICommand<ReadOnlyMemory<char>> PrintParameterCommand { get; }

    private void PrintParameter(ReadOnlyMemory<char> parameter)
    {
        Debug.Log(parameter);
    }
}
```

```xml
<UXML>
    <BindableButton command="PrintParameterCommand, Parameter={MyParameter}" />
    <!--or-->
    <BindableButton command="PrintParameterCommand, MyParameter" />
</UXML>
```

If you want to create your own parameter value converter, create a class that inherits the `ParameterValueConverter<TTargetType>` abstract class and then implement the `Convert` method.

```csharp
public class ParameterToIntConverter : ParameterValueConverter<int>
{
    public override int Convert(ReadOnlyMemory<char> parameter)
    {
        return int.Parse(parameter.Span);
    }
}
```

Don't forget to register the `ParameterToIntConverter` in the view.

```csharp
public class MyView : DocumentView<MyViewModel>
{
    protected override IValueConverter[] GetValueConverters()
    {
        return new IValueConverter[] { new ParameterToIntConverter() };
    }
}
```

Then you can use the `ParameterToIntConverter` as in the following example.

```csharp
public class MyViewModel : ViewModel
{
    public MyViewModel()
    {
        PrintParameterCommand = new Command<int>(PrintParameter);
    }

    public ICommand<int> PrintParameterCommand { get; }

    private void PrintParameter(int parameter)
    {
        Debug.Log(parameter);
    }
}
```

```xml
<UXML>
    <!--Full expression-->
    <BindableButton command="PrintIntParameterCommand, Parameter={5}, Converter={ParameterToIntConverter}" />
    <!--Short expression-->
    <BindableButton command="PrintIntParameterCommand, 5, ParameterToIntConverter" />
    <!--Minimal expression - the first appropriate converter will be used-->
    <BindableButton command="PrintIntParameterCommand, 5" />
</UXML>
```

## :watch: Quick start

Once the `PEPEnterfaceToolkit` is installed, create a class `MyFirstViewModel` that inherits the `ViewModel` class.

```csharp
using Pepengineers.PEPEnterfaceToolkit.Core;

public class MyFirstViewModel : ViewModel
{
    private string _text;

    public MyFirstViewModel()
    {
        _text = "Hello World";
    }

    public string Text
    {
        get => _text;
        set => Set(ref _text, value);
    }
}
```

#### UI Toolkit

The next step is to create a class `MyFirstDocumentView` that inherits the `DocumentView<TViewModel>` class.

```csharp
using Pepengineers.PEPEnterfaceToolkit.UITK;

public class MyFirstDocumentView : DocumentView<MyFirstViewModel>
{
}
```

Then create a file `MyFirstView.uxml`, add a `BindableLabel` control and set the `binding-text-path` to `Text`.

```xml
<ui:UXML xmlns:uitk="Pepengineers.PEPEnterfaceToolkit.UITK.BindableUIElements" ...>
    <uitk:BindableLabel binding-text-path="Text" />
</ui:UXML>
```

Finally, add `UI Document` to the scene, set the `MyFirstView.uxml` as a `Source Asset` and add the `MyFirstDocumentView` component to it.

<details><summary>UI Document Inspector</summary>
<br />

![ui-document-inspector](https://user-images.githubusercontent.com/28132516/187613060-e20a139d-72fc-4088-b8d5-f9a01f5afa5b.png)

</details>

#### Unity UI (uGUI)

For the `uGUI` do the following. Create a class `MyFirstCanvasView` that inherits the `CanvasView<TViewModel>` class.

```csharp
using Pepengineers.PEPEnterfaceToolkit.UGUI;

public class MyFirstCanvasView : CanvasView<MyFirstViewModel>
{
}
```

Then add a `Canvas` to the scene, and add the `MyFirstCanvasView` component to it.

<details><summary>Canvas Inspector</summary>
<br />

![canvas-inspector](https://user-images.githubusercontent.com/28132516/187613633-2c61c82e-ac25-4319-8e8d-1954eb4be197.png)

</details>

Finally, add a `Text - TextMeshPro` UI element to the canvas, add the `BindableLabel` component to it and set the `BindingTextPath` to `Text`.

<details><summary>Canvas Text Inspector</summary>
<br />

![canvas-text-inspector](https://user-images.githubusercontent.com/28132516/187614103-ad42d000-b3b7-4265-96a6-f6d4db6e8978.png)

</details>

## :rocket: How To Use

### Data-binding

The package contains a set of standard bindable UI elements out of the box.

The included UI elements are:
- [BindableLabel](#bindablelabel)
- [BindableTextField](#bindabletextfield)
- [BindableButton](#bindablebutton)
- [BindableListView](#bindablelistview)
- [BindableScrollView](#bindablescrollview)

> **Note:** The `BindableListView` & `BindableScrollView` are provided for `UI Toolkit` only.

#### BindableLabel

The `BindableLabel` element uses the `OneWay` binding by default.

```csharp
public class LabelViewModel : ViewModel
{
    private int _intValue;
    private string _strValue;

    public int IntValue
    {
        get => _intValue;
        set => Set(ref _intValue, value);
    }

    public string StrValue
    {
        get => _strValue;
        set => Set(ref _strValue, value);
    }
}

public class LabelView : DocumentView<LabelViewModel>
{
    protected override IValueConverter[] GetValueConverters()
    {
        return new IValueConverter[] { new IntToStrConverter() };
    }
}
```

```xml
<ui:UXML xmlns:uitk="Pepengineers.PEPEnterfaceToolkit.UITK.BindableUIElements" ...>
    <uitk:BindableLabel binding-text-path="StrValue" />
    <uitk:BindableLabel binding-text-path="IntValue" />
</ui:UXML>
```

#### BindableTextField

The `BindableTextField` element uses the `TwoWay` binding by default.

```csharp
public class TextFieldViewModel : ViewModel
{
    private string _textValue;

    public string TextValue
    {
        get => _textValue;
        set => Set(ref _textValue, value);
    }
}
```

```xml
<ui:UXML xmlns:uitk="Pepengineers.PEPEnterfaceToolkit.UITK.BindableUIElements" ...>
    <uitk:BindableTextField binding-text-path="TextValue" />
</ui:UXML>
```

#### BindableButton

The `BindableButton` can be bound to the following commands:
- [Command & Command\<T\>](#command--commandt)
- [AsyncCommand & AsyncCommand\<T\>](#asynccommand--asynccommandt)
- [AsyncLazyCommand & AsyncLazyCommand\<T\>](#asynclazycommand--asynclazycommandt)

To pass a parameter to the viewmodel, see the [ParameterValueConverter](#parametervalueconverterttargettype) section.

#### BindableListView

The `BindableListView` control is the most efficient way to create lists. Use the `binding-items-source-path` of the `BindableListView` to bind to an `ObservableCollection`.

The following example demonstrates how to bind to a collection of users with `BindableListView`.

Create a main `UI Document` named `UsersView.uxml` with the following content.

```xml
<ui:UXML xmlns:uitk="Pepengineers.PEPEnterfaceToolkit.UITK.BindableUIElements" ...>
    <uitk:BindableListView binding-items-source-path="Users" />
</ui:UXML>
```

Create a `UI Document` named `UserEntry.uxml` for the individual entries in the list.

```xml
<ui:UXML ...>
    <ui:Label name="NameLabel" />
</ui:UXML>
```

Create a `UserItemData` class to store user data.

```csharp
public class UserItemData
{
    public string Name { get; set; }
}
```

Create a `UserItemController` class to display the data of a user instance in the UI of the list entry. It needs to access the label for the user name and set it to display the name of the given user instance.

```csharp
public class UserItemController
{
    private readonly Label _nameLabel;

    public TaskItemController(VisualElement userEntryAsset)
    {
        _nameLabel = userEntryAsset.Q<Label>("NameLabel");
    }

    public void SetData(UserItemData userItemData)
    {
        _nameLabel.text = userItemData.Name;
    }
}
```

Create a `UserListViewWrapper` that inherits the `BindableListViewWrapper<TItem, TData>` abstract class and implement the `OnMakeItem` and `OnBindItem` methods.

```csharp
public class UserListViewWrapper : BindableListViewWrapper<UserItemController, UserItemData>
{
    public UserListViewWrapper(BindableListView listView, VisualTreeAsset itemAsset,
        IObjectProvider objectProvider) : base(listView, itemAsset, objectProvider)
    {
    }

    protected override UserItemController OnMakeItem(VisualElement itemAsset)
    {
        return new UserItemController(itemAsset);
    }

    protected override void OnBindItem(UserItemController item, UserItemData data)
    {
        item.SetData(data);
    }
}
```

Create a `CustomBindableElementsFactory` and override the `Create` method.

```csharp
public class CustomBindableElementsFactory : BindableElementsFactory
{
    private readonly VisualTreeAsset _userEntryAsset;

    public CustomBindableElementsFactory(VisualTreeAsset userEntryAsset)
    {
        _userEntryAsset = userEntryAsset;
    }

    public override IBindableElement Create(IBindableUIElement bindableUiElement, IObjectProvider objectProvider)
    {
        return bindableUiElement switch
        {
            BindableListView listView => new UserListViewWrapper(listView, _userEntryAsset, objectProvider),

            _ => base.Create(bindableUiElement, objectProvider)
        };
    }
}
```

Create a `UsersViewModel`.

```csharp
public class UsersViewModel : ViewModel
{
    public UsersViewModel()
    {
        Users = new ObservableCollection<UserItemData>
        {
            new() { Name = "User 1" },
            new() { Name = "User 2" },
            new() { Name = "User 3" },
        };
    }
    
    public ObservableCollection<UserItemData> Users { get; }
}
```

Create a `UsersView` with the following content.

```csharp
public class UsersView : DocumentView<UsersViewModel>
{
    [SerializeField] private VisualTreeAsset _userEntryAsset;

    protected override IBindableElementsFactory GetBindableElementsFactory()
    {
        return new CustomBindableElementsFactory(_userEntryAsset);
    }
}
```

#### BindableScrollView

The `BindableScrollView` has the same binding logic as the `BindableListView`, except that the `UserItemData` class must implement the `ICollectionItemData` interface.

```csharp
public class UserItemData : ICollectionItemData
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Name { get; set; }
}
```

### Create custom control

Let's create a `BindableImage` UI element.

First of all, create a base `Image` class.

```csharp
public class Image : VisualElement
{
    public void SetImage(Texture2D image)
    {
        // To prevent memory leaks.
        style.backgroundImage.Release(); // Object.Destroy(background.value.texture);
        style.backgroundImage = new StyleBackground(image);
    }

    public new class UxmlFactory : UxmlFactory<Image, UxmlTraits> {}
    
    public new class UxmlTraits : VisualElement.UxmlTraits {}
}
```

Then create a `BindableImage` and define `BindingImagePath` property.

```csharp
public class BindableImage : Image, IBindableUIElement
{
    public string BindingImagePath { get; set; }

    public new class UxmlFactory : UxmlFactory<BindableImage, UxmlTraits> {}

    public new class UxmlTraits : Image.UxmlTraits
    {
        private readonly UxmlStringAttributeDescription _bindingImageAttribute = new()
            { name = "binding-image-path", defaultValue = "" };

        public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
        {
            base.Init(visualElement, bag, context);
            ((BindableImage) visualElement).BindingImagePath = _bindingImageAttribute.GetValueFromBag(bag, context);
        }
    }
}
```

The next step is to describe the data binding logic. To do that, create a `BindableImageWrapper` that inherits the `BindablePropertyElement` abstract class.

```csharp
public class BindableImageWrapper : BindablePropertyElement
{
    private readonly BindableImage _bindableImage;
    private readonly IReadOnlyProperty<Texture2D> _imageProperty;

    public BindableImageWrapper(BindableImage bindableImage, IObjectProvider objectProvider) : base(objectProvider)
    {
        _bindableImage = bindableImage;
        _imageProperty = GetReadOnlyProperty<Texture2D>(bindableImage.BindingImagePath);
    }

    public override void UpdateValues()
    {
        _bindableImage.SetImage(_imageProperty.Value);
    }
}
```

The **PEPEnterfaceToolkit** contains two abstract classes `BindableCommandElement` and `BindablePropertyElement` that provide a methods for getting properties from the `BindingContext`.

Finally, tell the elements factory what to do with the new UI element.

```csharp
public class CustomBindableElementsFactory : BindableElementsFactory
{
    public override IBindableElement Create(IBindableUIElement bindableUiElement, IObjectProvider objectProvider)
    {
        return bindableUiElement switch
        {
            BindableImage bindableImage => new BindableImageWrapper(bindableImage, objectProvider),

            _ => base.Create(bindableUiElement, objectProvider)
        };
    }
}
```

Don't forget to override the `GetBindableElementsFactory` method in the view.

```csharp
public class ImageViewerView : DocumentView<ImageViewerViewModel>
{
    protected override IBindableElementsFactory GetBindableElementsFactory()
    {
        return new CustomBindableElementsFactory();
    }
}
```

Now you can use the new UI element as following.

```csharp
public class ImageViewerViewModel : ViewModel
{
    private Texture2D _texture;

    public Texture2D Image
    {
        get => _texture;
        private set => Set(ref _texture, value);
    }
}
```

```xml
<UXML>
    <BindableImage binding-image-path="Image" />
</UXML>
```

## :link: External Assets

### UniTask

To enable [async commands](#asynccommand--asynccommandt) support, you need to add the [UniTask](https://github.com/Cysharp/UniTask) package to your project.

In addition to async commands **PEPEnterfaceToolkit** provides extensions to make [USS transition](https://docs.unity3d.com/Manual/UIE-Transitions.html)'s awaitable.

For example, your `VisualElement` has the following transitions.
```css
.panel--animation {
    transition-property: opacity, padding-bottom;
    transition-duration: 65ms, 150ms;
}
```

You can `await` these transitions using several methods.
```csharp
public async UniTask DeactivatePanel()
{
    try
    {
        panel.style.opacity = 0;
        panel.style.paddingBottom = 0;
        
        // Await for the 'opacity' || 'paddingBottom' to end or cancel.
        await panel.WaitForAnyTransitionEnd();
        
        // Await for the 'opacity' & 'paddingBottom' to end or cancel.
        await panel.WaitForAllTransitionsEnd();
        
        // Await 150ms.
        await panel.WaitForLongestTransitionEnd();

        // Await 65ms.
        await panel.WaitForTransitionEnd(0);
        
        // Await for the 'paddingBottom' to end or cancel.
        await panel.WaitForTransitionEnd(new StylePropertyName("padding-bottom"));
        
        // Await for the 'paddingBottom' to end or cancel.
        // Uses ReadOnlySpan to match property names to avoid memory allocation.
        await panel.WaitForTransitionEnd(nameof(panel.style.paddingBottom));
        
        // Await for the 'opacity' || 'paddingBottom' to end or cancel.
        // You can write your own transition predicates, just implement a 'ITransitionPredicate' interface.
        await panel.WaitForTransitionEnd(new TransitionAnyPredicate());
    }
    finally
    {
        panel.visible = false;
    }
}
```

> **Note:** All transition extensions have a `timeoutMs` parameter (default value is `2500ms`).

## :balance_scale: License

Usage is provided under the [MIT License](LICENSE).
