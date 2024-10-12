using System.Reflection;

namespace DynamicAuthSystem.API.Extensions
{
    public static class AssemblyUtilities
    {
        public static Assembly GetAssemblyByName(string expectedAssemblyName)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .FirstOrDefault(assembly => assembly.FullName.StartsWith(expectedAssemblyName));
        }
    }
}
