namespace WebCompilerToNativeC.Tree.BaseClass
{
   public abstract class UnaryNode : ExpressionNode
    {
     
       public  ExpressionNode Value { get; set; }

       public string GetCode(string unarySymbol)
       {
           return $"{unarySymbol}+{Value.GenerateCode()} ";
       } 
    }
}
