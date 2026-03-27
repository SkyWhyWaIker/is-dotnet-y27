namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation;

public interface IScenario
{
    string Name { get; }

    Task Run();
}