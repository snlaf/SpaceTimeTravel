using System;

public class Engine
{
    public string Type { get; set; }
    public double Power { get; set; }

    public Engine(string type, double power)
    {
        Type = type;
        Power = power;
    }

    public double CalculateFuelConsumption(double distance)
    {
        switch (Type)
        {
            case "C-class":
                return distance * 0.1; // Линейный расход топлива
            case "E-class":
                return Math.Exp(distance * 0.1); // Экспоненциальный расход топлива
            default:
                throw new ArgumentException("Unknown engine type");
        }
    }
}