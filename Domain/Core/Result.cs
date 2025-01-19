namespace Domain.Core {
    public class Result
    {
        public Error Error { get; protected set; }
        public bool IsSuccess { get; protected set; }
        public bool IsFailure => !IsSuccess;
        protected internal Result(bool isSuccess,Error error){
            if(isSuccess && error != Error.None || !isSuccess && error == Error.None){
                throw new ArgumentException("No es posible crear un Result de este tipo");
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()=> new  (true, Error.None);
        public static Result Failure(Error failure)=>  new (false,failure);
        public static Result<TValue> Success<TValue>(TValue value)=> new (value,true,Error.None);
        public static Result<TValue> Failure<TValue>(Error error)=> new (default,false,error);
        public static implicit operator Result(Error failure) => Failure(failure) ;
    }

    public class Result<TValue>:Result
    {
        private readonly TValue? _value;
        public TValue Value => IsSuccess? _value! : throw new InvalidOperationException("No se puede obtener de un failure!");
        protected internal Result(TValue? value, bool isSuccess, Error error):base(isSuccess,error){
            _value = value;
        }
        public static implicit operator Result<TValue>(TValue? value) => value is not null? Success(value):Failure<TValue>(Error.NullValue);
        public static implicit operator Result<TValue>(Error failure) => Failure<TValue>(failure) ;
        
    }


}