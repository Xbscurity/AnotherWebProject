class Program 
{
    public static async Task SimulateWorkAsync(IProgress<int> progress)
    {
        for (int i = 0; i <= 100; i++) {
            await Task.Delay(30);
            progress.Report(i);
                }

    }
    public static async Task Main(string[] args)
    {
        var progress = new Progress<int>(count => 
        { 
            Console.Clear();
            Console.WriteLine($"Loading {count}...");
        }
        );
    await SimulateWorkAsync(progress);
    }
}