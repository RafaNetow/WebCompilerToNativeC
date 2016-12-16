using System;
using System.Collections.Generic;
using System.Linq;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Semantic.BaseTypes.Struct;
using WebCompilerToNativeC.Tree;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
    public class TypesTable
    {
        public Dictionary<string, BaseType> _table;
        public Dictionary<string, InforamtionVariable> InformatioNVariable;
        public Dictionary<string, Value> TableValue; 
        private static TypesTable _instance;
        public TypesTable()
        {

            InformatioNVariable = new Dictionary<string, InforamtionVariable>();
            _table = new Dictionary<string, BaseType>();
           TableValue = new Dictionary<string, Value>();
           // _table.Add("int", new IntType());
           // _table.Add("string", new StringType());
           // _table.Add("float", new FloatType());
           // _table.Add("char", new CharType());
           // _table.Add("bool", new BooleanType());
           //_table.Add("date", new DateType()); 
           // _table.Add("const", new ConstType());
           // _table.Add("struct", new StructType(new List<StructParams>()));
           // _table.Add("enum", new EnumType(null));
        }


        public static TypesTable Instance => _instance ?? (_instance = new TypesTable());
        public void RegisterType(string name, BaseType baseType, int propertiesOfVarible)
        {
       
            if (Context.StackOfContext.Stack.Peek()._table.ContainsKey(name))
            {
                throw new SemanticException($"Type :{name} exists.");
            }
            if (Context.StackOfContext.Stack.Peek().Contains(name))
                throw new SemanticException($"  :{name} is a type.");
            baseType.LenghtOfProperties = propertiesOfVarible;
           
            _table.Add(name, baseType);
            TableValue.Add(name,baseType.GetDefaultValue());
        }


        public void SetVariableValue(string variableName, Value val)
        {
            TableValue[variableName] = val;
        }

        public Value GetVariableValue(string variableName)
        {
            return TableValue[variableName];
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

    public class InforamtionVariable
    {
        public int Lenght;

    }


    internal class SemanticException : Exception
    {
        public SemanticException(string message) : base(message)
        {


        }
    }
   

}

