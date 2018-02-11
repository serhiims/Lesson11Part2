using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SetPositionSystem : ReactiveSystem<GameEntity>
{
    public SetPositionSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.view.value.transform;
            transform.position = new Vector2(entity.position.x, entity.position.y);
        }
    }
}
