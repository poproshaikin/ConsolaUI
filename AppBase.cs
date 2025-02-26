namespace ConsolaUI;

public abstract class AppBase
{
    private PageBase? _currentPage;

    private bool _initialized;
    
    protected abstract PageBase GetInitialPage();
    
    public void Run()
    {
        if (!_initialized)
        {
            Init();
            _initialized = true;
        }
        
        Console.Clear();

        _currentPage ??= GetInitialPage();

        while (true)
        {
            _currentPage.Render();
            _currentPage.HandleInput();
        }
    }

    public void ChangePage(PageBase page)
    {
        _currentPage = page;
    }


    protected virtual void Init()
    {
    }
}