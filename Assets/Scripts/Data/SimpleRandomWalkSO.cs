using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Parameters_", menuName = "PCG/SimpleRAndomWalkData")]
public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 5, walkLength = 5;
    public bool startRandomlyEachIteration = true;
}