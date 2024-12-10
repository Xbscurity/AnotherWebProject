class Program
{
   public static async Task FailingTaskAsync()
    {
   await Task.Delay(1);
    throw new Exception("Ошибка в задаче");
    }
    public static async Task SafeTaskAsync()
    {
        await Task.Delay(2000);
        Console.WriteLine("Безопасная задача завершена");
    }
    public static async Task Main(string[] args)
    {
        try
        {
            var tasks = new List<Task>()
        {

        FailingTaskAsync(),
        SafeTaskAsync()
    };
            await Task.WhenAll(tasks);
        }
        catch (Exception) 
        {
            Console.WriteLine("Exception caught");
        }

        }
}