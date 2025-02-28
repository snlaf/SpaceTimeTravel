using System;
using System.Collections.Generic;

public class SpaceTravelSimulator
{
    private Random random;

    public SpaceTravelSimulator(int seed)
    {
        random = new Random(seed);
    }

    public (double travelTime, double fuelConsumed, string outcome) TravelRoute(Route route, Ship ship)
    {
        double travelTime = 0;
        double fuelConsumed = 0;

        foreach (var segment in route.Segments)
        {
            switch (segment.Environment)
            {
                case "Обычный космос":
                    var meteorDamage = RandomDamage(1, 2); // Случайный урон от метеоритов или астероидов
                    if (!ship.Shield.CanDeflect(meteorDamage))
                        return (travelTime, fuelConsumed, "Уничтожение корабля");

                    fuelConsumed += ship.Engine.CalculateFuelConsumption(segment.Distance);
                    travelTime += segment.Distance / ship.Engine.Power;
                    break;

                case "Туманности высокой плотности":
                    if (ship.JumpEngine == null || (ship.JumpEngine.Type != "Alpha" && ship.JumpEngine.Type != "Omega" && ship.JumpEngine.Type != "Gamma"))
                        return (travelTime, fuelConsumed, "Потеря корабля");

                    var antimatterBurst = RandomEvent(0.1); // Вероятность антиматериальной вспышки
                    if (antimatterBurst && !ship.HasPhotonDeflector)
                        return (travelTime, fuelConsumed, "Гибель экипажа"); // Исправлено на "Гибель экипажа"

                    fuelConsumed += ship.Engine.CalculateFuelConsumption(segment.Distance);
                    travelTime += segment.Distance / ship.Engine.Power;
                    break;

                case "Туманности с нитринными частицами":
                    var cosmicKitAttack = RandomEvent(0.2); // Вероятность атаки космо-кита
                    if (cosmicKitAttack)
                    {
                        if (ship.Name == "Астра")
                            return (travelTime, fuelConsumed, "Уничтожение корабля");
                        else if (ship.Name == "Титан")
                            ship.Shield.Strength = 0;
                    }

                    fuelConsumed += ship.Engine.CalculateFuelConsumption(segment.Distance);
                    travelTime += segment.Distance / ship.Engine.Power;
                    break;

                default:
                    throw new ArgumentException("Unknown environment");
            }
        }

        return (travelTime, fuelConsumed, "Успешное прохождение");
    }

    private int RandomDamage(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    private bool RandomEvent(double probability)
    {
        return random.NextDouble() < probability;
    }
}