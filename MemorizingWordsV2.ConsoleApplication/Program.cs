using System.Threading.Tasks;
using MemorizingWordsV2.ConsoleApplication.Interfaces;

namespace MemorizingWordsV2.ConsoleApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IApplication application = new Application();
            await application.Initialize().Run();
        }
    }
}
