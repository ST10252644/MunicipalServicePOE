using System;
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    /// <summary>
    /// Binary Search Tree Node for service requests
    /// Part 3: Implements BST for O(log n) search efficiency
    /// </summary>
    public class BSTNode
    {
        public ServiceRequest Data { get; set; }
        public BSTNode Left { get; set; }
        public BSTNode Right { get; set; }

        public BSTNode(ServiceRequest data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    /// <summary>
    /// Binary Search Tree for service request management
    /// Provides efficient search, insert, and traversal operations
    /// </summary>
    public class ServiceRequestBST
    {
        private BSTNode root;
        private int nodeCount;

        public ServiceRequestBST()
        {
            root = null;
            nodeCount = 0;
        }

        public int Count => nodeCount;

        /// <summary>
        /// Insert a service request into the BST
        /// Time Complexity: O(log n) average, O(n) worst case
        /// </summary>
        public void Insert(ServiceRequest request)
        {
            root = InsertRecursive(root, request);
            nodeCount++;
        }

        private BSTNode InsertRecursive(BSTNode node, ServiceRequest request)
        {
            if (node == null)
            {
                return new BSTNode(request);
            }

            int comparison = request.CompareTo(node.Data);

            if (comparison < 0)
            {
                node.Left = InsertRecursive(node.Left, request);
            }
            else if (comparison > 0)
            {
                node.Right = InsertRecursive(node.Right, request);
            }
            // If equal, we could update or ignore (here we ignore duplicates)

            return node;
        }

        /// <summary>
        /// Search for a service request by ID
        /// Time Complexity: O(log n) average
        /// </summary>
        public ServiceRequest Search(string requestId)
        {
            return SearchRecursive(root, requestId);
        }

        private ServiceRequest SearchRecursive(BSTNode node, string requestId)
        {
            if (node == null)
            {
                return null;
            }

            int comparison = string.Compare(requestId, node.Data.RequestId, StringComparison.Ordinal);

            if (comparison == 0)
            {
                return node.Data;
            }
            else if (comparison < 0)
            {
                return SearchRecursive(node.Left, requestId);
            }
            else
            {
                return SearchRecursive(node.Right, requestId);
            }
        }

        /// <summary>
        /// In-order traversal (sorted order)
        /// </summary>
        public List<ServiceRequest> InOrderTraversal()
        {
            List<ServiceRequest> result = new List<ServiceRequest>();
            InOrderRecursive(root, result);
            return result;
        }

        private void InOrderRecursive(BSTNode node, List<ServiceRequest> result)
        {
            if (node != null)
            {
                InOrderRecursive(node.Left, result);
                result.Add(node.Data);
                InOrderRecursive(node.Right, result);
            }
        }

        /// <summary>
        /// Get all requests by status using in-order traversal
        /// </summary>
        public List<ServiceRequest> GetRequestsByStatus(RequestStatus status)
        {
            List<ServiceRequest> allRequests = InOrderTraversal();
            return allRequests.FindAll(r => r.Status == status);
        }

        /// <summary>
        /// Get all requests by category
        /// </summary>
        public List<ServiceRequest> GetRequestsByCategory(string category)
        {
            List<ServiceRequest> allRequests = InOrderTraversal();
            return allRequests.FindAll(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get all requests by priority
        /// </summary>
        public List<ServiceRequest> GetRequestsByPriority(int priority)
        {
            List<ServiceRequest> allRequests = InOrderTraversal();
            return allRequests.FindAll(r => r.Priority == priority);
        }

        /// <summary>
        /// Get tree height (for performance analysis)
        /// </summary>
        public int GetHeight()
        {
            return GetHeightRecursive(root);
        }

        private int GetHeightRecursive(BSTNode node)
        {
            if (node == null)
            {
                return 0;
            }

            int leftHeight = GetHeightRecursive(node.Left);
            int rightHeight = GetHeightRecursive(node.Right);

            return Math.Max(leftHeight, rightHeight) + 1;
        }

        /// <summary>
        /// Delete a service request by ID
        /// </summary>
        public bool Delete(string requestId)
        {
            int initialCount = nodeCount;
            root = DeleteRecursive(root, requestId);
            return nodeCount < initialCount;
        }

        private BSTNode DeleteRecursive(BSTNode node, string requestId)
        {
            if (node == null)
            {
                return null;
            }

            int comparison = string.Compare(requestId, node.Data.RequestId, StringComparison.Ordinal);

            if (comparison < 0)
            {
                node.Left = DeleteRecursive(node.Left, requestId);
            }
            else if (comparison > 0)
            {
                node.Right = DeleteRecursive(node.Right, requestId);
            }
            else
            {
                // Node found - delete it
                nodeCount--;

                // Case 1: No children
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }

                // Case 2: One child
                if (node.Left == null)
                {
                    return node.Right;
                }
                if (node.Right == null)
                {
                    return node.Left;
                }

                // Case 3: Two children - find in-order successor
                BSTNode successor = FindMin(node.Right);
                node.Data = successor.Data;
                node.Right = DeleteRecursive(node.Right, successor.Data.RequestId);
            }

            return node;
        }

        private BSTNode FindMin(BSTNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        /// <summary>
        /// Check if BST is balanced (for performance monitoring)
        /// </summary>
        public bool IsBalanced()
        {
            return IsBalancedRecursive(root);
        }

        private bool IsBalancedRecursive(BSTNode node)
        {
            if (node == null)
            {
                return true;
            }

            int leftHeight = GetHeightRecursive(node.Left);
            int rightHeight = GetHeightRecursive(node.Right);

            if (Math.Abs(leftHeight - rightHeight) > 1)
            {
                return false;
            }

            return IsBalancedRecursive(node.Left) && IsBalancedRecursive(node.Right);
        }

        /// <summary>
        /// Clear all nodes from the tree
        /// </summary>
        public void Clear()
        {
            root = null;
            nodeCount = 0;
        }
    }
}