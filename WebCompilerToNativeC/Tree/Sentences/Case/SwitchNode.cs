using System;
using System.Collections.Generic;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.Sentences.Case
{
    public  class SwitchNode : SentencesNode
    {
        public ExpressionNode OptionCase;
        public List<CaseStatement> CaseStatements;

        public override void ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }
}
