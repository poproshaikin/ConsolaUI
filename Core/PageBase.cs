namespace ConsolaUI;

public abstract partial class PageBase
{
    private AppBase _app;

    protected AppBase App => _app;
    
    private List<IElement> _elements;

    private int _currentY;
    private int _currentX;

    private const ConsoleKey enter_key = ConsoleKey.Spacebar;
    
    protected PageBase(AppBase app)
    {
        _app = app;
        _elements = [];
        _currentX = -1;
        _currentY = -1;
        PlaceCursorAtFirstSelectable();
    }
    
    protected TApp GetApp<TApp>() where TApp : AppBase => (TApp)_app;

    public void Render()
    {
        if (_elements.Count == 0)
            Markup();
        
        foreach (var element in _elements)
        {
            element.Print();
        }
    }

    protected void Redraw()
    {
        int prevX = _currentX;
        int prevY = _currentY;

        Console.Clear();
        _elements.Clear();
        Markup();

        foreach (var element in _elements)
        {
            element.Print();
        }

        PlaceCursorAt(prevY, prevX);
    }

    protected abstract void Markup();

    internal void HandleInput()
    {
        ConsoleKey key = Console.ReadKey().Key;
        UpdateState(key);
    }

    protected void ChangePage(PageBase page)
    {
        _app.ChangePage(page);
    }

    protected void Exit()
    {
        _app.Exit();
    }

    protected void AddElement(Element element)
    {
        element.PosY = CountNewLines();
        element.PosX = CountElementsAt(element.PosY);
        
        _elements.Add(element);
    }

    private int CountNewLines()
    {
        return _elements.OfType<NewLine>().Count();
    }

    private int CountElementsAt(int y)
    {
        return _elements.Count(el => el.PosY == y);
    }

    protected virtual void UpdateState(ConsoleKey key)
    {
        if (_currentX == -1 && _currentY == -1)
        {
            PlaceCursorAtFirstSelectable();
            return;
        }
        
        if (key == enter_key)
        {
            IClickableElement? clickable = _elements.OfType<IClickableElement>().FirstOrDefault(el => el.PosY == _currentY && el.PosX == _currentX);
            clickable?.Click();
        }
        else
            MoveCursor(key);
    }
    
    private void MoveCursor(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                MoveCursorUp();
                break;
            case ConsoleKey.DownArrow:
                MoveCursorDown();
                break;
            case ConsoleKey.LeftArrow:
                MoveCursorLeft();
                break;
            case ConsoleKey.RightArrow:
                MoveCursorRight();
                break;
        }
        
        ISelectableElement? selectable = _elements.OfType<ISelectableElement>().FirstOrDefault(el => el.PosY == _currentY && el.PosX == _currentX);
        selectable?.Select();
    }

    private void PlaceCursorAt(int y, int x)
    {
        ISelectableElement? selectable = _elements.OfType<ISelectableElement>().FirstOrDefault(el => el.PosY == y && el.PosX == x);

        if (selectable is null)
            return;
        
        _currentY = y;
        _currentX = x;

        selectable.Toggle();
    }

    private void PlaceCursorAtFirstSelectable()
    {
        ISelectableElement? selectable = _elements.OfType<ISelectableElement>().FirstOrDefault();

        if (selectable is null)
            return;
        
        _currentY = selectable.PosY;
        _currentX = selectable.PosX;

        selectable.Toggle();
    }
    
    private void MoveCursorUp()
    {
        ISelectableElement[] selectableElements = _elements.OfType<ISelectableElement>().ToArray();
        
        if (selectableElements.Length == 0)
            return;

        for (int y = _currentY - 1; y >= 0; y--)
        {
            if (selectableElements.Any(el => el.PosY == y))
            {
                ISelectableElement oldButton = selectableElements.First(el => el.PosY == _currentY && el.PosX == _currentX);
                oldButton.Toggle();

                ISelectableElement newButton = selectableElements.First(el => el.PosY == y);
                newButton.Toggle();

                _currentY = y;
                _currentX = newButton.PosX;

                break;
            }
        }
    }
    
    private void MoveCursorDown()
    {
        ISelectableElement[] selectableElements = _elements.OfType<ISelectableElement>().ToArray();
        int maxY = selectableElements.Max(el => el.PosY);

        if (_currentY == maxY)
            return;
        
        if (selectableElements.Length == 0)
            return;

        for (int y = _currentY + 1; y <= maxY; y++)
        {
            if (selectableElements.Any(el => el.PosY == y))
            {
                var oldButton = selectableElements.First(el => el.PosY == _currentY && el.PosX == _currentX);
                oldButton.Toggle();

                var newButton = selectableElements.First(el => el.PosY == y);
                newButton.Toggle();

                _currentY = y;
                _currentX = newButton.PosX;
                break;
            }
        }
    }
    
    private void MoveCursorLeft()
    {
        if (_currentX == 0)
            return;

        ISelectableElement[] selectableElements = _elements.OfType<ISelectableElement>().ToArray();
        
        if (selectableElements.Length == 0)
            return;

        for (int x = _currentX - 1; x >= 0; x--)
        {
            if (selectableElements.Any(el => el.PosY == _currentY && el.PosX == x))
            {
                var oldButton = selectableElements.First(el => el.PosY == _currentY && el.PosX == _currentX);
                oldButton.Toggle();

                var newButton = selectableElements.First(el => el.PosY == _currentY && el.PosX == x);
                newButton.Toggle();

                _currentX = x;
                break;
            }
        }
    }
    
    private void MoveCursorRight()
    {
        ISelectableElement[] selectableElements = _elements.OfType<ISelectableElement>().ToArray();
        int maxX = selectableElements.Where(el => el.PosY == _currentY).Max(el => el.PosX);

        if (_currentX == maxX)
            return;
        
        if (selectableElements.Length == 0)
            return;

        for (int x = _currentX + 1; x <= maxX; x++)
        {
            if (selectableElements.Any(el => el.PosY == _currentY && el.PosX == x))
            {
                var oldButton = selectableElements.First(el => el.PosY == _currentY && el.PosX == _currentX);
                oldButton.Toggle();

                var newButton = selectableElements.First(el => el.PosY == _currentY && el.PosX == x);
                newButton.Toggle();

                _currentX = x;
                break;
            }
        }
    }
}