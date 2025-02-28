public class Ship
{
    public string Name { get; set; }
    public Engine Engine { get; set; }
    public Shield Shield { get; set; }
    public bool HasPhotonDeflector { get; set; }
    public JumpEngine JumpEngine { get; set; }

    public Ship(string name, Engine engine, Shield shield, bool hasPhotonDeflector = false, JumpEngine jumpEngine = null)
    {
        Name = name;
        Engine = engine;
        Shield = shield;
        HasPhotonDeflector = hasPhotonDeflector;
        JumpEngine = jumpEngine;
    }
}