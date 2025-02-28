using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class SpaceTravelSimulatorTests
{
    private Ship shipAstra;
    private Ship shipOrien;
    private Ship shipHelios;
    private Ship shipCentaur;
    private Ship shipTitan;
    private Ship shipProghulochnyyChelnok;
    private Ship shipVaklas;

    [SetUp]
    public void Setup()
    {
        shipAstra = new Ship("Астра", new Engine("C-class", 10), new Shield(2));
        shipOrien = new Ship("Орион", new Engine("E-class", 10), new Shield(2), true);
        shipHelios = new Ship("Гелиос", new Engine("E-class", 10), new Shield(2));
        shipCentaur = new Ship("Центавр", new Engine("Omega", 10), new Shield(2));
        shipTitan = new Ship("Титан", new Engine("E-class", 10), new Shield(2));
        shipProghulochnyyChelnok = new Ship("Прогулочный челнок", new Engine("C-class", 10), new Shield(2));
        shipVaklas = new Ship("Ваклас", new Engine("E-class", 10), new Shield(2));
    }

    [Test]
    public void TestHighDensityNebulaWithoutSuitableEngine()
    {
        Ship ship1 = new Ship("Астра", new Engine("C-class", 10), new Shield(2));
        Ship ship2 = new Ship("Прогулочный челнок", new Engine("C-class", 10), new Shield(2));

        Route route = new Route();
        route.AddSegment(new Segment("Туманности высокой плотности", 100));

        SpaceTravelSimulator simulator = new SpaceTravelSimulator(42); // Фиксированный seed

        var result1 = simulator.TravelRoute(route, ship1);
        var result2 = simulator.TravelRoute(route, ship2);

        Assert.AreEqual("Потеря корабля", result1.outcome);
        Assert.AreEqual("Потеря корабля", result2.outcome);
    }

    [Test]
    public void TestAntimatterBurstWithAndWithoutPhotonDeflector()
    {
        Ship shipWithDeflector = new Ship("Орион", new Engine("E-class", 10), new Shield(2), true);
        Ship shipWithoutDeflector = new Ship("Астра", new Engine("C-class", 10), new Shield(2));

        Route route = new Route();
        route.AddSegment(new Segment("Туманности высокой плотности", 100));

        SpaceTravelSimulator simulator = new SpaceTravelSimulator(42); // Фиксированный seed

        var resultWithDeflector = simulator.TravelRoute(route, shipWithDeflector);
        var resultWithoutDeflector = simulator.TravelRoute(route, shipWithoutDeflector);

        Assert.AreNotEqual("Гибель экипажа", resultWithDeflector.outcome);
        Assert.AreEqual("Потеря корабля", resultWithoutDeflector.outcome);
    }

    [Test]
    public void TestCosmicKitInNitrogenNebula()
    {
        Route route = new Route();
        route.AddSegment(new Segment("Туманности с нитринными частицами", 100));

        SpaceTravelSimulator simulator = new SpaceTravelSimulator(42); // Фиксированный seed

        var resultAstra = simulator.TravelRoute(route, shipAstra);
        var resultTitan = simulator.TravelRoute(route, shipTitan);
        var resultHelios = simulator.TravelRoute(route, shipHelios);

        Assert.AreEqual("Успешное прохождение", resultAstra.outcome);
        Assert.AreEqual(0, shipTitan.Shield.Strength);
        Assert.AreEqual("Успешное прохождение", resultHelios.outcome);
    }

    [Test]
    public void TestShortRouteInNormalSpace()
    {
        Route route = new Route();
        route.AddSegment(new Segment("Обычный космос", 50));

        SpaceTravelSimulator simulator = new SpaceTravelSimulator(42); // Фиксированный seed

        var resultProghulochnyyChelnok = simulator.TravelRoute(route, shipProghulochnyyChelnok);
        var resultVaklas = simulator.TravelRoute(route, shipVaklas);

        Assert.Less(resultProghulochnyyChelnok.fuelConsumed, resultVaklas.fuelConsumed);
    }

    [Test]
    public void TestLongRouteThroughSubspaceChannels()
    {
        Ship shipCentaur = new Ship("Центавр", new Engine("C-class", 10), new Shield(2), false, new JumpEngine("Omega")); // Добавлен JumpEngine
        Ship shipAstra = new Ship("Астра", new Engine("C-class", 10), new Shield(2));

        Route route = new Route();
        route.AddSegment(new Segment("Туманности высокой плотности", 200));

        SpaceTravelSimulator simulator = new SpaceTravelSimulator(42); // Фиксированный seed

        var resultCentaur = simulator.TravelRoute(route, shipCentaur);
        var resultAstra = simulator.TravelRoute(route, shipAstra);

        Assert.AreNotEqual("Потеря корабля", resultCentaur.outcome);
        Assert.AreEqual("Потеря корабля", resultAstra.outcome);
    }

    [Test]
    public void TestRouteThroughNitrogenNebula()
    {
        Route route = new Route();
        route.AddSegment(new Segment("Туманности с нитринными частицами", 100));

        SpaceTravelSimulator simulator = new SpaceTravelSimulator(42); // Фиксированный seed

        var resultOrien = simulator.TravelRoute(route, shipOrien);

        Assert.AreEqual("Успешное прохождение", resultOrien.outcome);
    }

    [Test]
    public void TestComplexRouteWithVariousObstacles()
    {
        Ship shipOrien = new Ship("Орион", new Engine("E-class", 10), new Shield(2), true, new JumpEngine("Gamma")); // Добавлен JumpEngine

        Route route = new Route();
        route.AddSegment(new Segment("Обычный космос", 50));
        route.AddSegment(new Segment("Туманности высокой плотности", 100));
        route.AddSegment(new Segment("Туманности с нитринными частицами", 100));

        SpaceTravelSimulator simulator = new SpaceTravelSimulator(42); // Фиксированный seed

        var resultOrien = simulator.TravelRoute(route, shipOrien);

        Assert.AreEqual("Успешное прохождение", resultOrien.outcome);
    }
}