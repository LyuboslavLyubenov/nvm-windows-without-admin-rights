using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Asbtract class that must be inherited from all commands.
/// </summary>
public abstract class CustomCommand : ICustomCommand {
    public abstract Task Execute(string[] args);

    /// <summary>
    /// Adds value to User environment PATH variable
    /// </summary>
    /// <param name="value">value thats going to be added to the PATH variable</param>
    protected void AddToPath(string value) {
        var name = "PATH";
        var scope = EnvironmentVariableTarget.User;
        var oldValue = Environment.GetEnvironmentVariable(name, scope);
        oldValue += ";" + value;
        Environment.SetEnvironmentVariable(name, oldValue, scope);
    }

    /// <summary>
    /// Removes all values that contain current directory from User Path environment variable
    /// </summary>
    protected void RemoveAllFromPath() {
        var name = "PATH";
        var scope = EnvironmentVariableTarget.User;
        var oldValue = Environment.GetEnvironmentVariable(name, scope);
        var paths = oldValue.Split(';');
        var currentPath = Directory.GetCurrentDirectory();
        var newPaths = paths.Where(path => !path.Contains(currentPath)).ToArray();
        var newValue = string.Join(';', newPaths);
        Environment.SetEnvironmentVariable(name, newValue, scope);
    }

    /// <summary>
    /// Implemented by concrete class. Says what is this command used for
    /// </summary>
    /// <returns></returns>
    public override abstract string ToString();
}