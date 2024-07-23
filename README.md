# AbpWpfWithCM

Abp Wpf项目模板，使用Caliburn.Micro作为MVVM框架，使用MaterialDesignInXamlToolkit作为UI框架。

此模板主要集成了以下框架:
1. [abp vNext](https://docs.abp.io/zh-Hans/abp/latest) ABP是一个开源应用程序框架,专注于基于ASP.NET Core的Web应用程序开发,但也支持开发其他类型的应用程序.
2. [caliburn.micro](https://caliburnmicro.com/documentation/introduction) `caliburnmicro`是一个轻量级的MVVM框架，它的目标是简化WPF应用程序的开发。
3. [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged) `PropertyChanged.Fody`是一个实现属性更改通知的库，它可以在编译时自动实现属性更改通知。
4.  [MaterialDesignInXamlToolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/releases) Material Design Wpf 控件库

## 1. abp vNext 使用

[abp 文档地址](https://docs.abp.io/zh-Hans/abp/latest)

abp框架的功能较多，这里只介绍一些常用的功能。

### 1. 依赖注入

abp框架会自动扫描程序集中的类，将标记了`ITransientDependency`，`ISingletonDependency`，`IScopedDependency`接口的类注入到依赖注入容器中。

1. 因此待注入的类需要实现`ITransientDependency`，`ISingletonDependency`，`IScopedDependency`接口。

三个接口的功能分别是：

- ITransientDependency： 每次注入都会创建一个新的实例。
- ISingletonDependency： 单例模式，每次注入都会返回同一个实例。
- IScopedDependency： 每次请求都会在域范围内创建一个新的实例。

这三个都是空接口，只是为了标识类的生命周期。

```csharp
public interface IDataService : ITransientDependency
{
    List<string> GetData();
}

public class DataService : ITransientDependency
{
    public List<string> GetData()
    {
        return new List<string>
        {
            "Item1",
            "Item2",
            "Item3",
            "Item4",
            "Item5"
        };
    }
}

```

2. 需要注入的类在构造函数中声明。

```csharp
public class MainWindowViewModel : Screen
{
    private readonly IDataService _dataService;

    public MainWindowViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }
}
```



## 2. caliburn.micro 使用

### 1. View与ViewModel使用约定

1. View的命名规则为`{Xxx}View`，ViewModel的命名规则为`{Xxx}ViewModel`, 对应名称的View和ViewModel会自动关联。
2. View的文件类型是`UserControl`，在vs中新建后，可以只保留.xaml文件，删除对应的.cs文件。
3. ViewModel中的属性和方法，可以直接在View中用`x:Name`绑定。例如：

    在ViewModel中：

    ```csharp
    public class UserViewModel() 
    {
        public string NewName  {get;set;}
    }
    ```

    在View中：

    ```xml
    <TextBlock x:Name="NewName">
    ```

    此时，`TextBlock`的`Text`属性会自动绑定到`NewName`属性。方法也可以同样绑定。

### 2. 事件绑定

1. 顶部引用命名空间
```
xmlns:cal="http://www.caliburnproject.org"
```
2. 事件控件上
``` 
cal:Message.Attach="[Event PreviewKeyDown] = [Action Del($source, $eventArgs)]"
```
多个事件用分号分隔。

可选的事件参数：

$eventArgs
将 EventArgs 或输入参数传递给您的 Action（动作）。注意：对于保护方法（guard methods），此参数将是 null，因为触发事件实际上并未发生。

$dataContext
将 ActionMessage 所附加元素的数据上下文（DataContext）传递过去。在主从（Master/Detail）场景中这非常有用，其中 ActionMessage 可能会上冒到父级视图模型（VM），但需要携带子实例以供处理。

$source
触发 ActionMessage 发送的实际 FrameworkElement（框架元素）。

$view
与 ViewModel 绑定的视图（通常是 UserControl 或 Window）。

$executionContext
动作的执行上下文，其中包含上述所有信息以及更多。在高级场景中很有用。

$this

实际的 UI 元素，Action 动作即附着于此。在此情况下，元素本身不会作为参数传递，而是传递其默认属性。

请注意，"$this" 在不同的编程语言或框架中可能有不同的含义和实现方式，在这里它特指所附着的 UI 元素的默认属性。

### 3. 内部消息传递

参考文档: [The Event Aggregator](https://caliburnmicro.com/documentation/event-aggregator)

abp也有一个内部消息传递的机制，可以自行选择使用。

### 4. 窗口管理器

```csharp
FlashInfo = new FlashInfoViewModel(_softwareAppService, item);
_windowManager.ShowDialogAsync(FlashInfo);
```