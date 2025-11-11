# Municipal Services Application - Part 3

**Student Name:** Cherika Bodde  
**Student Number:** st10252644  
**GitHub Repository:** [https://github.com/ST10252644/MunicipalAppService.git](https://github.com/ST10252644/MunicipalServicePOE.git)
**Video Demonstration:** 

---

## Table of Contents
1. Project Overview
2. System Requirements
3. How to Compile and Run
4. How to Use the Application
5. Data Structures Implementation
6. Recommendation Feature
7. Features Implemented
8. Part 3 - Service Request Status Implementation
9. Project Structure
10. Troubleshooting
11. Academic Integrity Statement
12. References
13. Contact Information

---

## Project Overview
This is a C# .NET Framework Windows Forms application designed to streamline municipal services in South Africa. The application enables residents to:

- Report issues and request services (Part 1)
- Access information about local events and announcements (Part 2)
- Track service request status with advanced data structures (Part 3)

**Complete Application:** All three parts fully implemented with advanced data structures and intelligent features.

---

## System Requirements
- **Operating System:** Windows 10 or later
- **Framework:** .NET Framework 4.7.2 or higher
- **IDE:** Visual Studio 2019 or later
- **NuGet Packages:** FontAwesome.Sharp (for icons)

---

## How to Compile and Run

### Method 1: Using Visual Studio
1. **Open the Solution:**
   - Launch Visual Studio
   - Open `MunicipalServicesApp.sln`
2. **Restore NuGet Packages:**
   - Right-click on the solution in Solution Explorer
   - Select "Restore NuGet Packages"
3. **Build the Project:**
   - Press `Ctrl + Shift + B` or select `Build > Build Solution`
4. **Run the Application:**
   - Press `F5` or click the "Start" button (green arrow)

### Method 2: Using Command Line
```bash
cd path/to/MunicipalServicesApp
nuget restore
msbuild MunicipalServicesApp.sln /p:Configuration=Release
cd bin/Release
MunicipalServicesApp.exe
```

---

## How to Use the Application

### 1. Startup Screen
- Launch the application
- Click "Get Started" to access the main menu

### 2. Main Menu (Form1)
- **Report Issues** - Submit community problems (Part 1)
- **Local Events and Announcements** - Browse upcoming events (Part 2)
- **Service Request Status** - Track your requests (Part 3)

### 3. Report Issues (Part 1)
- Fill in location, category, and description
- Attach media files if needed
- Click "Submit" to report the issue
- Use "Back to Main Menu" to return

### 4. Local Events and Announcements (Part 2)
**Viewing Events:**
- Events displayed in a table sorted by priority and date
- **Priority Levels:**
  - HIGH (Red) - Urgent
  - MEDIUM (Yellow) - Important
  - LOW (Green) - General

**Searching Events:**
- By category, date, keyword, or combined filters
- Use "Clear Filters" to reset

**Viewing Event Details:**
- Double-click any event row for full details

**Viewing Recommendations:**
- Click "Recommended" button
- Personalized based on search history, viewed events, and high-priority events

**Recent Searches Panel:**
- Shows last 10 searches with timestamps
- Updates automatically

### 5. Service Request Status (Part 3)
**Viewing Service Requests:**
- All requests displayed in a sortable data grid
- Color-coded by priority:
  - RED - High Priority
  - ORANGE - Medium Priority
  - GREEN - Low Priority
- Shows Request ID, Category, Status, Priority, Date, Department, and Location

**Searching Requests:**
- **By Request ID:** Enter exact ID in search box (uses BST for O(log n) search)
- **By Status:** Filter by Pending, InProgress, Completed, or Rejected
- **By Priority:** Filter by HIGH, MEDIUM, or LOW
- **Combined Filters:** Use multiple filters simultaneously

**Viewing Request Details:**
- Click any request row to view full details in the right panel
- Shows:
  - Complete location information
  - Category and priority
  - Current status
  - Assigned department
  - Submission date and time
  - Full description
  - Complete status history with timestamps

**Understanding Dependencies:**
- Dependency tree shows relationships between requests
- **"Depends On"** - Requests that must be completed first (shown in red)
- **"Required By"** - Requests waiting for this one (shown in blue)
- Status icons indicate current state:
  - ⏳ Pending
  - ⚙️ In Progress
  - ✅ Completed
  - ❌ Rejected

**Statistics Dashboard:**
- Real-time statistics showing:
  - Total number of requests
  - Pending requests count
  - In Progress requests count
  - Completed requests count
- Each statistic displayed in a color-coded card with icon

**Refreshing Data:**
- Click the refresh button to update all data
- Shows BST performance metrics:
  - Total requests in system
  - Tree height
  - Balance status

**Navigation:**
- Use "Back" button to return to main menu
- All filters and searches persist until cleared

---

## Data Structures Implementation

### Part 2 Data Structures

#### 1. Stack (Recently Viewed Events)
```csharp
private Stack<EventInfo> recentlyViewedStack = new Stack<EventInfo>();

// On event view
recentlyViewedStack.Push(ev);
```

#### 2. Queue (Search History)
```csharp
private Queue<string> searchHistoryQueue = new Queue<string>();

searchHistoryQueue.Enqueue($"{DateTime.Now:HH:mm} - '{keyword}'");
if (searchHistoryQueue.Count > 10) searchHistoryQueue.Dequeue();
```

#### 3. Dictionaries and Sorted Dictionaries
```csharp
private SortedDictionary<DateTime, List<EventInfo>> eventsByDate = new SortedDictionary<DateTime, List<EventInfo>>();
private Dictionary<string, List<EventInfo>> categoryLookup = new Dictionary<string, List<EventInfo>>(StringComparer.OrdinalIgnoreCase);
```

#### 4. Sets (Unique Categories and Dates)
```csharp
private HashSet<string> uniqueCategories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
private HashSet<DateTime> eventDates = new HashSet<DateTime>();
```

#### 5. Priority Queue (SortedSet for Priority)
```csharp
private SortedSet<EventInfo> priorityEvents = new SortedSet<EventInfo>();

public class EventInfo : IComparable<EventInfo>
{
    public string Title { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; } // 1=HIGH, 2=MEDIUM, 3=LOW

    public int CompareTo(EventInfo other)
    {
        int priorityComparison = this.Priority.CompareTo(other.Priority);
        return priorityComparison != 0 ? priorityComparison : this.Date.CompareTo(other.Date);
    }
}
```

### Part 3 Data Structures

#### 1. Binary Search Tree (BST)
**Purpose:** Efficient O(log n) search, insert, and retrieval of service requests by Request ID.

**Implementation:**
```csharp
public class ServiceRequestBST
{
    private BSTNode root;
    private int nodeCount;

    // Insert operation - O(log n) average case
    public void Insert(ServiceRequest request)
    {
        root = InsertRecursive(root, request);
        nodeCount++;
    }

    // Search operation - O(log n) average case
    public ServiceRequest Search(string requestId)
    {
        return SearchRecursive(root, requestId);
    }

    // In-order traversal - returns sorted list
    public List<ServiceRequest> InOrderTraversal()
    {
        List<ServiceRequest> result = new List<ServiceRequest>();
        InOrderRecursive(root, result);
        return result;
    }
}

public class BSTNode
{
    public ServiceRequest Data { get; set; }
    public BSTNode Left { get; set; }
    public BSTNode Right { get; set; }
}
```

**Key Features:**
- Sorted storage by Request ID
- Fast lookups for tracking specific requests
- In-order traversal provides sorted display
- Height tracking for performance monitoring
- Balance checking for optimization insights

**Time Complexity:**
- Insert: O(log n) average, O(n) worst case
- Search: O(log n) average, O(n) worst case
- Traversal: O(n)

#### 2. Graph (Directed Graph for Dependencies)
**Purpose:** Model and manage dependencies between service requests.

**Implementation:**
```csharp
public class ServiceRequestGraph
{
    // Adjacency list representation
    private Dictionary<string, List<string>> adjacencyList;
    private Dictionary<string, ServiceRequest> requests;

    // Add dependency edge
    public void AddDependency(string fromRequestId, string toRequestId)
    {
        if (!adjacencyList.ContainsKey(fromRequestId))
            adjacencyList[fromRequestId] = new List<string>();
        
        adjacencyList[fromRequestId].Add(toRequestId);
    }

    // Get all dependencies for a request
    public List<ServiceRequest> GetDependencies(string requestId)
    {
        List<ServiceRequest> dependencies = new List<ServiceRequest>();
        if (adjacencyList.ContainsKey(requestId))
        {
            foreach (string depId in adjacencyList[requestId])
                dependencies.Add(requests[depId]);
        }
        return dependencies;
    }

    // Get requests that depend on this one
    public List<ServiceRequest> GetDependents(string requestId)
    {
        List<ServiceRequest> dependents = new List<ServiceRequest>();
        foreach (var kvp in adjacencyList)
        {
            if (kvp.Value.Contains(requestId))
                dependents.Add(requests[kvp.Key]);
        }
        return dependents;
    }

    // Detect circular dependencies using DFS
    public bool HasCircularDependency()
    {
        HashSet<string> visited = new HashSet<string>();
        HashSet<string> recursionStack = new HashSet<string>();
        
        foreach (string requestId in adjacencyList.Keys)
        {
            if (HasCycleDFS(requestId, visited, recursionStack))
                return true;
        }
        return false;
    }

    // Get topological sort (processing order)
    public List<ServiceRequest> GetProcessingOrder()
    {
        if (HasCircularDependency())
            return null;
        
        // Topological sort implementation
        // Returns ordered list of requests
    }
}
```

**Key Features:**
- Directed edges represent "depends on" relationships
- Cycle detection prevents invalid dependencies
- Topological sorting determines optimal processing order
- Bidirectional dependency queries (forward and backward)
- Depth calculation for dependency chains

**Algorithms Used:**
- Depth-First Search (DFS) for cycle detection
- Topological Sort for processing order
- Graph traversal for dependency visualization

**Use Cases:**
- REQ-002 (pothole repair) depends on REQ-001 (water pipe fix)
- REQ-005 (traffic light) depends on REQ-001 (water pipe fix)
- Ensures logical processing order
- Prevents completing dependent requests before prerequisites

#### 3. Min-Heap (Priority Queue)
**Purpose:** Efficiently manage and retrieve highest priority service requests.

**Implementation:**
```csharp
public class ServiceRequestMinHeap
{
    private List<ServiceRequest> heap;

    // Insert with O(log n) complexity
    public void Insert(ServiceRequest request)
    {
        heap.Add(request);
        HeapifyUp(heap.Count - 1);
    }

    // Extract minimum (highest priority) - O(log n)
    public ServiceRequest ExtractMin()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Heap is empty");
        
        ServiceRequest min = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        
        if (heap.Count > 0)
            HeapifyDown(0);
        
        return min;
    }

    // Peek at highest priority - O(1)
    public ServiceRequest Peek()
    {
        return heap[0];
    }

    // Compare requests by priority and date
    private int Compare(ServiceRequest a, ServiceRequest b)
    {
        if (a.Priority != b.Priority)
            return a.Priority.CompareTo(b.Priority);
        
        // Same priority: earlier date = higher priority
        return a.SubmittedDate.CompareTo(b.SubmittedDate);
    }

    // Maintain heap property upward
    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (Compare(heap[index], heap[parentIndex]) >= 0)
                break;
            
            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    // Maintain heap property downward
    private void HeapifyDown(int index)
    {
        while (true)
        {
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            int smallest = index;

            if (leftChild < heap.Count && Compare(heap[leftChild], heap[smallest]) < 0)
                smallest = leftChild;
            
            if (rightChild < heap.Count && Compare(heap[rightChild], heap[smallest]) < 0)
                smallest = rightChild;
            
            if (smallest == index)
                break;
            
            Swap(index, smallest);
            index = smallest;
        }
    }
}
```

**Key Features:**
- Min-heap structure (lower priority number = higher urgency)
- Constant-time access to highest priority request
- Logarithmic insertion and extraction
- Dynamic priority updates
- Maintains heap property automatically

**Priority System:**
- 1 = HIGH (e.g., water pipe burst, broken traffic lights)
- 2 = MEDIUM (e.g., pothole repair, missed garbage collection)
- 3 = LOW (e.g., street light out, playground maintenance)

**Secondary Sorting:**
- When priorities are equal, earlier submission date = higher priority
- Ensures fair processing order within priority levels

**Time Complexity:**
- Insert: O(log n)
- Extract Min: O(log n)
- Peek: O(1)
- Build Heap: O(n)

#### 4. ServiceRequest Model
**Purpose:** Core data model with IComparable implementation for BST ordering.

```csharp
public class ServiceRequest : IComparable<ServiceRequest>
{
    public string RequestId { get; set; }
    public string Location { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public DateTime SubmittedDate { get; set; }
    public RequestStatus Status { get; set; }
    public int Priority { get; set; }
    public string AssignedDepartment { get; set; }
    public List<string> StatusHistory { get; set; }
    public List<string> DependsOn { get; set; }

    // Compare by RequestId for BST ordering
    public int CompareTo(ServiceRequest other)
    {
        return string.Compare(this.RequestId, other.RequestId, StringComparison.Ordinal);
    }

    // Add status update with timestamp
    public void AddStatusUpdate(string update)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        StatusHistory.Add($"[{timestamp}] {update}");
    }
}

public enum RequestStatus
{
    Pending,
    InProgress,
    Completed,
    Rejected
}
```

### Data Structure Comparison and Performance

| Data Structure | Purpose | Time Complexity | Space Complexity |
|---------------|---------|-----------------|------------------|
| **BST** | Request lookup by ID | Search: O(log n)<br>Insert: O(log n)<br>Traversal: O(n) | O(n) |
| **Graph** | Dependency tracking | Add Edge: O(1)<br>Get Dependencies: O(V+E)<br>Cycle Detection: O(V+E) | O(V+E) |
| **Min-Heap** | Priority management | Insert: O(log n)<br>Extract: O(log n)<br>Peek: O(1) | O(n) |
| **Stack** | Recent views | Push/Pop: O(1) | O(n) |
| **Queue** | Search history | Enqueue/Dequeue: O(1) | O(n) |
| **Dictionary** | Category lookup | Insert/Search: O(1) | O(n) |
| **SortedDictionary** | Date-ordered events | Insert/Search: O(log n) | O(n) |
| **HashSet** | Unique tracking | Insert/Search: O(1) | O(n) |
| **SortedSet** | Priority sorting | Insert/Search: O(log n) | O(n) |

---

## Recommendation Feature (Part 2)
```csharp
private List<EventInfo> GetRecommendedEvents()
{
    var recommendations = new List<EventInfo>();
    
    // High priority events
    recommendations.AddRange(priorityEvents
        .Where(e => e.Priority == 1 && e.Date >= DateTime.Today)
        .Take(3));

    // User's most accessed category
    var mostAccessedCategory = categoryAccessCount
        .OrderByDescending(kv => kv.Value)
        .FirstOrDefault().Key;
    if (categoryLookup.ContainsKey(mostAccessedCategory))
        recommendations.AddRange(categoryLookup[mostAccessedCategory]
            .Where(e => !recommendations.Contains(e))
            .Take(3));

    // Recently viewed category
    if (recentlyViewedStack.Any())
    {
        var recentCategory = recentlyViewedStack.Peek().Category;
        recommendations.AddRange(categoryLookup[recentCategory]
            .Where(e => !recommendations.Contains(e))
            .Take(2));
    }

    // Fill with upcoming events
    recommendations.AddRange(eventsByDate
        .SelectMany(kv => kv.Value)
        .Where(e => !recommendations.Contains(e))
        .Take(8 - recommendations.Count));
    
    return recommendations;
}
```

---

## Features Implemented

### Part 1: Report Issues
✅ Startup screen with modern UI  
✅ Main menu navigation  
✅ Report Issues form with:
- Location input
- Category selection (dropdown)
- Description text area
- Media attachment capability
- Progress bar for submission
- Input validation
- Success confirmation

### Part 2: Local Events and Announcements
✅ Events display table with color-coded priorities  
✅ Advanced filtering system:
- By category
- By date
- By keyword
- Combined filters

✅ Search history tracking (Queue implementation)  
✅ Recently viewed events (Stack implementation)  
✅ Event details modal  
✅ Intelligent recommendation engine  
✅ Data structure implementations:
- Stack for recent views
- Queue for search history
- Dictionary for category lookup
- SortedDictionary for date-ordered events
- HashSet for unique tracking
- SortedSet for priority sorting

✅ Modern responsive UI with hover effects

### Part 3: Service Request Status
✅ **Advanced Data Structure Integration:**
- Binary Search Tree for efficient request lookup
- Directed Graph for dependency management
- Min-Heap for priority queue operations

✅ **Service Request Display:**
- Comprehensive data grid with 7 columns
- Color-coded priority system (High/Medium/Low)
- Alternating row colors for readability
- Modern header styling
- Sortable columns
- Real-time data updates

✅ **Search and Filter System:**
- BST-powered Request ID search (O(log n) efficiency)
- Status filtering (Pending/InProgress/Completed/Rejected)
- Priority filtering (HIGH/MEDIUM/LOW)
- Combined multi-filter capability
- Clear filters functionality

✅ **Request Details Panel:**
- Complete request information display
- Status history with timestamps
- Department assignment info
- Formatted descriptions
- Color-coded status indicators
- Rich text formatting

✅ **Dependency Visualization:**
- Interactive tree view
- Forward dependencies ("Depends On")
- Backward dependencies ("Required By")
- Status icons for quick reference
- Expandable/collapsible nodes
- Color-coded relationship types

✅ **Statistics Dashboard:**
- Real-time metrics cards
- Total requests counter
- Status breakdowns:
  - Pending count
  - In Progress count
  - Completed count
- Icon-enhanced cards
- Color-coded categories

✅ **Performance Features:**
- BST height monitoring
- Tree balance checking
- Node count tracking
- Efficient traversal algorithms
- Optimized search operations

✅ **Sample Data:**
- 12 diverse service requests
- Multiple categories:
  - Water & Sanitation
  - Roads & Transport
  - Electricity
  - Waste Management
  - Public Safety
  - Parks & Recreation
- Varied statuses and priorities
- Realistic descriptions
- Complete status histories
- Interdependent requests for graph demonstration

✅ **UI/UX Enhancements:**
- Modern flat design
- Rounded corners
- Professional color scheme
- Smooth transitions
- Responsive layout
- Emoji icons for visual appeal
- Custom panel borders
- Hover effects on buttons
- Clear visual hierarchy

✅ **Graph Algorithms:**
- Cycle detection (DFS-based)
- Topological sorting
- Dependency depth calculation
- Bidirectional relationship queries
- Processing order determination

✅ **Heap Operations:**
- Dynamic priority updates
- Efficient min extraction
- Heap property validation
- Batch heap building
- Priority-based sorting with date tiebreaker

✅ **BST Operations:**
- Recursive insertion
- Efficient search
- In-order traversal
- Height calculation
- Balance verification
- Node deletion
- Category/status filtering

---

## Project Structure
```
MunicipalServicesApp/
│
├── Forms/
│   ├── StartupForm.cs                    // Application entry point
│   ├── Form1.cs                          // Main menu hub
│   ├── ReportIssuesForm.cs               // Part 1: Issue reporting
│   ├── LocalEventsForm.cs                // Part 2: Events display
│   ├── RecommendedEventsForm.cs          // Part 2: Recommendations
│   └── ServiceRequestStatusForm.cs       // Part 3: Request tracking
│
├── Models/
│   ├── EventInfo.cs                      // Event data model
│   └── ServiceRequest.cs                 // Request data model
│
├── DataStructures/
│   ├── ServiceRequestBST.cs              // Binary Search Tree
│   ├── ServiceRequestGraph.cs            // Directed Graph
│   ├── ServiceRequestMinHeap.cs          // Min-Heap Priority Queue
│   └── BSTNode.cs                        // BST Node structure
│
├── img/
│   └── (Application icons and images)
│
├── MunicipalServicesApp.csproj           // Project configuration
├── App.config                            // Application settings
└── README.md                             // This file
```

---

## Troubleshooting

### Common Issues and Solutions

**Application won't start:**
- Ensure .NET Framework 4.7.2 or higher is installed
- Check for missing dependencies
- Rebuild the solution (Ctrl + Shift + B)

**Missing icons or UI elements:**
- Restore NuGet packages (FontAwesome.Sharp)
- Clean and rebuild solution
- Check package references in project file

**Events not displaying (Part 2):**
- Verify sample data is loaded in LocalEventsForm
- Check InitializeSampleData() method execution
- Ensure event list is populated

**Recommendations not updating:**
- Interact with events (view, search, filter)
- Build search history by performing searches
- View events to populate recently viewed stack

**Service requests not showing (Part 3):**
- Verify LoadSampleData() is called in constructor
- Check BST, Graph, and Heap initialization
- Ensure data structures are properly populated

**Filters not working:**
- Check enum naming matches filter values
- Verify case-sensitive comparisons
- Ensure filter combinations are compatible

**Dependencies not displaying:**
- Check graph initialization
- Verify AddDependency() calls
- Ensure request IDs match exactly

**Performance issues:**
- Check BST balance status (use Refresh button)
- Monitor tree height (should be O(log n))
- Consider rebalancing for large datasets

**Search returning no results:**
- Verify exact Request ID format (e.g., "REQ-001")
- Check BST contains the request
- Use case-sensitive search

### Debug Mode
To enable detailed debugging:
1. Set breakpoints in data structure operations
2. Watch BST tree structure
3. Monitor heap property maintenance
4. Track graph traversal paths
5. Inspect dictionary lookups

### Performance Monitoring
The application includes built-in performance metrics:
- BST Height display (on Refresh)
- Tree balance status
- Node count tracking
- Operation time complexity indicators

---

## Academic Integrity Statement
This project was completed individually by **Cherika Bodde (st10252644)** for PROG7312. The following third-party resources were used under their respective licenses:

- **FontAwesome.Sharp** - Icon library (MIT License)
- **Microsoft .NET Framework** - Application framework
- **C# Language** - Programming language

---

## References

### Books and Academic Papers
- Cormen, T.H., Leiserson, C.E., Rivest, R.L. and Stein, C., 2022. *Introduction to Algorithms*. 4th ed. Cambridge: MIT Press. ISBN: 978-0262046305.
  - Referenced for: BST algorithms, graph traversal (DFS), topological sorting, heap operations
- Jamro, M., 2024. *C# Data Structures and Algorithms - Second Edition*. Birmingham: Packt Publishing. ISBN: 978-1803248271.
  - Referenced for: C# implementation patterns for trees, graphs, and heaps
- Ricci, F., Rokach, L. and Shapira, B., 2022. *Recommender Systems Handbook*. 3rd ed. New York: Springer. DOI: 10.1007/978-1-0716-2197-4
  - Referenced for: Part 2 recommendation algorithms
- Sedgewick, R. and Wayne, K., 2011. *Algorithms*. 4th ed. Boston: Addison-Wesley. ISBN: 978-0321573513.
  - Referenced for: BST operations, graph algorithms, heap implementation
- Weiss, M.A., 2012. *Data Structures and Algorithm Analysis in C++*. 4th ed. Boston: Pearson. ISBN: 978-0132847377.
  - Referenced for: Tree balancing concepts, heap property maintenance

### Online Documentation
- C# Corner, 2023. *Understanding Data Structures in C#*. Available at: https://www.c-sharpcorner.com/article/data-structures-in-c-sharp/ [Accessed 10 October 2025].
  - Referenced for: General C# data structure patterns
- GeeksforGeeks, 2024. *Binary Search Tree Data Structure*. Available at: https://www.geeksforgeeks.org/binary-search-tree-data-structure/ [Accessed 15 October 2025].
  - Referenced for: BST insertion, deletion, and search algorithms
- GeeksforGeeks, 2024. *Binary Search Tree | Set 1 (Search and Insertion)*. Available at: https://www.geeksforgeeks.org/binary-search-tree-set-1-search-and-insertion/ [Accessed 15 October 2025].
  - Referenced for: Recursive BST operations
- GeeksforGeeks, 2024. *Binary Search Tree | Set 2 (Delete)*. Available at: https://www.geeksforgeeks.org/binary-search-tree-set-2-delete/ [Accessed 15 October 2025].
  - Referenced for: BST deletion with three cases (no children, one child, two children)
- GeeksforGeeks, 2024. *Graph Data Structure and Algorithms*. Available at: https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/ [Accessed 15 October 2025].
  - Referenced for: Graph representation using adjacency list
- GeeksforGeeks, 2024. *Detect Cycle in a Directed Graph*. Available at: https://www.geeksforgeeks.org/detect-cycle-in-a-graph/ [Accessed 16 October 2025].
  - Referenced for: DFS-based cycle detection algorithm
- GeeksforGeeks, 2024. *Topological Sorting*. Available at: https://www.geeksforgeeks.org/topological-sorting/ [Accessed 16 October 2025].
  - Referenced for: Topological sort using DFS for dependency ordering
- GeeksforGeeks, 2024. *Heap Data Structure*. Available at: https://www.geeksforgeeks.org/heap-data-structure/ [Accessed 15 October 2025].
  - Referenced for: Min-heap structure and operations
- GeeksforGeeks, 2024. *Min Heap in C#*. Available at: https://www.geeksforgeeks.org/min-heap-in-csharp/ [Accessed 16 October 2025].
  - Referenced for: C# min-heap implementation with heapify operations
- GeeksforGeeks, 2024. *Insertion and Deletion in Heaps*. Available at: https://www.geeksforgeeks.org/insertion-and-deletion-in-heaps/ [Accessed 16 October 2025].
  - Referenced for: HeapifyUp and HeapifyDown algorithms
- Microsoft, 2024. *Dictionary<TKey,TValue> Class*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 [Accessed 13 October 2025].
  - Referenced for: Efficient key-value storage in graph adjacency list
- Microsoft, 2024. *List<T> Class*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 [Accessed 16 October 2025].
  - Referenced for: Dynamic array implementation for heap and BST traversal
- Microsoft, 2024. *HashSet<T> Class*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 [Accessed 16 October 2025].
  - Referenced for: Visited set in graph cycle detection and DFS
- Microsoft, 2024. *Queue<T> Class*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1 [Accessed 13 October 2025].
  - Referenced for: Search history tracking (Part 2)
- Microsoft, 2024. *Stack<T> Class*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1 [Accessed 13 October 2025].
  - Referenced for: DFS recursion stack and topological sort
- Microsoft, 2024. *IComparable<T> Interface*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.icomparable-1 [Accessed 16 October 2025].
  - Referenced for: ServiceRequest comparison for BST ordering
- Microsoft, 2024. *Windows Forms Overview*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/overview/ [Accessed 12 October 2025].
  - Referenced for: UI implementation and DataGridView usage
- Microsoft, 2024. *TreeView Class*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.treeview [Accessed 16 October 2025].
  - Referenced for: Dependency tree visualization
- Microsoft, 2024. *RichTextBox Class*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.richtextbox [Accessed 16 October 2025].
  - Referenced for: Request details formatting

### Government and Municipal Resources
- Department of Cooperative Governance and Traditional Affairs, 2023. *Improving Municipal Service Delivery*. Republic of South Africa. Available at: https://www.cogta.gov.za/ [Accessed 8 October 2025].

### Design and UI Resources
- FontAwesome, 2024. *FontAwesome.Sharp Documentation*. Available at: https://github.com/awesome-inc/FontAwesome.Sharp [Accessed 13 October 2025].
- Material Design, 2024. *Design Guidelines*. Google. Available at: https://material.io/design [Accessed 12 October 2025].

### Technical Resources
- DotNetPerls, 2024. *C# SortedSet Examples*. Available at: https://www.dotnetperls.com/sortedset [Accessed 11 October 2025].
  - Referenced for: Part 2 priority event sorting
- DotNetPerls, 2024. *C# Tree Examples*. Available at: https://www.dotnetperls.com/tree [Accessed 16 October 2025].
  - Referenced for: Tree node structure patterns
- Programiz, 2024. *Binary Search Tree (BST)*. Available at: https://www.programiz.com/dsa/binary-search-tree [Accessed 16 October 2025].
  - Referenced for: BST visualization and algorithm understanding
- Programiz, 2024. *Heap Data Structure*. Available at: https://www.programiz.com/dsa/heap-data-structure [Accessed 16 October 2025].
  - Referenced for: Heap property maintenance concepts
- Programiz, 2024. *Graph Data Structure*. Available at: https://www.programiz.com/dsa/graph [Accessed 16 October 2025].
  - Referenced for: Graph representation and traversal concepts
- Stack Overflow, 2024. *When to use Stack vs Queue in C#?*. Available at: https://stackoverflow.com/questions/3825050/when-to-use-stack-vs-queue [Accessed 11 October 2025].
  - Referenced for: Part 2 data structure selection
- Stack Overflow, 2024. *Binary Search Tree Implementation in C#*. Available at: https://stackoverflow.com/questions/tagged/binary-search-tree+c%23 [Accessed 15 October 2025].
  - Referenced for: C# BST implementation patterns
- Stack Overflow, 2024. *Implementing a Min Heap in C#*. Available at: https://stackoverflow.com/questions/19720438/c-sharp-implementing-a-min-heap [Accessed 16 October 2025].
  - Referenced for: Min-heap generic implementation approach
- Stack Overflow, 2024. *Detect cycle in directed graph algorithm*. Available at: https://stackoverflow.com/questions/261573/best-algorithm-for-detecting-cycles-in-a-directed-graph [Accessed 16 October 2025].
  - Referenced for: DFS cycle detection optimization
- Stack Overflow, 2024. *Topological sort using DFS*. Available at: https://stackoverflow.com/questions/2739392/topological-sort-using-dfs [Accessed 16 October 2025].
  - Referenced for: Topological sorting implementation details
- Tutorialspoint, 2024. *C# - Collections*. Available at: https://www.tutorialspoint.com/csharp/csharp_collections.htm [Accessed 9 October 2025].
  - Referenced for: C# collection framework understanding
- Tutorialspoint, 2024. *Data Structures and Algorithms - Binary Search Tree*. Available at: https://www.tutorialspoint.com/data_structures_algorithms/binary_search_tree.htm [Accessed 16 October 2025].
  - Referenced for: BST theoretical foundations
- Tutorialspoint, 2024. *Data Structures and Algorithms - Graph Data Structure*. Available at: https://www.tutorialspoint.com/data_structures_algorithms/graph_data_structure.htm [Accessed 16 October 2025].
  - Referenced for: Graph adjacency list representation
- Tutorialspoint, 2024. *Data Structures and Algorithms - Heap*. Available at: https://www.tutorialspoint.com/data_structures_algorithms/heap_data_structure.htm [Accessed 16 October 2025].
  - Referenced for: Heap array representation
- Towards Data Science, 2023. *Building a Simple Recommendation System*. Medium. Available at: https://towardsdatascience.com/building-a-recommendation-system-e6cb0c1e5673 [Accessed 9 October 2025].
  - Referenced for: Part 2 recommendation algorithm design

### Algorithm Visualization
- VisuAlgo, 2024. *Visualising Data Structures and Algorithms*. Available at: https://visualgo.net/ [Accessed 15 October 2025].
  - Referenced for: Understanding BST rotations, heap operations, and graph traversal visualization
- VisuAlgo, 2024. *Binary Search Tree Visualization*. Available at: https://visualgo.net/en/bst [Accessed 16 October 2025].
  - Referenced for: BST insertion, deletion, and search visualization
- VisuAlgo, 2024. *Heap Visualization*. Available at: https://visualgo.net/en/heap [Accessed 16 October 2025].
  - Referenced for: Min-heap heapify operations visualization
- VisuAlgo, 2024. *Graph Traversal Visualization*. Available at: https://visualgo.net/en/dfsbfs [Accessed 16 October 2025].
  - Referenced for: DFS traversal and cycle detection patterns

### Additional C# and .NET Resources
- C# Station, 2024. *C# Tutorial - Data Structures*. Available at: https://www.csharp-station.com/ [Accessed 16 October 2025].
  - Referenced for: C# best practices in data structure implementation
- Code Project, 2024. *Binary Search Tree in C#*. Available at: https://www.codeproject.com/Articles/1095392/Binary-Search-Tree-BST-Implementation-in-Csharp [Accessed 16 October 2025].
  - Referenced for: C# BST implementation patterns and error handling
- Code Project, 2024. *Graph Algorithms in C#*. Available at: https://www.codeproject.com/Articles/32212/Introduction-to-Graph-with-Breadth-First-Search-BF [Accessed 16 October 2025].
  - Referenced for: Graph data structure design patterns
- Javatpoint, 2024. *Binary Search Tree*. Available at: https://www.javatpoint.com/binary-search-tree [Accessed 16 October 2025].
  - Referenced for: BST properties and characteristics
- Javatpoint, 2024. *Min Heap and Max Heap*. Available at: https://www.javatpoint.com/heap-data-structure [Accessed 16 October 2025].
  - Referenced for: Heap comparison and selection criteria

### FontAwesome and UI Design
- FontAwesome, 2024. *FontAwesome.Sharp Documentation*. Available at: https://github.com/awesome-inc/FontAwesome.Sharp [Accessed 13 October 2025].
  - Referenced for: Icon integration in Windows Forms
- Material Design, 2024. *Design Guidelines*. Google. Available at: https://material.io/design [Accessed 12 October 2025].
  - Referenced for: Modern UI color schemes and layout principles
- Material Design, 2024. *Color System*. Google. Available at: https://material.io/design/color/the-color-system.html [Accessed 16 October 2025].
  - Referenced for: Priority color coding (high/medium/low)

---

---

*This application was developed as part of the PROG7312 coursework demonstrating proficiency in C# programming, Windows Forms development, and advanced data structures implementation.*
