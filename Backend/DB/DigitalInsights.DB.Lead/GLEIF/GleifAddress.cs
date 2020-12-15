namespace DigitalInsights.DB.Lead.GLEIF
{
    public partial class GleifAddress
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public string Xmllang { get; set; }
        public string Type { get; set; }
        public string Firstaddressline { get; set; }
        public string Addressnumber { get; set; }
        public string Addressnumberwithinbuilding { get; set; }
        public string Mailrouting { get; set; }
        public string Additionaladdressline1 { get; set; }
        public string Additionaladdressline2 { get; set; }
        public string Additionaladdressline3 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Postalcode { get; set; }

        public virtual GleifEntity Entity { get; set; }
    }
}
