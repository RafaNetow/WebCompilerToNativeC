using System;
using System.Collections.Generic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Semantic.BaseTypes.Struct;
using WebCompilerToNativeC.Tree.Sentences.Declaretion;

namespace WebCompilerToNativeC.Tree.Sentences
    //Que tenga el mismo lenght
    //Que no tenga propiedades
    //
{   //strcut.x[2][3]
    //struct.x[2]
    //struct.x
   public  class StructNode : SentencesNode
    {
        public IdVariable NameOfStruct = new IdVariable();
        public  List<DeclarationNode>  StructItems = new List<DeclarationNode>();
        public override void ValidateSemantic()
       {
            var listParams = new List<StructParams>();
            foreach (var item in StructItems)
            {
                listParams.Add(new StructParams() {Name = item.Variable.Value, LengOfProperties = item.Variable.Accesors.Count});
            }


        TypesTable.Instance.RegisterType(NameOfStruct.Value, new StringType());
       }

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }
    }
}
