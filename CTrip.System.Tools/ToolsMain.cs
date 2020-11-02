using CTrip.System.Interfaces.Service;
using System;

namespace CTrip.System.Tools
{
    class ToolsMain
    {
        static void Main(string[] args)
        {
            var tables = new ToolsService().GetAllTables();
            foreach(var table in tables)
            {
                //Console.Write($"生成[{ table }]表 模型: ");
                //Console.WriteLine(new ToolsService().CreateModels("..\\..\\..\\..\\CTrip.System.Model\\Entity", "CTrip.System.Model", table, ""));
                Console.Write($"生成[{ table }]表 服务: ");
                Console.WriteLine(new ToolsService().CreateServices("..\\..\\..\\..\\CTrip.System.Interfaces\\Service", "CTrip.System.Interfaces", table));
                Console.Write($"生成[{ table }]表 接口: ");
                Console.WriteLine(new ToolsService().CreateIServices("..\\..\\..\\..\\CTrip.System.Interfaces\\IService", "CTrip.System.Interfaces", table));
            }
            Console.Write($"生成DbContext: ");
            Console.WriteLine(new ToolsService().CreateDbContext("..\\..\\..\\..\\CTrip.System.Core\\DbContext.cs", "CTrip.System.Core"));
            Console.ReadKey();
        }
    }
}
