namespace ThreadsafeFileAccess
{
    internal sealed class FileWrapper : IFileWrapper
    {
        public FileOperationResult Initialize(string? filePath, String content)
        {
            if (filePath == null)
            {
                return FileOperationResult.Failure(new FailureDetail("File Path not provided", null));
            }

            try
            {
                string? directory = Path.GetDirectoryName(filePath);

                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(filePath, content);
            }
            catch(Exception ex)
            {
                return FileOperationResult.Failure(new FailureDetail("Failed while creating file", ex));
            }

            return FileOperationResult.Success();
        }

        public FileOperationResult Write(String filePath, String content)
        {
            try
            {
                File.AppendAllText(filePath, content);
            }
            catch(Exception ex)
            {
                return FileOperationResult.Failure(new FailureDetail("Error while writing to file", ex));
            }

            return FileOperationResult.Success();
        }
    }
}
