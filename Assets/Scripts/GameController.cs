using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Settings settings;

    private Contexts _contexts;
    private Systems _systems;

    private void Start()
    {
        _contexts = new Contexts();
        _systems = CreateSystems(_contexts);

        _contexts.game.SetSettings(settings);
        
        _systems.Initialize();
    }

    private void Update()
    {
        _systems.Execute();
    }

    private Systems CreateSystems(Contexts contexts)
    {
        return new Feature("Game")
            .Add(new InitializePlayerSystem(contexts))

            .Add(new AddViewSystem(contexts))
            .Add(new SetPositionSystem(contexts))
            ;
    }
}
