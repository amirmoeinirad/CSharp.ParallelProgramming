
// Amir Moeini Rad
// September 2025

// Main Concept: Parallel/Concurrent Programming in C#
// With Help from Grok

/* 

Difference Between Parallel/Concurrent Programming and Asynchronous Programming:


(1) Parallel/Concurrent Programming


Definition: Involves executing multiple tasks simultaneously (parallel) or 
interleaving their execution (concurrent)
to improve performance by utilizing multiple CPU cores or threads.

Parallel: Tasks run simultaneously on multiple threads or cores, often for CPU-bound tasks (e.g., computations). 
The Task Parallel Library (TPL) in .NET facilitates this.

Concurrent: Tasks may share a single thread or core, with execution interleaved (e.g., via context switching)
for tasks like I/O operations or managing multiple activities.

Goal: Maximize CPU utilization and reduce processing time for compute-intensive tasks.


(2) Asynchronous Programming


Definition: Involves executing tasks in a non-blocking manner, 
allowing the main thread to continue other work while waiting for operations (often I/O-bound) to complete.

Uses 'async' and 'await' in C# to pause execution without blocking the thread, freeing it for other tasks.
Ideal for I/O-bound operations like file reading, database queries, or HTTP requests, 
where the program waits for external resources.

Goal: Improve responsiveness and scalability by avoiding thread blocking.

Example: Using Task with await to fetch data from a web API without freezing the UI.


---------------------------------


Execution Focus: 

- Parallel programming focuses on dividing work across multiple threads/cores for speed (CPU-bound). 
- Asynchronous programming focuses on non-blocking operations to keep the application responsive (I/O-bound).

Resource Usage:

- Parallelism creates multiple threads, consuming more CPU resources. 
- Async programming often reuses a single thread, releasing it during waits.

Tools in .NET: 

- TPL ('Task', 'Parallel') is used for parallel programming. 
- 'async/await' with 'Task' is used for asynchronous programming.


---------------------------------


Main Namespace and Classes:

- System.Threading.Tasks namespace: Provides classes for both parallel and asynchronous programming.
  - Parallel class: Contains methods for parallel loops and tasks.
  - Task class : Represents an asynchronous operation.
 
*/


namespace ParallelProgrammingDemo
{
    internal class Program
    {
        // A synchronous method to simulate a CPU-bound task
        static int ComputeSquare(int number)
        {
            // Simulate some work
            // Synchronous delay for demo
            // Delay time is in milliseconds.
            // For I/O-bound tasks, you’d typically use async/await with Task (e.g., await Task.Delay instead of Task.Delay().Wait).
            Task.Delay(1000).Wait();
            return number * number;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Parallel/Concurrent Programming in C#.NET.");
            Console.WriteLine("------------------------------------------\n");


            // (1) Using 'Parallel.For' for parallel processing (PARALLELISM)
            Console.WriteLine("Starting Parallel.For...\n");

            // The third input parameter is an Action<int> delegate
            // that points to a method that takes an integer parameter and returns void.
            // So, in each loop iteration, the method (lambda expression) is called with the current index value.
            // Because the loop is executed in a parallel manner, multiple iterations may run simultaneously on different threads.
            // Therefore, the output may appear in a non-sequential order.
            // In addition, the total time spent for the loop will be significantly less than the time spent for a sequential loop.
            // 'Parallel.For' iterates from 1 to 4 in parallel, computing the square of each number.
            // It automatically distributes the work across available CPU cores/threads, showing different thread IDs in the output.
            Parallel.For(1, 5, i =>
            {
                int result = ComputeSquare(i);
                Console.WriteLine($"Square of {i} = {result} (Thread: {Environment.CurrentManagedThreadId})");
            });


            // (2) Using 'Task' for asynchronous parallel tasks (CONCURRENCY)
            Console.WriteLine("\nStarting Task-based processing...\n");

            // Create an array of tasks
            Task[] tasks = new Task[4];

            // Start multiple tasks
            // 'Task' creates an array of Task objects, each computing a square asynchronously.
            // Task.Run() schedules the work on the thread pool, and 'Task.WaitAll' ensures all tasks complete before exiting.
            for (int i = 1; i <= 4; i++)
            {
                int num = i;
                // 'Run()' method queues the specified work to run on the thread pool.
                // 'Run()' accepts an Action delegate that represents the work to be executed.
                tasks[i - 1] = Task.Run(() =>
                {
                    int result = ComputeSquare(num);
                    Console.WriteLine($"Square of {num} = {result} (Thread: {Environment.CurrentManagedThreadId})");
                });
            }

            // Wait for all tasks to complete
            Task.WaitAll(tasks);
            Console.WriteLine("\nAll tasks completed!");


            Console.WriteLine("\nDone.");
        }
    }
}

/* 

FINAL NOTES:

- First loop (Parallel.For) (PARALLELISM):
  Iterations run simultaneously across multiple threads, as managed by the TPL.

- Second loop (Task) (CONCURRENCY):
  Tasks are started sequentially but execute concurrently on the thread pool, not in a non-blocking async manner in this example.
  
- ASYNCHRONY:
  For true async behavior, you'd typically use 'async' and 'await' keywords.
 
*/
