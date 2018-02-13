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
		return entity.hasView && entity.hasIndex;
    }

    protected override void Execute(List<GameEntity> entities)
    {       
		if (entities.Count == 0 && !entities [0].hasPosition)
        {
            return;
        }
		int column = entities [0].index.i;
		int row = entities [0].index.j;
        var settings = _contexts.game.settings.value;

        var topPieces = new List<GameEntity>(settings.width);
		var allEntities = _contexts.game.GetEntities ();
		foreach (var localPiece in allEntities)
        {
			if (localPiece.hasView && localPiece.hasIndex && !localPiece.hasDestroyed) {
				if (localPiece.index.i == column && localPiece.index.j > row) {
					topPieces.Add (localPiece);
				}
			}
        }

        bool needPieceCreate = true;
        foreach (var localPiece in topPieces)
        {
            if (localPiece.hasBlocker && localPiece.blocker.value)
            {
                needPieceCreate = false;
                break;
            }
			localPiece.ReplaceIndex(localPiece.index.i, localPiece.index.j - 1);            
        }

        if (needPieceCreate)
        {
            var entity = _contexts.game.CreateEntity();
            string prefabResourceName = settings.piecePrefabResourceName + Random.Range(1, 7);
            entity.AddResource(prefabResourceName);           
			entity.AddIndex(column, settings.height - 1);
        }
    }
}
