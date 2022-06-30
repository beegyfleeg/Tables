namespace TablesLib;

/// <summary>
/// A section of the console window on which to render Nodes.
/// </summary>
public class RenderArea
{
    /// <value>List of active Render Areas, in order of creation.</value>
    public static IReadOnlyList<RenderArea> ActiveAreas => activeAreas;

    /// <value>The minimum height of the Render Area.</value>
    public int Height { get; private set; }

    /// <value>
    /// The number of columns from the left of the console window the Render Area begins.
    /// </value>
    public int OffsetX { get; private set; }

    /// <value>
    /// The number of rows from the top of the console window the Render Area begins.
    /// </value>
    public int OffsetY { get; private set; }

    /// <value>The minimum width of the RenderArea.</value>
    public int Width { get; private set; }

    static readonly List<RenderArea> activeAreas = new();
    readonly static int originX = Console.CursorLeft;
    readonly static int originY = Console.CursorTop;

    public void Close()
    {
        activeAreas.Remove(this);
    }

    internal static void Print(string contents, int left, int top)
    {
        int maximumLength = Math.Min(contents.Length, Console.WindowWidth - left + originX);
        Console.SetCursorPosition(left, top);
        Console.Write(contents[..maximumLength]);
    }

    public RenderArea(int width, int height, int offsetX, int offsetY)
    {
        Height = height;
        OffsetX = offsetX;
        OffsetY = offsetY;
        Width = width;

        // Check for overlap with other active RenderAreas.
        foreach (RenderArea otherArea in activeAreas)
            if ((otherArea.OffsetX <= offsetX + width
                || otherArea.OffsetX + otherArea.Width >= offsetX)
                && (otherArea.OffsetY <= offsetY + height
                || otherArea.OffsetY + otherArea.Height >= offsetY))
                throw new ArgumentException("Render Area overlaps other active Render Areas");

        // Prepare Console area.
        string clearString = new(' ', width);
        for (int top = offsetY; top <= offsetY + height; top++)
            Print(clearString, offsetX, top);

        activeAreas.Add(this);
    }

    ~RenderArea() => Close();
}