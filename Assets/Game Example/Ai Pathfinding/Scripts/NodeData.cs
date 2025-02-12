using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeData
{
    public float gScore;
    public float hScore;
    public Node cameFrom;

    public float Fscore()
    {
        return gScore + hScore;
    }
}
