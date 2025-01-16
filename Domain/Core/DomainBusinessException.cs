namespace Domain.Core {
    public class DomainBusinessException : Exception
    {
        public DomainBusinessException(string exception) : base(exception) { }
    }
}