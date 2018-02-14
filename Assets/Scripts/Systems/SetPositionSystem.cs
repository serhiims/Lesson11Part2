using System.Collections.Generic;
using Entitas;
using UnityEngine;
using DG.Tweening;

public class SetPositionSystem : ReactiveSystem<GameEntity>
{
    private Settings settings;
    Contexts _contexts;

    public SetPositionSystem (Contexts contexts) : base (contexts.game)
    {
        _contexts = contexts;        
    }

    protected override ICollector<GameEntity> GetTrigger (IContext<GameEntity> context)
    {
        return context.CreateCollector (GameMatcher.Index);
    }

    protected override bool Filter (GameEntity entity)
    {
        return entity.hasView && entity.hasIndex;
    }

    protected override void Execute (List<GameEntity> entities)
    {
        var settings = _contexts.game.settings.value;
    
        foreach (var entity in entities) {
            var transform = entity.view.value.transform;
            var posX = settings.startingPosition.x + entity.index.i * settings.spacing + settings.positionOffset.x;
            var posY = settings.startingPosition.y + entity.index.j * settings.spacing + settings.positionOffset.y;

            if (entity.index.j == settings.height - 1 || !entity.hasPosition) {
                transform.position = new Vector2 (posX, posY + settings.spacing);
            }
            entity.view.value.gameObject.transform.DOMove (new Vector3 (posX ,posY, 0f), settings.moveSpeed);
            entity.ReplacePosition (posX, posY);
        }
    }
}
