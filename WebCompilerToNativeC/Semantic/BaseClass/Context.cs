using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Semantic.BaseTypes.Struct;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
    public   class Context
    {
        private static Context _context ;
        public Stack<TypesTable> Stack = new Stack<TypesTable>();

        public Dictionary<string, BaseType> _table;
      

        public Context()
        {
            Stack.Push(new TypesTable());
            _table = new Dictionary<string, BaseType>
            {
                {"int", new IntType()},
                {"string", new StringType()},
                {"float", new FloatType()},
                {"char", new CharType()},
                {"bool", new BooleanType()},
                {"date", new DateType()},
                {"const", new ConstType()},
                {"struct", new StructType(new List<StructParams>())},
                {"enum", new EnumType(null)}
            };
        }

        public BaseType GetType(string name)
        {
           
            
                if (_table.ContainsKey(name))
                    return _table[name];
            

            throw new SemanticException($"Type : {name} doesn't exist.");
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
