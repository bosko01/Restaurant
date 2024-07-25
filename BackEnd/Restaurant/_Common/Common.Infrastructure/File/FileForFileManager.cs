namespace Common.Infrastructure.File
{
    public class FileForFileManager
    {
        public string FileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public long Length { get; set; } = default;

        public byte[] Content { get; set; } = default!;
    }
}