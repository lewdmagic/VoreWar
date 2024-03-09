namespace Utility
{
    public class CommandLine
    {
        // Helper function for getting the command line arguments
        public static string GetArg(string name)
        {
            var args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == name && args.Length > i + 1)
                {
                    return args[i + 1];
                }
            }
            return null;
        }
    
        // Helper function for getting the command line arguments
        public static bool HasFlag(string name)
        {
            var args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}