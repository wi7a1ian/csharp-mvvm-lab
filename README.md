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
  - *View* and **ViewModel** interact via: data binding, commands, data validation and error reporting
  - *View* and **Control** interact via: dependency property
  - *ViewModel* and **Control** interact via: (data binding / commands) + dependency property
  - *ViewModel* and **ViewModel** event aggregator, mediator (including dialog service), data binding + dependency property (less preferable), data binding + data context (less preferable)
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
- the **ViewModel** retrieves data from the (domain) *Model* and then makes the data available to the *View*, and may reformat the data in some way that makes it simpler for the *View* to handle
