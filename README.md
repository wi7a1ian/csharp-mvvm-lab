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
  - *ViewModel* and *ViewModel* event aggregator, mediator (including dialog service), data binding + dependency property (less preferable), data binding + data context (less preferable)
- binding: use *ViewModels* that inherits `BindableBase` that implements `INotifyPropertyChanged`
- commands: define `DelegateCommand` (aka `RelayCommand`)
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
- *Views* can be connected to *ViewModels* in code-behind file, or in the view itself (`DataContext`) via static resource or service locator

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
- add design time data (default `dtor` or injected by DI container)
- add navigation and enable visibility-awareness on *ViewModels*
- add dialog/modal service
- add event aggregator
- add support for localization
- add form validation
- log unhandled exceptions (from both the dispatcher and current ap domain) and unobserved tasks
- add style [MaterialDesign](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
- compare with [MVVM Light Toolkit](http://www.mvvmlight.net/)
