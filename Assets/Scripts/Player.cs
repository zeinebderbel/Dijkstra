using Dijkstra;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Grille Grille;
    [SerializeField]
    public float Speed = 0.9f;
    protected Node Current;
    protected Path Path = new Path();
    public GameObject Target;
    public Node end;
    // Update is called once per frame
    void Update()
    {
        Node start = new Node();
       

        if (Input.GetMouseButtonDown(0))
        {
            foreach (var n in Grille.Graph.Nodes)
                n.Reset();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                start = GetNearestNode(transform.position);
                end = GetNearestNode(hit.point);

                Path = Grille.Graph.GetShortestPath(start, end);
                if (Path != null && Path.Nodes != null)
                {
                    Target.gameObject.SetActive(true);
                    Follow();
                }

            }
        }
        if (Current != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Current.Position, Speed);
        }
    }
    public void Follow()
    {
        StopCoroutine("FollowPath");
        transform.position = Path.Nodes[0].Position;
        StartCoroutine("FollowPath");
    }
    IEnumerator FollowPath()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.update += Update;
#endif
        var e = Path.Nodes.GetEnumerator();
        while (e.MoveNext())
        {
            Current = e.Current;

            // Wait until we reach the current target node and then go to next node
            yield return new WaitUntil(() =>
            {
                return transform.position == Current.Position;
            });
        }
        Current = null;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.update -= Update;
#endif
    }


    public Node GetNearestNode(Vector3 targetPosition)
    {
        var min = float.MaxValue;
        var nearestNode = new Node();
        foreach (var node in Grille.Graph.Nodes)
        {
            var distance = Vector3.Distance(node.Position, targetPosition);
            if (min > distance)
            {
                min = distance;
                nearestNode = node;
            }
        }
        return nearestNode;
    }
}
