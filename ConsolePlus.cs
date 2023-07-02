using System.Drawing;
using Console = Colorful.Console;

public class ConsolePlus
{
    private static object _locker = new object();
    private static Dictionary<string, int> _map = new Dictionary<string, int>();

    public static void WriteLine(string name, object value, Color color = default)
    {
        lock (_locker)
        {
            _map.Add(name, Console.CursorTop);
            Console.WriteLine(value, color == default ? Color.Gray : color);
        }
    }

    public static void EditLine(string name, object value, Color color = default)
    {
        lock (_locker)
        {
            if (_map.ContainsKey(name))
            {
                int index = _map[name];
                EditLine(index, value.ToString()!, color == default ? Color.Gray : color);
            }
        }
    }

    //https://stackoverflow.com/questions/34423986/c-sharp-rewrite-edit-line-while-program-is-running
    private static void EditLine(int index, string value, Color color)
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, index);
        Console.Write(value, color);
        Console.WriteLine(new string(' ', Console.WindowWidth - value.Length), color);
        Console.SetCursorPosition(0, currentLineCursor);
    }
}
