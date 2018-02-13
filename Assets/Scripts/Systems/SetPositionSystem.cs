using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SetPositionSystem : ReactiveSystem<GameEntity>
{
    private static Vector2 _positionOffset;
    private Settings settings;
    Contexts _contexts;
    public SetPositionSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;        
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
		return context.CreateCollector(GameMatcher.Index);
    }

    protected override bool Filter(GameEntity entity)
    {
		return entity.hasView && entity.hasIndex;
    }

    protected override void Execute(List<GameEntity> entities)
    {
		var settings = _contexts.game.settings.value;
        if (_positionOffset.Equals(Vector2.zero))
        {
            _positionOffset = new Vector2(-settings.width * 0.5f * settings.spacing + settings.spacing * 0.5f, -settings.height * 0.5f * settings.spacing + settings.spacing * 0.5f);
        }
        foreach (var entity in entities)
        {
            var transform = entity.view.value.transform;
			var posX = settings.startingPosition.x + entity.index.i * settings.spacing + _positionOffset.x;
			var posY = settings.startingPosition.y + entity.index.j * settings.spacing + _positionOffset.y;
            transform.position = new Vector2(posX, posY);
			entity.ReplacePosition (posX, posY);
        }
    }
}
