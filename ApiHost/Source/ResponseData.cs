namespace MyApiClientGenerateSample;

public class ResponseData
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="success"></param>
    /// <param name="message"></param>
    /// <param name="code"></param>
    /// <param name="errorData"></param>
    public ResponseData(bool success = true, string message = "", int code = 0, IEnumerable<object>? errorData = null)
    {
        this.Success = success;
        this.Message = message;
        this.Code = code;
        this.ErrorData = (IEnumerable<object>) ((object) errorData ?? (object) Array.Empty<object>());
    }

    public bool Success { get; protected set; }

    public string Message { get; protected set; }

    public int Code { get; protected set; }

    public IEnumerable<object> ErrorData { get; protected set; }
}

public class ResponseData<T> : ResponseData
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="success"></param>
    /// <param name="message"></param>
    /// <param name="code"></param>
    /// <param name="errorData"></param>
    public ResponseData(
        T data,
        bool success = true,
        string message = "",
        int code = 0,
        IEnumerable<object>? errorData = null)
        : base(success, message, code, errorData)
    {
        this.Data = data;
    }

    public T Data { get; protected set; }
}


public static class ResponseDataExtensions
{
    public static ResponseData<TData> AsResponseData<TData>(
        this TData data,
        bool success = true,
        string message = "",
        int code = 0,
        IEnumerable<object>? errorData = null)
    {
        return new ResponseData<TData>(data, success, message, code, errorData);
    }

    public static async Task<ResponseData> AsResponseData(
        this Task task,
        bool success = true,
        string message = "",
        int code = 0,
        IEnumerable<object>? errorData = null)
    {
        await task;
        return new ResponseData(success, message, code, errorData);
    }

    public static async Task<ResponseData<TData>> AsResponseData<TData>(
        this Task<TData> data,
        bool success = true,
        string message = "",
        int code = 0,
        IEnumerable<object>? errorData = null)
    {
        return new ResponseData<TData>(await data, success, message, code, errorData);
    }
}
