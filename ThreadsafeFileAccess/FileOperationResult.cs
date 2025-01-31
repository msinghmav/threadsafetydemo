namespace ThreadsafeFileAccess
{
    internal sealed record FileOperationResult
    {
        public bool IsSuccess { get; }
        public FailureDetail? FailureDetail { get; }

        private FileOperationResult(bool isSuccess, FailureDetail? errofailureDetailr)
        {
            IsSuccess = isSuccess;
            FailureDetail = errofailureDetailr;
        }

        public static FileOperationResult Success() => new FileOperationResult(true,  null);
        public static FileOperationResult Failure(FailureDetail failure) => new FileOperationResult(false, failure);
    }

    internal sealed record FailureDetail
    {
        public FailureDetail(string error, Exception? exception)
        {
            Error = error;
            ExceptionDetail = exception;
        }

        public string Error { get; } = "Operation failed";

        public Exception? ExceptionDetail { get; }

        public override string ToString()
        {
            return $"Error: {Error}, Exception: {ExceptionDetail?.Message}";
        }
    }
}
