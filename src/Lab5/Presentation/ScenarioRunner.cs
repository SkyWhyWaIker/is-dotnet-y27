using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Factories;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation;

public class ScenarioRunner(AbstractScenarioFactory factory)
{
    public async Task Run()
    {
        var scenarios = new List<IScenario>
        {
            factory.AdminLogin(),
            factory.UserLogin(),
            factory.AdminLogout(),
            factory.UserLogout(),
            factory.AdminRegister(),
            factory.UserRegister(),
            factory.UserDeposite(),
            factory.UserWithdraw(),
            factory.UserGetOperationsHistory(),
            factory.UserGetBalance(),
        };

        SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
            .Title("Select action")
            .AddChoices(scenarios)
            .UseConverter(x => x.Name);

        IScenario scenario = AnsiConsole.Prompt(selector);

        await scenario.Run().ConfigureAwait(false);
    }
}