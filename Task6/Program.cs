class Program
{
    static int counter = 0;
    static readonly object counterLock = new object();

    public static async Task IncrementCounterAsync()
    {
        for (int i = 0; i < 1000; i++)
        {
     
                counter++;  // Теперь доступ к counter защищен
            
            await Task.Delay(1);
        }
    }

    public static async Task Main(string[] args)
    {
        Task task1 = IncrementCounterAsync();
        Task task2 = IncrementCounterAsync();
        await Task.WhenAll(task1, task2);
        Console.WriteLine($"Counter: {counter}");  // Теперь результат будет 2000
    }
}
