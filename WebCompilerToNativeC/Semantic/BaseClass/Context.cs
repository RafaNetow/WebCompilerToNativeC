using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic.BaseClass.BaseTypes;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Semantic.BaseTypes.Struct;
using WebCompilerToNativeC.Tree.Sentences.Declaretion;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
    public   class Context
    {
        private static Context _context ;
        public Stack<TypesTable> Stack = new Stack<TypesTable>();
        public Dictionary<Guid,TypesTable> RemembersContext = new Dictionary<Guid, TypesTable>();
        public Dictionary<string, FunctionDeclaretion> FunctionsNodes;

        public Dictionary<string, BaseType> Table;
      

        public Context()
        {
            Stack.Push(new TypesTable());
            Table = new Dictionary<string, BaseType>
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
           
            
                if (Table.ContainsKey(name))
                    return Table[name];
            

            throw new SemanticException($"Type : {name} doesn't exist.");
        }

        public void RegisterType(string name, BaseType baseType)
        {

            if (StackOfContext.Table.ContainsKey(name))
            {
                throw new SemanticException($"Type :{name} exists.");
            }
            if (Context.StackOfContext.Stack.Peek().Contains(name))
                throw new SemanticException($"  :{name} is a type.");
            

            Table.Add(name, baseType);
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
