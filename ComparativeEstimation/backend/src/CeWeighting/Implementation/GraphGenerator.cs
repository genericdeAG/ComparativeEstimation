using System;
using System.Collections.Generic;
using System.Linq;

using CeContracts.data;
using CeWeighting.data;


namespace CeWeighting
{
    internal static class GraphGenerator
    {
        public static Graph Create_Graph(IEnumerable<IndexTupel> relations)
        {
            var graph = new Graph();

            foreach (var relation in relations)
            {
                var moreWeight = Get_Or_Create_Node(graph, relation.MoreWeight);
                var lessWeight = Get_Or_Create_Node(graph, relation.LessWeight);

                moreWeight.Successors.Add(lessWeight);
            }

            return graph;
        }



        private static Node Get_Or_Create_Node(Graph graph, int index)
        {
            var node = graph.Nodes.FirstOrDefault(k => k.Index == index);

            if (node == null)
            {
                node = new Node { Index = index };
                graph.Nodes.Add(node);
            }

            return node;
        }

        
        /// <summary>
        /// Sortierung des Graphen anhand
        /// https://de.wikipedia.org/wiki/Topologische_Sortierung
        /// </summary>
        public static void Sort(Graph graph, Action<TotalWeighting> ok, Action exception)
        {
            var dict = Calculate_Number_Of_Predecessors(graph);
            Sort_Topologically(graph, dict,
                ok,
                exception);
        }

        internal static Dictionary<int, int> Calculate_Number_Of_Predecessors(Graph graph) {
            if (graph?.Nodes == null) return new Dictionary<int, int>();

            return graph.Nodes.Select(k => new {
                    Index = k.Index,
                    NumberOfPredecessors = graph.Nodes.Count(v => v.Successors.Select(m => m.Index)
                                                      .Contains(k.Index))
                })
                .ToDictionary(k => k.Index, v => v.NumberOfPredecessors);
        }

        
        internal static void Sort_Topologically(Graph graph, Dictionary<int, int> predecessors, 
                                                Action<TotalWeighting> ok, Action exception)
        {
            var storyIndices = new List<int>();

            while (predecessors.Any()) {
                // finde elemente ohne Vorgänger
                var nodesWithoutPredecessors = predecessors.Where(v => v.Value == 0).ToList();
                if (!nodesWithoutPredecessors.Any()) {
                    exception();
                    return;
                }

                foreach (var pair in nodesWithoutPredecessors) {
                    storyIndices.Add(pair.Key);
                    foreach (var node in graph.Nodes.First(k => k.Index == pair.Key).Successors) {
                        predecessors[node.Index]--;
                    }
                    predecessors.Remove(pair.Key);
                }
            }

            ok(new TotalWeighting() { StoryIndizes = storyIndices });
        }
    }
}
