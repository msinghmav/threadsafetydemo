namespace ThreadsafeFileAccess
{
    internal sealed class FileWriter
    {
        private const string DateTimeFormat = "HH:mm:ss.fff";
        private static int lineCount = 0;
        private static object lockObject = new object();
        private readonly IFileWrapper _fileWrapper;
        private string _outputFilePath;

        public FileWriter(string outputFilePath)
        {
            _outputFilePath = outputFilePath;
            _fileWrapper = new FileWrapper();
        }

        public FileOperationResult Initialize()
        {
            return _fileWrapper.Initialize(_outputFilePath, GetLineContent(0));
        }

        public FileOperationResult WriteToFile(int threadId)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    lock (lockObject)
                    {
                        lineCount++;
                        var result = _fileWrapper.Write(_outputFilePath, GetLineContent(threadId));
                        if (!result.IsSuccess)
                            return result;
                    }
                }
            }
            catch(Exception ex)
            {
                return FileOperationResult.Failure(new FailureDetail($"Failure while writing to file for thread id {threadId}", ex));
            }

            return FileOperationResult.Success();
        }

        private static string GetLineContent(int threadId)
        {
            return $"{lineCount}, {threadId}, {DateTime.Now.ToString(DateTimeFormat)}\n";
        }
    }
}
