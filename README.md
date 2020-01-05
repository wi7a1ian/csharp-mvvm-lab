# Laboratory: Basic concepts of MVVM pattern in WPF (without any framework)

# Guidelines
- each *View* should have a single *ViewModel* and a *ViewModel* should only service a single *View*
  - *ViewModel* must have no knowledge of the view containing it
  - *ViewModel* is an abstraction of the *View* that exposes public properties and commands
  - if you want to reuse *ViewModel* in multiple *Views*, you can inherit multiple *ViewModels* from common *ViewModel* class
- the *ViewModel* and *Models* should be testable without the *View* - this leads to low coupling
- design-time:
  - *ViewModel* class must have a public parameterless constructor and the `DataContext` of the *View* should be defined in XAML
  - *ViewModel* should supply *Design-Time Data*
  - DI container should be configured to support design-time
- communication:
  - *View* and *ViewModel* interact via: data binding, commands, data validation and error reporting
  - *View* and *Control* interact via: dependency property
  - *ViewModel* and *Control* interact via: (data binding / commands) + dependency property
  - *ViewModel* and *ViewModel* interact via: event aggregator, mediator (including dialog service), data binding + dependency property (less preferable), data binding + data context (less preferable, when one *View* embeds another)
- binding: 
  - use *ViewModels* that inherits [`BindableBase`](https://gist.github.com/wi7a1ian/1eb34a2d1135cacc0af64106301f853b) that implements `INotifyPropertyChanged`
  - connect *Views* to *ViewModels* via `ViewModelLocator`, binding to the `DataContext` of the first child element within `UserControl`/`Page`
  - use specialized converters when data type of the bound property is not consumable by XAML controls
- commands: define [`DelegateCommand` (aka `RelayCommand`)](https://gist.github.com/wi7a1ian/28c042b64cfd26e8e3bb5de64c0d50f6)
- validation: implement data validation via the `IDataErrorInfo` or `INotifyDataErrorInfo` interfaces
- collections: 
  - derive classes that represent collections of objects from the `ObservableCollection<T>` class, which provides an implementation of the `INotifyCollectionChanged` interface
  - implement `ICollectionView` (`ListCollectionView`) for more sophisticated collections by wrapping an underlying collection of items so that they can provide automatic selection tracking and sorting, filtering, and paging for them (`CollectionViewSource`)
- navigation:
  - write your custom `ViewModelToViewConverter`, this creates *View* from viewmodel based on namig convention. It is usefull in situations like navigation or when you have to select view based on user input.\
`<ContentControl Content="{Binding SelectedViewModel, Converter={StaticResource ViewModelToViewConverter}" />`
  - use behaviors implemented as attached propetry - the `ViewModelLifeCycleBehaviour` calls ViewModel's `Activate` and `Deactivate` methods automatically - the big advantage is that you can put the behaviour into XAML style that is used by all *Views*, so you don't have to repeat in in each view.\
`<UserControl x:Class="..." xmlns:local="..." local:ViewModelLifeCycleBehaviour.AutoWireEvents="True">`
- avoid MVVM frameworks - they are too generic, consider toolkits like *MVVL Light instead*

![](https://docs.microsoft.com/en-us/previous-versions/msp-n-p/images/gg405484.333d7f906287fb8887d43c85a4a8fc08%28en-us%2cpandp.40%29.png)

### Details
- read [Practial MVVM Manifesto](https://web.archive.org/web/20160127012811/practicalmvvm.com/Manifesto/)
- MVVM vs MVC vs MVP: instead of the *controller of the MVC pattern*, or the *presenter of the MVP pattern*, **MVVM has a binder**, which automates communication between the view and its bound properties in the view model. The main difference between the view model and the Presenter in the MVP pattern, is that the presenter has a reference to a view whereas the view model does not. Instead, a view directly binds to properties on the view model to send and receive updates. To function efficiently, this requires a binding technology or generating boilerplate code to do the binding
- the *ViewModel* retrieves data from the (domain) *Model* and then makes the data available to the *View*, and may reformat the data in some way that makes it simpler for the *View* to handle
- the *ViewModel* also provides implementations of commands that a user of the application initiates in the *View*, i.e. when a user clicks a button in the UI, that action can trigger a command in the *ViewModel*
- the *ViewModel* may also be responsible for defining logical state changes that affect some aspect of the display in the *View*, such as an indication that some operation is pending
- in order for the *ViewModel* to participate in two-way data binding with the *View*, its properties must raise the `PropertyChanged` event
- *Views* can be connected to *ViewModels* in code-behind file, or in the view itself (`DataContext`) via static resource. A common approach to doing this is to use a `ViewModelLocator`
- if a *ViewModel* does not have any constructor arguments, the *ViewModel* can be instantiated in the *View* as its `DataContext`
- the *ViewModel* can implement commands as either a Command Method or as a Command Object (an object that implements the `ICommand` interface)
- the *View* should not have any logic code that you need to unit test
- the *View* defines and handles UI visual behavior, such as animations or transitions that may be triggered from a state change in the *ViewModel* or via the user's interaction with the UI
- the *View* may customize the data binding behavior between the *View* and the *ViewModel*. For example, the *View* may use value converters to format the data to be displayed in the UI, or it may use validation rules to provide additional input data validation to the user
- the *View's* code-behind may define UI logic to implement visual behavior that is difficult to express in XAML or that requires direct references to the specific UI controls defined in the *View*
- the *Model* classes typically provide property and collection change notification events through the `INotifyPropertyChanged` and `INotifyCollectionChanged` interfaces. This allows them to be easily data bound in the *View*. *Model* classes that represent collections of objects typically derive from the `ObservableCollection<T>` class
- sometimes you will need to work with *Model* objects that do not implement the INotifyPropertyChanged, INotifyCollectionChanged, IDataErrorInfo, or INotifyDataErrorInfo interfaces. In those cases, the *ViewModel* may need to wrap the model objects and expose the required properties to the *View*
- the *Model* classes typically provide data validation and error reporting through either the `IDataErrorInfo` or `INotifyDataErrorInfo` interfaces
- the *Model* classes are typically used in conjunction with a service or repository that encapsulates data access and caching
- if you need to implement filtering, sorting, grouping, or selection tracking of items in the *collection* from within your *ViewModel*, your *ViewModel* should create an instance of a collection view class for each collection to be exposed to the view. You can then subscribe to selection changed events, such as the `CurrentChanged` event, or control filtering, sorting, or grouping using the methods provided by the collection view class from within your *ViewModel*
- the *ViewModel* should implement a read-only property that returns an `ICollectionView` reference so that controls in the *View* can data bind to the collection view object and interact with it. All WPF controls that derive from the `ItemsControl` base class can automatically interact with `ICollectionView` classes
- create a `UserControl` when implementing an application specific *View* with a *ViewModel* and few dependency properties which have to be synchronized with properties of the *ViewModel*. These views should be put into the *Views/* directory
- create a *templated control* when implementing a reusable UI control without *ViewModel* and/or lots of dependency properties which are directly bound to attributes in XAML. These controls should be put into the Controls directory.

### Suggested project file structure
- *App.xaml* - loads configuration, logs unhandled exceptions, registers global resources like `ServiceLocator`
- *Configuration.cs* - i.e. DI container configuration
- *Shell.xaml* - shell for *ViewModels*, can handle navigation and dialogs/modals
- *Controls* - reusable UI controls (application independent views) without *ViewModels*
  - *DateTimePicker.xaml*
- *Converters* - converters for all the *Views*
  - *Common.cs* - i.: `NullToVisibilityConverter`
  - *MyFlagToStringConverter.cs*
- *Core*: domain model (aka domain classes) i.e. domain entities, domain services, exceptions, factories, validators, repository and service interfaces...
- *Events* - events/messages used with event aggregator, used via *ViewModels*
- *Infrastructure* - technology specific services, i.e. DAL implementing repository interfaces, reusable WPF helper classes
- *Localization* - classes and resources for application localization
    - Strings.resx/.resw
- Resources - styles, icons and templates
- *Services*: app specific services, so anything with app logic that does not belong to the domain model but is used via *ViewModels*, i.e. mediators, dialog service
- ViewModels:
    - MainWindowModel.cs
    - MyViewModel.cs
    - Dialogs
        - SelectItemDialogModel.cs
- Views:
    - MainWindow.xaml
    - MyView.xaml
    - Dialogs
        - SelectItemDialog.xaml

# More Exercises
- add design time data ([default `dtor`](https://gist.github.com/wi7a1ian/d2257384ec77f963463748d7df315be5) or injected by DI container)
- add navigation and enable visibility-awareness on *ViewModels* (via nav service, attach prop defined in *Shell.xaml* or [code-behind](https://gist.github.com/wi7a1ian/6adf1dce77e6fe8d5df3752b37e66235))
- add [dialog/modal service](https://gist.github.com/wi7a1ian/927d4131e099bf31e957c0da5e8f84e9) with [modal presenter control](https://gist.github.com/wi7a1ian/bef05df08402b62102fb64246e1e1677)
- add [event aggregator](https://gist.github.com/wi7a1ian/7f067c3ddf9d69ec3ba73080fac6f32e) for VM-to-VM communication
- add [support for localization](https://gist.github.com/wi7a1ian/ea12d9b69bcb2cfcd527cd20e4a36bed) ([reactive](https://gist.github.com/wi7a1ian/b813653db41cb1669d19682753a6f41d))
- add [form validation](https://gist.github.com/wi7a1ian/8f7575295c8575b3e2ed5818204df67b)
- log unhandled exceptions (from both the dispatcher and current ap domain) and unobserved tasks via [global exception handlers](https://gist.github.com/wi7a1ian/7a35a0c4479fb78879a052e882c83ed0)
- add [event-to-command](https://gist.github.com/wi7a1ian/25645daf4c8ad11efcc2ca4483350839)
- use style [MaterialDesign](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
- use `async` events and [avoid deadlocks](https://gist.github.com/wi7a1ian/b1c2fe0510d6a4e109975cc2ac30038f)
- compare with [MVVM Light Toolkit](http://www.mvvmlight.net/)
