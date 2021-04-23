namespace BrokenLinksTesting.WPF.Common
{
    public class Link
    {
        internal int Id { get; set; }
        internal string RequestDate { get; set; }
        internal string RequestMethod { get; set; }
        internal string URL { get; set; }
        internal string ResponseCode { get; set; }
        internal string ResponseDetails { get; set; }
        internal LinkTypeEnum Type { get; set; }
    }
}
