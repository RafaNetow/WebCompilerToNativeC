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
        public Dictionary<string, List<Value>> ValuesOfArrays;
        private static TypesTable _instance;
        public TypesTable()
        {

            InformatioNVariable = new Dictionary<string, InforamtionVariable>();
            _table = new Dictionary<string, BaseType>();

           TableValue = new Dictionary<string, Value>();
            ValuesOfArrays = new Dictionary<string, List<Value>>();

           // Table.Add("int", new IntType());
           // Table.Add("string", new StringType());
           // Table.Add("float", new FloatType());
           // Table.Add("char", new CharType());
           // Table.Add("bool", new BooleanType());
           //Table.Add("date", new DateType()); 
           // Table.Add("const", new ConstType());
           // Table.Add("struct", new StructType(new List<StructParams>()));
           // Table.Add("enum", new EnumType(null));
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
             foreach (var stack in Context.StackOfContext.Stack)
            {
                if (stack.TableValue.ContainsKey(variableName))
                {
                    stack.TableValue[variableName] = val;
                }
            }
        }

        public Value GetVariableValue(string variableName)
        {
            foreach (var stack in Context.StackOfContext.Stack)
            {
                if (stack.TableValue.ContainsKey(variableName))
                {
                    return stack.TableValue[variableName];
                }
            }
            return null;
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

        public void SetArrayVariableValue(string name, Value value)
        {
            foreach (var stack in Context.StackOfContext.Stack)
            {


                List<Value> existing;
                if (!stack.ValuesOfArrays.TryGetValue(name, out existing))
                {
                    existing = new List<Value>();
                    stack.ValuesOfArrays[name] = existing;
                }
                // At this point we know that "existing" refers to the relevant list in the 
                // dictionary, one way or another.
                int pos = 0;
                foreach (var value1 in existing.ToList())
                {
                    if (value1.row == value.row && value1.column == value.column)
                    {
                        existing[pos] = value;
                    }
                    pos++;
                }
            }
        }

        public void SetArrayVariableValue(string name, List<Value> values)
        {
            foreach (var stack in Context.StackOfContext.Stack)
            {
                if (stack.TableValue.ContainsKey(name))
                {
                    stack.ValuesOfArrays[name] = values;
                }
            }
        }


        public List<Value> GetArrayVariableValues(string name)
        {
            foreach (var stack in Context.StackOfContext.Stack)
            {
                if (stack.TableValue.ContainsKey(name))
                {
                    return stack.ValuesOfArrays[name];
                }
            }
            return null;
        }

        public Value GetArrayValue(string name, int? row, int? column)
        {
            foreach (var stack in Context.StackOfContext.Stack)
            {
                if (stack.TableValue.ContainsKey(name))
                {
                    var list = stack.ValuesOfArrays[name];
                var value =    list.FirstOrDefault(x => x.row == row && x.column == column);
                    return value;
                }
            }
            return null;
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

