using System.Runtime.CompilerServices;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;

namespace CDA.Models;


public static class Logger
{
    public static string _directory
    {
        get
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName, "logs");
        }
    }

    public static ILog GetLogger()
    {
        ILog logger = LogManager.GetLogger(typeof (Logger));
        log4net.Repository.Hierarchy.Hierarchy repository = (log4net.Repository.Hierarchy.Hierarchy) LogManager.GetRepository();
        PatternLayout patternLayout = new PatternLayout()
        {
            ConversionPattern = "%date [%thread] %-5level %logger - %message%newline"
        };
        DateTime now = DateTime.Now;
        RollingFileAppender rollingFileAppender = new RollingFileAppender();
        rollingFileAppender.AppendToFile = true;
        string directory = Logger._directory;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        interpolatedStringHandler.AppendFormatted<DateTime>(now, "dd-MM-yyyy");
        interpolatedStringHandler.AppendLiteral(".txt");
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        rollingFileAppender.File = Path.Combine(directory, stringAndClear);
        rollingFileAppender.Layout = (ILayout) patternLayout;
        RollingFileAppender newAppender = rollingFileAppender;
        newAppender.ActivateOptions();
        repository.Root.AddAppender((IAppender) newAppender);
        repository.Root.Level = Level.All;
        repository.Configured = true;
        return logger;
    }

    public static void Prepare() => Directory.CreateDirectory(Logger._directory);
}