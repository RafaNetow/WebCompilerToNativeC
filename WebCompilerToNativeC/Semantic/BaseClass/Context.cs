using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
    public   class Context
    {
        private static Context _context ;
        public Stack<TypesTable> Stack = new Stack<TypesTable>();


        public Context()
        {
            Stack.Push(new TypesTable());
        }

        public static Context StackOfContext
        {
            get
            {
                if(_context == null)
                    _context = new Context();
                return _context;
            }
           
        } 


    }
}
