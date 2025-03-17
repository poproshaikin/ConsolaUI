namespace ConsolaUI;

public abstract partial class PageBase
{
    private const ConsoleKey enter_key = ConsoleKey.Spacebar;
    
    private int _currentY;
    private int _currentX;
    
    private AppBase _app;

    // TODO: в майбутньому можливо реалiзувати власний клас, який буде зберiгати елементи i
    // пам'ятати представлятиме методи для запам'ятовування старих i re-render нових елементiв
    private List<IElement> _elements;
    
    protected PageBase(AppBase app)
    {
        _app = app;
        _elements = [];
        _currentX = -1;
        _currentY = -1;
        PlaceCursorAtFirstSelectable();
    }
    
    protected AppBase GetApp() => _app;
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

    protected void Rerender()
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
        //PlaceCursorAtFirstSelectable();
        _app.ChangePage(page);
    }

    protected void Exit()
    {
        _app.Exit();
    }

    protected void AddElement(ElementBase element)
    {
        element.PosY = CountNewLines();
        element.PosX = CountElementsAt(element.PosY);
        
        _elements.Add(element);

        // Rerender();
    }

    protected void RemoveElement(string id)
    {
        IElement? element = _elements.FirstOrDefault(el => el.Id == id);
        _elements.Remove(element!);
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
        
        ISelectableElement? selectable = _elements.OfType<ISelectableElement>()
            .FirstOrDefault(el => el.PosY == _currentY && el.PosX == _currentX);
        
        selectable?.Select();
    }

    private void PlaceCursorAt(int y, int x)
    {
        ISelectableElement? selectable = _elements.OfType<ISelectableElement>()
            .FirstOrDefault(el => el.PosY == y && el.PosX == x);

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
                var oldButton = selectableElements.FirstOrDefault(el => el.PosY == _currentY && el.PosX == _currentX); 
                var newButton = selectableElements.FirstOrDefault(el => el.PosY == y);

                if (oldButton is null || newButton is null)
                {
                    PlaceCursorAtFirstSelectable();
                    return;
                }
                
                oldButton.Toggle();
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

        if (selectableElements.Length == 0)
            return;
        
        int maxY = selectableElements.Max(el => el.PosY);

        if (_currentY == maxY)
            return;
        
        if (selectableElements.Length == 0)
            return;

        for (int y = _currentY + 1; y <= maxY; y++)
        {
            if (selectableElements.Any(el => el.PosY == y))
            {
                var oldButton = selectableElements.FirstOrDefault(el => el.PosY == _currentY && el.PosX == _currentX);
                var newButton = selectableElements.FirstOrDefault(el => el.PosY == y);
                
                if (oldButton is null || newButton is null)
                {
                    PlaceCursorAtFirstSelectable();
                    return;
                }

                oldButton.Toggle();
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
                var oldButton = selectableElements.FirstOrDefault(el => el.PosY == _currentY && el.PosX == _currentX);
                var newButton = selectableElements.FirstOrDefault(el => el.PosY == _currentY && el.PosX == x);

                if (oldButton is null || newButton is null)
                {
                    PlaceCursorAtFirstSelectable();
                    return;
                }
                
                oldButton.Toggle();
                newButton.Toggle();

                _currentX = x;
                break;
            }
        }
    }
    
    private void MoveCursorRight()
    {
        var selectableElements = _elements.OfType<ISelectableElement>().ToArray();

        if (selectableElements.Length == 0)
            return;

        var atCurrentY = selectableElements.Where(el => el.PosY == _currentY).ToArray();

        if (atCurrentY.Length == 0)
            return;
        
        int maxX = atCurrentY.Max(el => el.PosX);

        if (_currentX == maxX)
            return;
        
        if (selectableElements.Length == 0)
            return;

        for (int x = _currentX + 1; x <= maxX; x++)
        {
            if (selectableElements.Any(el => el.PosY == _currentY && el.PosX == x))
            {
                var oldButton = selectableElements.FirstOrDefault(el => el.PosY == _currentY && el.PosX == _currentX);
                var newButton = selectableElements.FirstOrDefault(el => el.PosY == _currentY && el.PosX == x);

                if (oldButton is null || newButton is null)
                {
                    PlaceCursorAtFirstSelectable();
                    return;
                }

                oldButton.Toggle();
                newButton.Toggle();

                _currentX = x;
                break;
            }
        }
    }
}