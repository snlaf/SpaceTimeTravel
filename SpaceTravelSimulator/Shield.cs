public class Shield
{
    public int Strength { get; set; }

    public Shield(int strength)
    {
        Strength = strength;
    }

    public bool CanDeflect(int damage)
    {
        if (Strength >= damage)
        {
            Strength -= damage;
            return true;
        }
        return false;
    }
}