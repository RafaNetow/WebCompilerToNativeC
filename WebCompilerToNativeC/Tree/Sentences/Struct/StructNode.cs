using System;
using System.Collections.Generic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes.Struct;
using WebCompilerToNativeC.Tree.Sentences.Declaretion;

namespace WebCompilerToNativeC.Tree.Sentences.Structs
    //Que tenga el mismo Lenght
    //Que no tenga propiedades
    //
{   //strcut.x[2][3]
    //struct.x[2]
    //struct.x
   public  class StructNode : SentencesNode
    {
        public IdVariable NameOfStruct = new IdVariable();
        public  List<DeclarationNode>  StructItems = new List<DeclarationNode>();
       public List<IdVariable> VariableDeclaretion; 
        public override void ValidateSemantic()
       {
            Context.StackOfContext.Stack.Push(new TypesTable());
            var listParams = new List<StructParams>();
            foreach (var item in StructItems)
            {
                item.ValidateSemantic();
            
                listParams.Add(new StructParams() {Name = item.Variable.Value, LengOfProperties = item.Variable.Accesors.Count, Type = item.Type });

            }

           
            
        Context.StackOfContext.Stack.Pop();

            foreach (var variableToDeclaretion in VariableDeclaretion)
            {
                Context.StackOfContext.Stack.Peek().RegisterType(variableToDeclaretion.Value, new StructType(listParams),variableToDeclaretion.Accesors.Count);
            }
        Context.StackOfContext.RegisterType(NameOfStruct.Value, new StructType(listParams) );
       }

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }

       public override void Interpretation()
       {
           throw new NotImplementedException();
       }
    }
}
