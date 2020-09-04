using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace BinarySearchTree
{
    class Node
    {
        public int value;
        public int sum;
        public int layer;
        public int position;
        public string path;
        public bool isEven;
    }

    class Tree
    {
        public List<Node> TreeNodes = new List<Node>();
        public Node Initialise(int value)
        {
            var (layer, position) = GetNodeCoordinates(TreeNodes.LastOrDefault());
            var node = new Node();
            node.value = value;
            node.isEven = value % 2 == 0;
            node.layer = layer;
            node.position = position;
            var (nodeSum, nodePath) = GetNodeSumMaxAndPath(node);
            node.sum = nodeSum;
            node.path = nodePath;

            return node;
        }

        public Node GetOptimalPathNode(string inputVal)
        {
            Node result = null;

            try
            {
                var stringNumbers = inputVal.Split(new char[3] { ' ','\r','\n'}).Where(s => !string.IsNullOrEmpty(s)).ToArray();
                int[] numbers = Array.ConvertAll(stringNumbers, s => int.Parse(s));

                foreach (var number in numbers)
                {
                    TreeNodes.Add(Initialise(number));
                }

                var maxLayer = TreeNodes.Select(s => s.layer).Max();
                var maxSum = TreeNodes.Select(s => s.sum).Max();
                return TreeNodes.FirstOrDefault(s => s.sum == maxSum && s.layer == maxLayer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        private (int layer, int position) GetNodeCoordinates(Node parentNode)
        {
            var layer = 0;
            var position = 0;

            if(parentNode != null)
            {
                var isLastInLayer = parentNode.position == parentNode.layer;
                position = isLastInLayer ? 0 : parentNode.position + 1;
                layer = isLastInLayer ? parentNode.layer + 1 : parentNode.layer;
            }

            return (layer, position);
        }

        private (int nodeSum, string nodePath) GetNodeSumMaxAndPath(Node node)
        {
            if(node.layer == 0
            && node.layer == 0)
            {
                return (node.value, $"{node.value}");
            }

            var parentNodeLeft = TreeNodes.FirstOrDefault(s => s.layer == node.layer - 1 
                                                            && s.position == node.position - 1
                                                            && s.isEven != node.isEven);
            var parentNodeTop = TreeNodes.FirstOrDefault(s => s.layer == node.layer - 1 
                                                           && s.position == node.position
                                                           && s.isEven != node.isEven);

            var parentNodeLeftSum = parentNodeLeft?.sum ?? 0;
            var parentNodeTopSum = parentNodeTop?.sum ?? 0;
            var parentPath = parentNodeLeftSum > parentNodeTopSum ? parentNodeLeft?.path : parentNodeTop?.path;
            var parentSumMax = parentNodeLeftSum > parentNodeTopSum ? parentNodeLeftSum : parentNodeTopSum;

            return (parentSumMax > 0 ? parentSumMax + node.value : parentSumMax, $"{parentPath}, {node.value}");
        }

    }
}