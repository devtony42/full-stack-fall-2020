namespace Emailer
{
    public class BoolResult
    {
        public string? ValueName { get; set; } 
        public bool Value { get; set; }
    }
    
    public static class Extensions
    {
        public static BoolResult AsResult(this bool value)
        {
            return new BoolResult
            {
                ValueName = "Result",
                Value = value
            };
        }
    }
}