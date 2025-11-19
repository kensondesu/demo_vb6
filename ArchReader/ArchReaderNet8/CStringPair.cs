namespace ArchReaderNet8
{
    /// <summary>
    /// String pair class
    /// </summary>
    public class CStringPair
    {
        public string LValue { get; set; } = string.Empty;
        public string RValue { get; set; } = string.Empty;

        public CStringPair()
        {
        }

        public CStringPair(string lValue, string rValue)
        {
            LValue = lValue;
            RValue = rValue;
        }
    }
}
