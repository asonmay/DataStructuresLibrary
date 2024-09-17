using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphStuff
{
    public class Edge<T>
    {
        public Node<T> StartingNode;
        public Node<T> EndingNode;
        public float Weight;

        public Edge(Node<T> startingNode, Node<T> endingNode, float weight) => (StartingNode, EndingNode, Weight) = (startingNode, endingNode, weight);
    }

    public class Node<T>
    {
        public T Value;
        public List<Edge<T>> Neighbors;
        public Point Pos;

        public Node(T value, Point pos) => (Value, Neighbors, Pos) = (value, new List<Edge<T>>(), pos);

        public void AddNeighbor(Node<T> node) => Neighbors.Add(new Edge<T>(this, node, 1));
    }

    public class NodeWrapper<T>
    {
        public Node<T> WrappedNode { get; set; }
        public double DistanceFromStart { get; set; }
        public NodeWrapper<T> Founder { get; set; }
        public double DistanceFromEnd { get; set; }

        public NodeWrapper(Node<T> node, double distanceFromStart, double distanceFromEnd, NodeWrapper<T> founder) => (WrappedNode, DistanceFromStart, DistanceFromEnd, Founder) = (node, distanceFromStart, distanceFromEnd, founder);
    }

    public class Graph<T>
    {
        public List<Node<T>> Nodes;
        
        public Graph() => Nodes = new List<Node<T>>();

        public void AddNode(T value, Point pos) => Nodes.Add(new Node<T>(value, pos));

        public void AddEdge(Node<T> startingNode, Node<T> endingNode) => startingNode.AddNeighbor(endingNode);

        public List<Node<T>> Search(NodeWrapper<T> startingNode, NodeWrapper<T> endingNode, Func<List<NodeWrapper<T>>, NodeWrapper<T>> selection, Func<NodeWrapper<T>, NodeWrapper<T>, double> heuristic)
        {
            List<NodeWrapper<T>> visitedNodes = new List<NodeWrapper<T>>();
            NodeWrapper<T> currentNode = startingNode;
            List<NodeWrapper<T>> frontier = new List<NodeWrapper<T>>();

            while (currentNode.WrappedNode != endingNode.WrappedNode)
            {
                for (int i = 0; i < currentNode.WrappedNode.Neighbors.Count; i++)
                {
                    double distanceFromStart = currentNode.DistanceFromStart + currentNode.WrappedNode.Neighbors[i].Weight;
                    double distanceFromEnd = distanceFromStart + heuristic(currentNode, endingNode);
                    NodeWrapper<T> neighbor = new NodeWrapper<T>(currentNode.WrappedNode.Neighbors[i].EndingNode, distanceFromStart, distanceFromEnd, currentNode);
                    if (!visitedNodes.Contains(neighbor))
                    {
                        frontier.Add(neighbor);
                    }
                }

                visitedNodes.Add(currentNode);
                currentNode = selection(frontier);
            }

            List<Node<T>> path = new List<Node<T>>();
            while(currentNode.WrappedNode != startingNode.WrappedNode)
            {
                path.Add(currentNode.WrappedNode);
                currentNode = currentNode.Founder;
            }
            path.Add(currentNode.WrappedNode);
            path.Reverse();

            return path;
        }
    }

    public static class GraphFunctions
    {
        static NodeWrapper<int> DFSSelection(List<NodeWrapper<int>> nodes)
        {
            NodeWrapper<int> node = nodes[^1];
            nodes.Remove(node);
            return node;
        }

        static NodeWrapper<int> BFSSelection(List<NodeWrapper<int>> nodes)
        {
            NodeWrapper<int> node = nodes[0];
            nodes.Remove(node);
            return node;
        }

        static NodeWrapper<int> DijstrasSelection(List<NodeWrapper<int>> nodes)
        {
            NodeWrapper<int> node = nodes[0];

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].DistanceFromStart < node.DistanceFromStart)
                {
                    node = nodes[i];
                }
            }
            nodes.Remove(node);

            return node;
        }

        static NodeWrapper<int> AStarSelection(List<NodeWrapper<int>> nodes)
        {
            NodeWrapper<int> node = nodes[0];

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].DistanceFromEnd < node.DistanceFromEnd)
                {
                    node = nodes[i];
                }
            }
            nodes.Remove(node);

            return node;
        }

        static double ManhattanHeuristic(NodeWrapper<int> startingNode, NodeWrapper<int> endingNode)
        {
            float dx = Math.Abs(startingNode.WrappedNode.Pos.X - endingNode.WrappedNode.Pos.X);
            float dy = Math.Abs(startingNode.WrappedNode.Pos.Y - endingNode.WrappedNode.Pos.Y);
            return dx + dy;
        }

        static double Diagonal(NodeWrapper<int> startingNode, NodeWrapper<int> endingNode)
        {
            float dx = Math.Abs(startingNode.WrappedNode.Pos.X - endingNode.WrappedNode.Pos.X);
            float dy = Math.Abs(startingNode.WrappedNode.Pos.Y - endingNode.WrappedNode.Pos.Y);
            return (dx + dy) + (Math.Sqrt(2) - 2) * Math.Min(dx,dy);
        }

        static double Euclidean(NodeWrapper<int> startingNode, NodeWrapper<int> endingNode)
        {
            float dx = Math.Abs(startingNode.WrappedNode.Pos.X - endingNode.WrappedNode.Pos.X);
            float dy = Math.Abs(startingNode.WrappedNode.Pos.Y - endingNode.WrappedNode.Pos.Y);
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
