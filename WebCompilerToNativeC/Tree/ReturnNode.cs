﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree
{
   public class ReturnNode : SentencesNode
   {
       public ExpressionNode ExpressionToReturn;
        public bool ValidateSemantic(BaseType baseToCompare)
        {
            return ExpressionToReturn.ValidateSemantic() == baseToCompare;
        }

       public override void ValidateSemantic()
       {
           throw new NotImplementedException();
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
