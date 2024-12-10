class Program
{
    public static async Task<string?> ReadFileAsync(string filePath)
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
         return await sr.ReadToEndAsync();
        }
    }
    public static async Task Main(string[] args)
    {
        string path = "C:\\Users\\sasha\\source\\repos\\asyncStuff\\Task5\\test.txt";
        try
        {
            var result = await ReadFileAsync(path);
        Console.WriteLine(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception caught: {ex.Message}");
        }
    }
}
