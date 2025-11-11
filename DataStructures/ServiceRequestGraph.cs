using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp
{
    /// <summary>
    /// Graph structure for tracking service request dependencies
    /// Part 3: Implements directed graph for dependency management
    /// </summary>
    public class ServiceRequestGraph
    {
        // Adjacency list: Key = RequestId, Value = List of dependent request IDs
        private Dictionary<string, List<string>> adjacencyList;

        // Store actual request objects for quick access
        private Dictionary<string, ServiceRequest> requests;

        public ServiceRequestGraph()
        {
            adjacencyList = new Dictionary<string, List<string>>();
            requests = new Dictionary<string, ServiceRequest>();
        }

        /// <summary>
        /// Add a service request as a vertex in the graph
        /// </summary>
        public void AddRequest(ServiceRequest request)
        {
            if (!requests.ContainsKey(request.RequestId))
            {
                requests[request.RequestId] = request;
                adjacencyList[request.RequestId] = new List<string>();
            }
        }

        /// <summary>
        /// Add a dependency edge: fromRequest depends on toRequest
        /// Example: Request A cannot be completed until Request B is done
        /// </summary>
        public void AddDependency(string fromRequestId, string toRequestId)
        {
            if (!adjacencyList.ContainsKey(fromRequestId))
            {
                adjacencyList[fromRequestId] = new List<string>();
            }

            if (!adjacencyList[fromRequestId].Contains(toRequestId))
            {
                adjacencyList[fromRequestId].Add(toRequestId);
            }

            // Update the request's DependsOn list
            if (requests.ContainsKey(fromRequestId))
            {
                requests[fromRequestId].DependsOn.Add(toRequestId);
            }
        }

        /// <summary>
        /// Get all dependencies for a specific request
        /// </summary>
        public List<ServiceRequest> GetDependencies(string requestId)
        {
            List<ServiceRequest> dependencies = new List<ServiceRequest>();

            if (adjacencyList.ContainsKey(requestId))
            {
                foreach (string depId in adjacencyList[requestId])
                {
                    if (requests.ContainsKey(depId))
                    {
                        dependencies.Add(requests[depId]);
                    }
                }
            }

            return dependencies;
        }

        /// <summary>
        /// Get all requests that depend on this request (reverse dependencies)
        /// </summary>
        public List<ServiceRequest> GetDependents(string requestId)
        {
            List<ServiceRequest> dependents = new List<ServiceRequest>();

            foreach (var kvp in adjacencyList)
            {
                if (kvp.Value.Contains(requestId))
                {
                    if (requests.ContainsKey(kvp.Key))
                    {
                        dependents.Add(requests[kvp.Key]);
                    }
                }
            }

            return dependents;
        }

        /// <summary>
        /// Check if a request can be processed (all dependencies are completed)
        /// </summary>
        public bool CanProcess(string requestId)
        {
            if (!adjacencyList.ContainsKey(requestId))
            {
                return true; // No dependencies
            }

            foreach (string depId in adjacencyList[requestId])
            {
                if (requests.ContainsKey(depId))
                {
                    if (requests[depId].Status != RequestStatus.Completed)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Get all requests that are ready to be processed (no pending dependencies)
        /// </summary>
        public List<ServiceRequest> GetProcessableRequests()
        {
            List<ServiceRequest> processable = new List<ServiceRequest>();

            foreach (var request in requests.Values)
            {
                if (request.Status == RequestStatus.Pending && CanProcess(request.RequestId))
                {
                    processable.Add(request);
                }
            }

            return processable;
        }

        /// <summary>
        /// Detect circular dependencies using DFS
        /// Returns true if circular dependency exists
        /// </summary>
        public bool HasCircularDependency()
        {
            HashSet<string> visited = new HashSet<string>();
            HashSet<string> recursionStack = new HashSet<string>();

            foreach (string requestId in adjacencyList.Keys)
            {
                if (HasCycleDFS(requestId, visited, recursionStack))
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasCycleDFS(string requestId, HashSet<string> visited, HashSet<string> recursionStack)
        {
            if (recursionStack.Contains(requestId))
            {
                return true; // Circular dependency found
            }

            if (visited.Contains(requestId))
            {
                return false;
            }

            visited.Add(requestId);
            recursionStack.Add(requestId);

            if (adjacencyList.ContainsKey(requestId))
            {
                foreach (string neighbor in adjacencyList[requestId])
                {
                    if (HasCycleDFS(neighbor, visited, recursionStack))
                    {
                        return true;
                    }
                }
            }

            recursionStack.Remove(requestId);
            return false;
        }

        /// <summary>
        /// Get topological sort of requests (processing order)
        /// Returns null if circular dependency exists
        /// </summary>
        public List<ServiceRequest> GetProcessingOrder()
        {
            if (HasCircularDependency())
            {
                return null; // Cannot process if circular dependency exists
            }

            HashSet<string> visited = new HashSet<string>();
            Stack<string> stack = new Stack<string>();

            foreach (string requestId in adjacencyList.Keys)
            {
                if (!visited.Contains(requestId))
                {
                    TopologicalSortDFS(requestId, visited, stack);
                }
            }

            List<ServiceRequest> orderedRequests = new List<ServiceRequest>();
            while (stack.Count > 0)
            {
                string requestId = stack.Pop();
                if (requests.ContainsKey(requestId))
                {
                    orderedRequests.Add(requests[requestId]);
                }
            }

            return orderedRequests;
        }

        private void TopologicalSortDFS(string requestId, HashSet<string> visited, Stack<string> stack)
        {
            visited.Add(requestId);

            if (adjacencyList.ContainsKey(requestId))
            {
                foreach (string neighbor in adjacencyList[requestId])
                {
                    if (!visited.Contains(neighbor))
                    {
                        TopologicalSortDFS(neighbor, visited, stack);
                    }
                }
            }

            stack.Push(requestId);
        }

        /// <summary>
        /// Get total number of dependencies for a request (including indirect)
        /// </summary>
        public int GetDependencyDepth(string requestId)
        {
            HashSet<string> visited = new HashSet<string>();
            return GetDependencyDepthRecursive(requestId, visited);
        }

        private int GetDependencyDepthRecursive(string requestId, HashSet<string> visited)
        {
            if (visited.Contains(requestId) || !adjacencyList.ContainsKey(requestId))
            {
                return 0;
            }

            visited.Add(requestId);
            int maxDepth = 0;

            foreach (string depId in adjacencyList[requestId])
            {
                int depth = GetDependencyDepthRecursive(depId, visited) + 1;
                maxDepth = Math.Max(maxDepth, depth);
            }

            return maxDepth;
        }

        /// <summary>
        /// Get all requests (vertices) in the graph
        /// </summary>
        public List<ServiceRequest> GetAllRequests()
        {
            return requests.Values.ToList();
        }

        /// <summary>
        /// Get the request object by ID
        /// </summary>
        public ServiceRequest GetRequest(string requestId)
        {
            return requests.ContainsKey(requestId) ? requests[requestId] : null;
        }

        /// <summary>
        /// Remove a request and all its dependencies
        /// </summary>
        public void RemoveRequest(string requestId)
        {
            if (requests.ContainsKey(requestId))
            {
                requests.Remove(requestId);
            }

            if (adjacencyList.ContainsKey(requestId))
            {
                adjacencyList.Remove(requestId);
            }

            // Remove this request from all dependency lists
            foreach (var list in adjacencyList.Values)
            {
                list.Remove(requestId);
            }
        }

        /// <summary>
        /// Get graph statistics
        /// </summary>
        public Dictionary<string, int> GetStatistics()
        {
            return new Dictionary<string, int>
            {
                { "TotalRequests", requests.Count },
                { "TotalDependencies", adjacencyList.Values.Sum(list => list.Count) },
                { "RequestsWithDependencies", adjacencyList.Count(kvp => kvp.Value.Count > 0) },
                { "IndependentRequests", requests.Count - adjacencyList.Count(kvp => kvp.Value.Count > 0) }
            };
        }

        /// <summary>
        /// Clear the entire graph
        /// </summary>
        public void Clear()
        {
            adjacencyList.Clear();
            requests.Clear();
        }
    }
}
