using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Hanlders
{
   public class HanldersFiles
   {
       private string _defaulPath;

       public HanldersFiles(string defaulPath)
       {
           this._defaulPath = defaulPath;
       }

       public HanldersFiles()
       {
           this._defaulPath = System.Web.Hosting.HostingEnvironment.MapPath("~/bin/SourceCode.c");
         //   this._defaulPath = "SourceCode.txt";
        }
        public string getCode()
        {
            string file = "";
            try
            {
                file = System.IO.File.ReadAllText(_defaulPath);
            }
            catch (Exception e)

            {
                Console.Write(" No se ha encontrado el archivo");
                return "";
            }
            return file;

        }

    }


}
