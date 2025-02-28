public class Segment
{
    public string Environment { get; set; }
    public double Distance { get; set; }

    public Segment(string environment, double distance)
    {
        Environment = environment;
        Distance = distance;
    }
}