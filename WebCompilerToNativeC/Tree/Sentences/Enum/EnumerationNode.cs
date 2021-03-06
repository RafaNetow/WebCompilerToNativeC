﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Tree.DataType.IdNode;
using WebCompilerToNativeC.Tree.Sentences.Declaretion;

namespace WebCompilerToNativeC.Tree.Sentences.Enum
{
   public class EnumerationNode: SentencesNode
    {
        public IdNode NameOfEnum { get; set; }
        public  List<IdNode> ListEnum { get; set; }

        public override void ValidateSemantic()
       {
           Context.StackOfContext.Stack.Peek().RegisterType(NameOfEnum.Value,new EnumType(ListEnum), 0);
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
