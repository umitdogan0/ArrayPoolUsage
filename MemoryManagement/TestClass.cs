using System.Buffers;
using System.Text;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.ObjectPool;

namespace MemoryManagement;

public class TestClass
{
    [Benchmark]
    public async Task BenchWithOpti()
    {
       var poolForInt = ArrayPool<int>.Shared;
               
        var poolForString = ArrayPool<string>.Shared; 
        
        DefaultObjectPoolProvider objectPoolProvider = new DefaultObjectPoolProvider();
        
        ObjectPool<ExampleObjectForNormal> objectPool =
            objectPoolProvider.Create(new DefaultPooledObjectPolicy<ExampleObjectForNormal>());

        for (int j = 0; j < 10000; j++)
        { 
          await  Task.Run(() =>
            {
                var intArray = poolForInt.Rent(1000);
                var stringArray = poolForString.Rent(1000);
                var object1 = objectPool.Get();
                for (int i = 0; i < 1000; i++)
                {
                    intArray[i] = i + 1;
                }

                for (int a = 0; a < 1000; a++)
                {
                    stringArray[a] = "a";

                }

                object1.Ages = intArray;
                object1.Names = stringArray;

                poolForInt.Return(intArray);
                poolForString.Return(stringArray);
                objectPool.Return(object1);
            });
            

        }
    } 
    [Benchmark]
   public async Task Bench()
   {
           for (int j = 0; j < 10000; j++)
           {
              await Task.Run(() =>
                   {
                       int[] intArray = new int[1000];
                       string[] stringArray = new string[1000];
                       ExampleObjectForNormal object1 = new();
                       for (int i = 0; i < 1000; i++)
                       {
                           intArray[i] = i + 1;
                       }

                       for (int a = 0; a < 1000; a++)
                       {
                           stringArray[a] ="a";
                       }

                       object1.Ages = intArray;
                       object1.Names = stringArray;
                   }
               );
               
           }
        
    }
}