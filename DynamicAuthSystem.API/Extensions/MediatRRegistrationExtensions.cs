using MediatR;

namespace DynamicAuthSystem.API.Extensions
{
    public static class MediatRRegistrationExtensions
    {
        public static void RegisterMediatRHandler(this IServiceCollection services)
        {
            var applicationAssembly = AssemblyUtilities.GetAssemblyByName("DynamicAuthSystem.Application");

            var handlers = applicationAssembly.GetTypes()
                .Where(type => type.GetInterfaces().Any(interfaceType =>
                    interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                .ToList();

            foreach (var handler in handlers)
            {
                services.AddMediatR(x => x.RegisterServicesFromAssemblies(handler.Assembly));
            }
        }
    }
}
