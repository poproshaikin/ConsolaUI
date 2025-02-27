# ConsolaUI
For a programming project, I needed to create a console UI. Instead of hardcoding a basic UI with the standard library, I quickly built my own framework for console interfaces. It's simple, but probably better than just using Console.WriteLine. (maybe)

## Usage

### App
First of all, you need to create a class that inherits from `AppBase`.  
For a most basic application, you only need to override a `GetInitialPage` method.
This method will provide a basic page that will be displayed when the application starts.

### Page
A page is a screen that will be displayed to the user.  
You can create a page by inheriting from `PageBase`.  

In the constructor you need to pass your `AppBase` object (just using `this`).  
You need to override the `Markup` method to define the content of the page.  

### Elements
You can add elements to the page by using the `AddElement` method.  
Or use pre-defined methods in PageBase class such as `Text`, `InputField`, `Button`, etc.

Also, you can create your own elements by inheriting from `ElementBase` class.

### Example

```csharp
class Program
{
    static void Main(string[] args)
    {
        MyApp app = new MyApp();
        app.Run();
    }
}

class MyApp : AppBase
{
    protected override PageBase GetInitialPage()
    {
        return new MainPage(this);
    }
}
class MainPage : PageBase 
{
    private string? _name = null;
    private bool _clicked = false;
    
    public MainPage(AppBase app) : base(app) 
    {
    }
    
    protected override void Markup() 
    {
        Text($"Hello, {_name}");
        NewLine();
        InputField("Enter your name: ", input =>
        {
            _name = input;
            Redraw();
        });
        NewLine();
        Button("Click me", () =>
        {
            _clicked = true;
            Redraw();
        });
        if (_clicked)
        {
            NewLine();
            Text("You clicked me!");
        }
    }
}
```

### Tips

You can use ChangePage() method in your PageBase class to change the current page of an app.