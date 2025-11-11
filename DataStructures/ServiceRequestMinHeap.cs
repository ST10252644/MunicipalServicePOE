using System;
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    /// <summary>
    /// Min-Heap implementation for priority queue of service requests
    /// Part 3: Lower priority number = higher priority (1 = HIGH, 2 = MEDIUM, 3 = LOW)
    /// </summary>
    public class ServiceRequestMinHeap
    {
        private List<ServiceRequest> heap;

        public ServiceRequestMinHeap()
        {
            heap = new List<ServiceRequest>();
        }

        public int Count => heap.Count;

        public bool IsEmpty => heap.Count == 0;

        /// <summary>
        /// Insert a service request into the heap
        /// Time Complexity: O(log n)
        /// </summary>
        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        /// <summary>
        /// Extract the highest priority request (minimum priority number)
        /// Time Complexity: O(log n)
        /// </summary>
        public ServiceRequest ExtractMin()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            ServiceRequest min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
            {
                HeapifyDown(0);
            }

            return min;
        }

        /// <summary>
        /// Peek at the highest priority request without removing it
        /// Time Complexity: O(1)
        /// </summary>
        public ServiceRequest Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            return heap[0];
        }

        /// <summary>
        /// Get all requests in the heap (not in sorted order)
        /// </summary>
        public List<ServiceRequest> GetAllRequests()
        {
            return new List<ServiceRequest>(heap);
        }

        /// <summary>
        /// Get all requests sorted by priority
        /// Time Complexity: O(n log n)
        /// </summary>
        public List<ServiceRequest> GetSortedRequests()
        {
            List<ServiceRequest> sorted = new List<ServiceRequest>();
            ServiceRequestMinHeap tempHeap = new ServiceRequestMinHeap();

            // Copy all elements to temp heap
            foreach (var request in heap)
            {
                tempHeap.Insert(request);
            }

            // Extract all elements in sorted order
            while (!tempHeap.IsEmpty)
            {
                sorted.Add(tempHeap.ExtractMin());
            }

            return sorted;
        }

        /// <summary>
        /// Update priority of a request
        /// </summary>
        public bool UpdatePriority(string requestId, int newPriority)
        {
            for (int i = 0; i < heap.Count; i++)
            {
                if (heap[i].RequestId == requestId)
                {
                    int oldPriority = heap[i].Priority;
                    heap[i].Priority = newPriority;

                    if (newPriority < oldPriority)
                    {
                        HeapifyUp(i);
                    }
                    else if (newPriority > oldPriority)
                    {
                        HeapifyDown(i);
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Remove a specific request from the heap
        /// </summary>
        public bool Remove(string requestId)
        {
            for (int i = 0; i < heap.Count; i++)
            {
                if (heap[i].RequestId == requestId)
                {
                    heap[i] = heap[heap.Count - 1];
                    heap.RemoveAt(heap.Count - 1);

                    if (i < heap.Count)
                    {
                        HeapifyDown(i);
                        HeapifyUp(i);
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Heapify up to maintain heap property
        /// </summary>
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;

                if (Compare(heap[index], heap[parentIndex]) >= 0)
                {
                    break;
                }

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        /// <summary>
        /// Heapify down to maintain heap property
        /// </summary>
        private void HeapifyDown(int index)
        {
            while (true)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int smallest = index;

                if (leftChild < heap.Count && Compare(heap[leftChild], heap[smallest]) < 0)
                {
                    smallest = leftChild;
                }

                if (rightChild < heap.Count && Compare(heap[rightChild], heap[smallest]) < 0)
                {
                    smallest = rightChild;
                }

                if (smallest == index)
                {
                    break;
                }

                Swap(index, smallest);
                index = smallest;
            }
        }

        /// <summary>
        /// Compare two service requests
        /// Priority comparison: Lower number = higher priority
        /// If priorities equal, compare by submission date (earlier = higher priority)
        /// </summary>
        private int Compare(ServiceRequest a, ServiceRequest b)
        {
            if (a.Priority != b.Priority)
            {
                return a.Priority.CompareTo(b.Priority);
            }

            // If same priority, earlier submission date has higher priority
            return a.SubmittedDate.CompareTo(b.SubmittedDate);
        }

        /// <summary>
        /// Swap two elements in the heap
        /// </summary>
        private void Swap(int i, int j)
        {
            ServiceRequest temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        /// <summary>
        /// Check if heap property is maintained (for testing)
        /// </summary>
        public bool IsValidHeap()
        {
            for (int i = 0; i < heap.Count; i++)
            {
                int leftChild = 2 * i + 1;
                int rightChild = 2 * i + 2;

                if (leftChild < heap.Count && Compare(heap[i], heap[leftChild]) > 0)
                {
                    return false;
                }

                if (rightChild < heap.Count && Compare(heap[i], heap[rightChild]) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get statistics about the heap
        /// </summary>
        public Dictionary<string, int> GetStatistics()
        {
            Dictionary<string, int> stats = new Dictionary<string, int>
            {
                { "TotalRequests", heap.Count },
                { "HighPriority", 0 },
                { "MediumPriority", 0 },
                { "LowPriority", 0 }
            };

            foreach (var request in heap)
            {
                switch (request.Priority)
                {
                    case 1:
                        stats["HighPriority"]++;
                        break;
                    case 2:
                        stats["MediumPriority"]++;
                        break;
                    case 3:
                        stats["LowPriority"]++;
                        break;
                }
            }

            return stats;
        }

        /// <summary>
        /// Get requests by status from the heap
        /// </summary>
        public List<ServiceRequest> GetRequestsByStatus(RequestStatus status)
        {
            List<ServiceRequest> filtered = new List<ServiceRequest>();

            foreach (var request in heap)
            {
                if (request.Status == status)
                {
                    filtered.Add(request);
                }
            }

            return filtered;
        }

        /// <summary>
        /// Build heap from an existing list (heapify)
        /// Time Complexity: O(n)
        /// </summary>
        public void BuildHeap(List<ServiceRequest> requests)
        {
            heap.Clear();
            heap.AddRange(requests);

            // Start from last non-leaf node and heapify down
            for (int i = (heap.Count / 2) - 1; i >= 0; i--)
            {
                HeapifyDown(i);
            }
        }

        /// <summary>
        /// Clear all requests from the heap
        /// </summary>
        public void Clear()
        {
            heap.Clear();
        }

        /// <summary>
        /// Check if a request exists in the heap
        /// </summary>
        public bool Contains(string requestId)
        {
            foreach (var request in heap)
            {
                if (request.RequestId == requestId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
