using Integration.Service;

namespace Integration;

public abstract class Program
{
    public static void Main(string[] args)
    {
        //OriginalMain();

        //There are three way to solution, please run methods one by one

        //SolutionMain(new ItemIntegrationServiceLockSolution());
        //SolutionMain(new ItemIntegrationServiceConcurrentDictionarySolution());
        SolutionMain(new ItemIntegrationServiceSemaphoreSlimSolution());
    }

    static void OriginalMain()
    {
        var service = new ItemIntegrationService();

        ThreadPool.QueueUserWorkItem(_ => service.SaveItem("a"));
        ThreadPool.QueueUserWorkItem(_ => service.SaveItem("b"));
        ThreadPool.QueueUserWorkItem(_ => service.SaveItem("c"));

        Thread.Sleep(500);

        ThreadPool.QueueUserWorkItem(_ => service.SaveItem("a"));
        ThreadPool.QueueUserWorkItem(_ => service.SaveItem("b"));
        ThreadPool.QueueUserWorkItem(_ => service.SaveItem("c"));

        Thread.Sleep(5000);

        Console.WriteLine("Everything recorded:");

        service.GetAllItems().ForEach(Console.WriteLine);

        Console.ReadLine();
    }

    static void SolutionMain(ISolution service)
    {
        ThreadPool.QueueUserWorkItem(_ => Console.WriteLine(service.SaveItem("a").Message));
        ThreadPool.QueueUserWorkItem(_ => Console.WriteLine(service.SaveItem("b").Message));
        ThreadPool.QueueUserWorkItem(_ => Console.WriteLine(service.SaveItem("c").Message));

        Thread.Sleep(500);

        ThreadPool.QueueUserWorkItem(_ => Console.WriteLine(service.SaveItem("a").Message));
        ThreadPool.QueueUserWorkItem(_ => Console.WriteLine(service.SaveItem("b").Message));
        ThreadPool.QueueUserWorkItem(_ => Console.WriteLine(service.SaveItem("c").Message));

        Thread.Sleep(5000);

        Console.WriteLine("Everything recorded:");

        service.GetAllItems().ForEach(Console.WriteLine);

        Console.ReadLine();
    }
}