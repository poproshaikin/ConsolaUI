namespace ConsolaUI;

public abstract class AppBase
{
    private PageBase? _currentPage;

    private bool _initialized;
    private bool _requestedExit;

    protected AppBase()
    {
        _requestedExit = false;
    }
    
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

        while (!_requestedExit)
        {
            Console.Clear();
            _currentPage.Render();
            _currentPage.HandleInput();
        }
        
        Console.Clear();
    }

    public void ChangePage(PageBase page)
    {
        _currentPage = page;
    }

    protected virtual void Init()
    {
    }

    public void Exit()
    {
        _requestedExit = true;
    }
}