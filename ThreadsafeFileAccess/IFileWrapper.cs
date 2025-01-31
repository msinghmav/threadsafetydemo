namespace ThreadsafeFileAccess
{
    internal interface IFileWrapper
    {
        FileOperationResult Initialize(string? filePath, string content);

        FileOperationResult Write(string filePath, string content);
    }
}