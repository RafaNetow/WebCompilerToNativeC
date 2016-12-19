namespace WebCompilerToNativeC.interpretation.BaseClass
{
   public abstract class Value

   {
       public  abstract Value Clone();
        public int? row { get; set; }
        public int? column { get; set; }
   
   
   }
}
