using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Hanlders;

namespace WebCompilerToNativeC
{
    class Program
    {
        static void Main(string[] args)
        {
            HanldersFiles handlerFile = new HanldersFiles();
            var sourceCodes = handlerFile.getCode();
            Console.WriteLine(sourceCodes);
            Console.ReadKey();


        }
    }
}
