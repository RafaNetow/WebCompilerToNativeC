using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic.BaseTypes;

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
            _table.Add("struct", new StructType());
            _table.Add("enum", new EnumType());
        }


        public static TypesTable Instance => _instance ?? (_instance = new TypesTable());
        public void RegisterType(string name, BaseType baseType)
        {

         
            if (_table.ContainsKey(name))
            {
                throw new SemanticException($"Type :{name} exists.");
            }
            if (Instance.Contains(name))
                throw new SemanticException($"  :{name} is a type.");

            _table.Add(name, baseType);
        }

        public BaseType GetType(string name)
        {
            if (_table.ContainsKey(name))
            {
                return _table[name];
            }
          
           
            throw new SemanticException($"Type :{name} doesn't exists.");
        }


        public bool Contains(string name)
        {
            return _table.ContainsKey(name);
        }
    }

    internal class EnumType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }
    }


    internal class SemanticException : Exception
    {
        public SemanticException(string message) : base(message)
        {


        }
    }

}

