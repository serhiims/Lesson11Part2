using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class ChangePoitionSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public ChangePoitionSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Destroyed);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDestroyed;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity targetEntity = null;
        foreach (var entity in entities)
        {
            if (entity.hasDestroyed && entity.destroyed.value)
            {
                targetEntity = entity;
                break;
            }
        }
        if (targetEntity == null)
        {
            return;
        }
        float column = targetEntity.position.x;
        float row = targetEntity.position.y;
        var settings = _contexts.game.settings.value;

        var topPieces = new List<GameEntity>(settings.width);
        for (int i = 0; i < entities.Count; i++)
        {
            var localPiece = entities[i];

            if (localPiece.position.x == column && localPiece.position.y > row)
            {
                topPieces.Add(localPiece);
            }
        }

        bool needPieceCreate = true;
        foreach (var localPiece in topPieces)
        {
            if (localPiece.hasBlocker)
            {
                needPieceCreate = false;
                break;
            }
            localPiece.AddPosition(localPiece.position.x, localPiece.position.y - 1);            
        }

        if (needPieceCreate)
        {
            var entity = _contexts.game.CreateEntity();
            bool isBlocker = Random.value < settings.blockerProbability;
            string prefabResourceName = isBlocker ? settings.blockerPrefabResourceName : settings.piecePrefabResourceName + Random.Range(1, 7);
            entity.AddResource(prefabResourceName);
            if (isBlocker)
            {
                entity.AddBlocker(isBlocker);
            }
            entity.AddPosition(column, settings.height - 1);
            entity.AddMoved(true);
        }
    }
}
