class Program
{
public static async Task<string> Task1Async()
    {
        await Task.Delay(2000);
        return "Task 1 completed";
    }
    public static async Task<string> Task2Async()
    {
        await Task.Delay(3000);
        return "Task 2 completed";
    }
    public static async Task<string> Task3Async()
    {
        await Task.Delay(1000);
        return "Task 3 completed";
    }
    public static async Task Main(string[] args)
    {
        var tasks = new List<Task<string>>()
        {
        Task1Async(),
        Task2Async(),
        Task3Async()
        };
        string[] result = await Task.WhenAll(tasks);
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
    }
}
