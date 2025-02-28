using System.Collections.Generic;

public class Route
{
    public List<Segment> Segments { get; set; } = new List<Segment>();

    public void AddSegment(Segment segment)
    {
        Segments.Add(segment);
    }
}