using Entitas;
using UnityEngine;

public class InputSystem : IExecuteSystem
{
    private Contexts _contexts;

    public InputSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    { 
        if(Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100);
            if (hit.collider != null)
            {
                var mousePos = hit.collider.transform.position;
                var entities = _contexts.game.GetEntities();
                foreach (var entity in entities)
                {

                    if (!entity.hasView || !entity.hasPosition || entity.hasBlocker || entity.hasDestroyed)
                    {
                        continue;
                    }
                    Collider2D c2d = entity.view.value.GetComponent<Collider2D>();
                    if (c2d != null && c2d.bounds.Contains(new Vector3(mousePos.x, mousePos.y, 0)))
                    {
                        entity.AddDestroyed(true);
                    }
                       
                }
            }
        }		
    }
}
