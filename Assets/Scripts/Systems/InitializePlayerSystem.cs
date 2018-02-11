using Entitas;
using UnityEngine;

public class InitializePlayerSystem : IInitializeSystem
{
    private Contexts _contexts;

    public InitializePlayerSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var settings = _contexts.game.settings.value;
        var piecesPosOffset = new Vector2(-settings.width * 0.5f * settings.spacing + settings.spacing * 0.5f, -settings.height * 0.5f * settings.spacing + settings.spacing * 0.5f);
        for (int i = 0; i < settings.width; i++)
        { 
            for (int j = 0; j < settings.height; j++)
            {
                var entity = _contexts.game.CreateEntity();
                var position = new Vector2(i * settings.spacing, j * settings.spacing);
                var isBocker = Random.value < settings.blockerProbability;
                var prefabResourceName = isBocker ? settings.blockerPrefabResourceName : settings.piecePrefabResourceName + Random.Range(1, 6);
                entity.AddResource(prefabResourceName);
                entity.AddBlocker(isBocker);
                entity.AddPosition(settings.startingPosition.x + position.x + piecesPosOffset.x, settings.startingPosition.y + position.y + piecesPosOffset.y);
                entity.isPlayer = true;
            }
        }
        

       
    }
}
