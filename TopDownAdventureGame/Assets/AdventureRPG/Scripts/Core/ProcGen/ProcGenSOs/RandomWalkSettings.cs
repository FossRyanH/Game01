using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RandomWalkSettings", menuName = "Scriptable Objects/RandomWalkSettings")]
public class RandomWalkSettings : ScriptableObject
{
    [field: SerializeField] public int WalkLength { get; set; } = 24;
    [field: SerializeField] public int Iterations { get; set; } = 10;
    [field: SerializeField] public Vector3Int StartingPos { get; set; } = Vector3Int.zero;
}
