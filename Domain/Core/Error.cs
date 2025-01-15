namespace Domain.Core
{
     public  class Error 
    {
        public string Code { get;private set; }
        public string? Descripcion { get; private set; }

        public Error(string code,string? descripcion = null)
        {
            Code = code;
            Descripcion = descripcion;
        }

        public static readonly Error None = new(string.Empty);
        public static readonly Error NullValue = new("Error.NullValue");
        public static readonly Error Unknow = new("Error.Desconocido");

        public bool Equals(Error? other) {
            return other is not null && Code == other.Code;
        }

    }
}