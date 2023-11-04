namespace LTA.Application.Common.Interfaces;

public interface ILoggerAdapter<T>
{
    void LogInformation(string message, params object[] args);
    void LogError(Exception ex, string message, params object[] args);
}