using System;
using System.Collections.Generic;
using System.Linq;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Semantic.BaseTypes.Struct;
using WebCompilerToNativeC.Tree;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
    public class TypesTable
    {
        public Dictionary<string, BaseType> _table;
        private static TypesTable _instance;
        public TypesTable()
        {
            _table = new Dictionary<string, BaseType>();
            _table.Add("int", new IntType());
            _table.Add("string", new StringType());
            _table.Add("float", new FloatType());
            _table.Add("char", new CharType());
            _table.Add("bool", new BooleanType());
           _table.Add("date", new DateType()); 
            _table.Add("const", new ConstType());
            _table.Add("struct", new StructType(new List<StructParams>()));
            _table.Add("enum", new EnumType(null));
        }


        public static TypesTable Instance => _instance ?? (_instance = new TypesTable());
        public void RegisterType(string name, BaseType baseType)
        {
       
            if (Context.StackOfContext.Stack.Peek()._table.ContainsKey(name))
            {
                throw new SemanticException($"Type :{name} exists.");
            }
            if (Context.StackOfContext.Stack.Peek().Contains(name))
                throw new SemanticException($"  :{name} is a type.");

            _table.Add(name, baseType);
        }


        public BaseType GetType(string name)
        {
           foreach(var table in Context.StackOfContext.Stack)
            {
                if (table._table.ContainsKey(name))
                    return table._table[name];
            }
            
            throw new SemanticException($"Type : {name} doesn't exist.");
        }


        public bool Contains(string name)
        {
            return Context.StackOfContext.Stack.Select(typesTable => typesTable._table.ContainsKey(name)).FirstOrDefault();
        }
    }

   


    internal class SemanticException : Exception
    {
        public SemanticException(string message) : base(message)
        {


        }
    }
   

}

