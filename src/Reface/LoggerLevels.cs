namespace Reface
{
    public enum LoggerLevels
    {
        Debug = 1,
        Info = 2,
        Warning = 4,
        Error = 8,
        All = Debug | Info | Warning | Error
    }
}
