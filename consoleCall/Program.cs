class Program
{
    async static Task<string> LoadDataAsync()
    {
        await Task.Delay(1000);
         return "Data loaded";
    }
    public static async Task Main(string[] args)
    {
        var result = await LoadDataAsync();
        Console.WriteLine(result);
    }
}