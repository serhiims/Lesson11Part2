using Entitas;
using UnityEngine;

public class InitializePlayerSystem : IInitializeSystem
{
	private Contexts _contexts;

	public InitializePlayerSystem (Contexts contexts)
	{
		_contexts = contexts;
	}

	public void Initialize ()
	{
		var settings = _contexts.game.settings.value;
		for (int i = 0; i < settings.width; i++) { 
			for (int j = 0; j < settings.height; j++) {
				var entity = _contexts.game.CreateEntity ();
				bool isBlocker = Random.value < settings.blockerProbability;
				string prefabResourceName = isBlocker ? settings.blockerPrefabResourceName : settings.piecePrefabResourceName + Random.Range (1, 7);
				entity.AddResource (prefabResourceName);
				if (isBlocker) {
					entity.AddBlocker (isBlocker);
				}
				entity.AddIndex (i, j);
          
			}
		}       
	}
}
