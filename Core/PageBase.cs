namespace ConsolaUI;

public abstract partial class PageBase
{
    protected AppBase App { get; set; }
    
    private List<Element> _elements;

    private int _currentY;
    private int _currentX;

    private const ConsoleKey enter_key = ConsoleKey.Spacebar;
    
    protected PageBase(AppBase app)
    {
        App = app;
        _elements = [];
        _currentX = -1;
        _currentY = -1;
    }

    public void Render()
    {
        foreach (var element in _elements)
        {
            element.Print();
        }
    }

    public void HandleInput()
    {
        ConsoleKey key = Console.ReadKey().Key;
        UpdateState(key);
    }

    public void ChangePage(PageBase page)
    {
        App.ChangePage(page);
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
        Console.Clear();
        
        if (_currentX == -1 && _currentY == -1)
        {
            PlaceCursorAtFirstButton();
            return;
        }
        
        if (key == enter_key)
        {
            Button btn = _elements.OfType<Button>().First(el => el.PosY == _currentY && el.PosX == _currentX);
            btn.Click();
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
        
        Button? btn = _elements.OfType<Button>().FirstOrDefault(el => el.PosY == _currentY && el.PosX == _currentX);
        btn?.Select();
    }

    private void PlaceCursorAtFirstButton()
    {
        Button btn = _elements.OfType<Button>().First();
        
        _currentY = btn.PosY;
        _currentX = btn.PosX;

        btn.Toggle();
    }
    
    private void MoveCursorUp()
    {
        Button[] buttons = _elements.OfType<Button>().ToArray();

        for (int y = _currentY - 1; y >= 0; y--)
        {
            if (buttons.Any(el => el.PosY == y))
            {
                var oldButton = buttons.First(el => el.PosY == _currentY && el.PosX == _currentX);
                oldButton.Toggle();

                var newButton = buttons.First(el => el.PosY == y);
                newButton.Toggle();

                _currentY = y;
                _currentX = newButton.PosX;

                break;
            }
        }
    }
    
    private void MoveCursorDown()
    {
        Button[] buttons = _elements.OfType<Button>().ToArray();
        int maxY = buttons.Max(el => el.PosY);

        if (_currentY == maxY)
            return;

        for (int y = _currentY + 1; y <= maxY; y++)
        {
            if (buttons.Any(el => el.PosY == y))
            {
                var oldButton = buttons.First(el => el.PosY == _currentY && el.PosX == _currentX);
                oldButton.Toggle();

                var newButton = buttons.First(el => el.PosY == y);
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

        Button[] buttons = _elements.OfType<Button>().ToArray();

        for (int x = _currentX - 1; x >= 0; x--)
        {
            if (buttons.Any(el => el.PosY == _currentY && el.PosX == x))
            {
                var oldButton = buttons.First(el => el.PosY == _currentY && el.PosX == _currentX);
                oldButton.Toggle();

                var newButton = buttons.First(el => el.PosY == _currentY && el.PosX == x);
                newButton.Toggle();

                _currentX = x;
                break;
            }
        }
    }
    
    private void MoveCursorRight()
    {
        Button[] buttons = _elements.OfType<Button>().ToArray();
        int maxX = buttons.Where(el => el.PosY == _currentY).Max(el => el.PosX);

        if (_currentX == maxX)
            return;

        for (int x = _currentX + 1; x <= maxX; x++)
        {
            if (buttons.Any(el => el.PosY == _currentY && el.PosX == x))
            {
                var oldButton = buttons.First(el => el.PosY == _currentY && el.PosX == _currentX);
                oldButton.Toggle();

                var newButton = buttons.First(el => el.PosY == _currentY && el.PosX == x);
                newButton.Toggle();

                _currentX = x;
                break;
            }
        }
    }
}