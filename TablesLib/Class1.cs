namespace TablesLib;

/// <summary>
/// A section of the console window on which to render Nodes.
/// </summary>
public class RenderArea
{
    /// <value>The minimum height of the Render Area.</value>
    public uint Height { get; private set; }

    /// <value>
    /// The number of characters from the left of the console window the Render Area begins.
    /// </value>
    public uint OffsetX { get; private set; }

    /// <value>
    /// The number of characters from the top of the console window the Render Area begins.
    /// </value>
    public uint OffsetY { get; private set; }

    /// <value>The minimum width of the RenderArea.</value>
    public uint Width { get; private set; }

    static readonly List<RenderArea> activeAreas = new();

    public RenderArea(uint width, uint height, uint offsetX, uint offsetY)
    {
        Height = height;
        OffsetX = offsetX;
        OffsetY = offsetY;
        Width = width;

        // Check for overlap with other active RenderAreas.
        foreach (RenderArea renderArea in activeAreas)
        {

        }

        activeAreas.Add(this);
    }
}