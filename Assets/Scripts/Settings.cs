using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu]
[Game, Unique]
public class Settings : ScriptableObject
{
	public int width = 10;
	public int height = 10;
	public float spacing = 1f;
	public string piecePrefabResourceName;
	public string blockerPrefabResourceName;
	public Vector2 startingPosition;
	public float blockerProbability = 0.1f;
	public float moveSpeed = 0.05f;
	public Vector2 positionOffset = new Vector2(-4.5f, -4.5f);
}
