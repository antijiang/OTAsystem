namespace OTASystem.Data.Vo
{
    public class Result<T>
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T>
            {
                Code = 200,
                Msg = data.ToString(),
                Data = data
            };
        }

        public static Result<T> Error(int code, string msg = null)
        {
            return new Result<T>
            {
                Msg = msg,
                Code = code
            };
        }
    }
}
