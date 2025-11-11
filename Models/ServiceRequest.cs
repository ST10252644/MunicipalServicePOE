using System;
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    /// <summary>
    /// Represents a service request with tracking information
    /// Part 3: Advanced data structure model for BST/AVL Tree storage
    /// </summary>
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public string RequestId { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime SubmittedDate { get; set; }
        public RequestStatus Status { get; set; }
        public int Priority { get; set; } // 1 = High, 2 = Medium, 3 = Low
        public string AttachmentPath { get; set; }
        public List<string> StatusHistory { get; set; }
        public DateTime? EstimatedCompletion { get; set; }
        public List<string> DependsOn { get; set; } // For graph relationships
        public string AssignedDepartment { get; set; }

        public ServiceRequest()
        {
            StatusHistory = new List<string>();
            DependsOn = new List<string>();
            SubmittedDate = DateTime.Now;
            Status = RequestStatus.Pending;
        }

        /// <summary>
        /// Compare requests by RequestId for BST ordering
        /// </summary>
        public int CompareTo(ServiceRequest other)
        {
            if (other == null) return 1;
            return string.Compare(this.RequestId, other.RequestId, StringComparison.Ordinal);
        }

        /// <summary>
        /// Get color based on status for UI display
        /// </summary>
        public System.Drawing.Color GetStatusColor()
        {
            switch (Status)
            {
                case RequestStatus.Pending:
                    return System.Drawing.Color.FromArgb(255, 193, 7); // Amber
                case RequestStatus.InProgress:
                    return System.Drawing.Color.FromArgb(33, 150, 243); // Blue
                case RequestStatus.Completed:
                    return System.Drawing.Color.FromArgb(76, 175, 80); // Green
                case RequestStatus.Rejected:
                    return System.Drawing.Color.FromArgb(244, 67, 54); // Red
                default:
                    return System.Drawing.Color.Gray;
            }
        }

        /// <summary>
        /// Get priority label for display
        /// </summary>
        public string GetPriorityLabel()
        {
            switch (Priority)
            {
                case 1: return "HIGH";
                case 2: return "MEDIUM";
                case 3: return "LOW";
                default: return "UNKNOWN";
            }
        }

        /// <summary>
        /// Add status update to history
        /// </summary>
        public void AddStatusUpdate(string update)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            StatusHistory.Add($"[{timestamp}] {update}");
        }

        public override string ToString()
        {
            return $"{RequestId} - {Category} ({Status})";
        }
    }

    /// <summary>
    /// Enum for service request status
    /// </summary>
    public enum RequestStatus
    {
        Pending,
        InProgress,
        Completed,
        Rejected
    }
}