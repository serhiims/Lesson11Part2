using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SetPositionSystem : ReactiveSystem<GameEntity>
{
    private static Vector2 _positionOffset;
    private Settings _settings;
    Contexts _contexts;
    public SetPositionSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        
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
        if (_settings == null)
        {
            _settings = _contexts.game.settings.value;
        }
        if (_positionOffset == null)
        {
            _positionOffset = new Vector2(-_settings.width * 0.5f * _settings.spacing + _settings.spacing * 0.5f, -_settings.height * 0.5f * _settings.spacing + _settings.spacing * 0.5f);
        }
        foreach (var entity in entities)
        {
            var transform = entity.view.value.transform;
            Vector2 position = new Vector2(entity.position.x * _settings.spacing, entity.position.y * _settings.spacing);
            transform.position = new Vector2(_settings.startingPosition.x + position.x + _positionOffset.x, _settings.startingPosition.y + position.y + _positionOffset.y);
        }
    }
}
