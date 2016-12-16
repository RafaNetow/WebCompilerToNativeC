using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.Sentences
{
  public  class DoWhile : SentencesNode
  {
      public ExpressionNode WhileCondition;
      public List<SentencesNode> Sentences;
          

        public override void ValidateSemantic()
      {
            if (!(WhileCondition.ValidateSemantic() is BooleanType))
                throw new SemanticException("Se esperaba expresion booleana en la sentencia while");
            foreach (var statement in Sentences)
            {
                statement.ValidateSemantic();
            }
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
