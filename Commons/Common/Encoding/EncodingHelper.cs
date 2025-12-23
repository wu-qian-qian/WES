namespace Common.Encoding;

public static class EncodingHelper
{
    public static System.Text.Encoding GetEncodingFromContentType(string? charset)
    {
        if (!string.IsNullOrWhiteSpace(charset))
            try
            {
                return System.Text.Encoding.GetEncoding(charset);
            }
            catch
            {
                return System.Text.Encoding.UTF8;
            }

        return System.Text.Encoding.UTF8;
    }
    
    
}