using System.Collections.Generic;
using UnityEngine;

namespace Dijkstra
{
    public class Node
    {
        public bool IsWalkable { get; set; }
        public int Id { get; set; }
        public Dictionary<Node, float> Neighbors { get; set; }
        public bool HasBeenVisted { get; set; }
        public Vector3 Position { get; set; }

        public Node(bool isWalkable, Dictionary<Node, float> neighbors)
        {
            IsWalkable = isWalkable;
            Neighbors = neighbors;
        }

        public Node()
        {
        }
        public void Reset()
        {
            HasBeenVisted = false;
        }
    }
}
