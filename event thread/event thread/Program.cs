using System;
using System.Threading;

class Shared
{
    public static int[] Data { get; set; }
    public static int BatchCount { get; set; }
    public static int BatchSize { get; set; }
    public static ManualResetEvent Event { get; set; }

        static Shared()
        {
            BatchCount = 5;
            BatchSize = 3;
            Data = Enumerable.Range(1, BatchCount * BatchSize).ToArray();
            Event = new ManualResetEvent(true); // Initialize as true
        }

  

}

class Producer
{
    public void Produce()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} started");
        for (int i = 0; i < Shared.BatchCount; i++)
        {
            for (int j = 0; j < Shared.BatchSize; j++)
            {
                Shared.Data[i * Shared.BatchSize + j] = (i * Shared.BatchSize) + j + 1;
                Thread.Sleep(300);
            }
        }

        Shared.Event.Set();
        Console.WriteLine($"{Thread.CurrentThread.Name} completed");
    }
}

class Consumer
{
    public void Consume()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} started");
        Console.WriteLine("Consumer is waiting for producer thread to finish generating data");

        for (int i = 0; i < Shared.BatchCount; i++)
        {
            Shared.Event.WaitOne();
            Console.WriteLine("Received the signal");

            Console.WriteLine("\nData is");
            for (int j = 0; j < Shared.BatchSize; j++)
            {
                Console.WriteLine(Shared.Data[i * Shared.BatchSize + j]);
            }
        }

        Console.WriteLine($"{Thread.CurrentThread.Name} completed");
    }
}

class Program
{
    static void Main()
    {
        Producer producer = new Producer();
        Consumer consumer = new Consumer();

        // create delegate objects of ThreadStart
        ThreadStart threadStart1 = new ThreadStart(producer.Produce);
        ThreadStart threadStart2 = new ThreadStart(consumer.Consume);

        // create thread objects
        Thread producerThread = new Thread(threadStart1) { Name = "Producer Thread" };
        Thread consumerThread = new Thread(threadStart2) { Name = "Consumer Thread" };

        // starting threads
        producerThread.Start();
        consumerThread.Start();

        // joining the threads
        producerThread.Join();
        consumerThread.Join();
    }
}
