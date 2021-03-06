﻿using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class DestroyedSystem : ReactiveSystem<GameEntity>
{
    public DestroyedSystem (Contexts contexts) : base (contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger (IContext<GameEntity> context)
    {
        return context.CreateCollector (GameMatcher.Destroyed);
    }

    protected override bool Filter (GameEntity entity)
    {
        return entity.hasDestroyed;
    }

    protected override void Execute (List<GameEntity> entities)
    {        
        foreach (var entity in entities) {
            if (entity.hasView && entity.hasPosition && entity.hasDestroyed && entity.destroyed.value) {
                entity.RemovePosition ();
                DestroyEntity (entity);
            }
        }
    }

    private void DestroyEntity (GameEntity entity)
    {
        ViewComponent view = entity.view;
        if(view == null)
        {
            entity.Destroy();
            return;
        }
        var spriteRenderer = view.value.GetComponent<SpriteRenderer> ();
        spriteRenderer.transform.DetachChildren ();

        spriteRenderer.sortingOrder = 2;
        var collinder2D = view.value.GetComponent<Collider2D> ();
        if (collinder2D != null) {
            collinder2D.enabled = false;
        }        
        spriteRenderer.DOFade(0, 0.45f);
        view.value.gameObject.transform
                .DOScale(Vector3.one * 1.5f, 0.5f)
                .OnComplete(() => {
                    view.value.gameObject.Unlink();
                    Object.Destroy(view.value.gameObject);
                    entity.Destroy();
                });    
    }
}
