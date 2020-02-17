using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SIS.MvcFramework
{
    public class ServiceCollection : IServiceCollection
    {
        private IDictionary<Type, Type> dependencyContainer = new ConcurrentDictionary<Type, Type>();

        public void Add<TSource, TDestination>()
            where TDestination : TSource
        {
            dependencyContainer[typeof(TSource)] = typeof(TDestination);
        }

        public object CreateInstance(Type type)
        {
            if (dependencyContainer.ContainsKey(type))
            {
                type = dependencyContainer[type];
            }

            var constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .OrderBy(x => x.GetParameters().Count()).FirstOrDefault();

            var parametersValues = new List<object>();
            foreach (var parameter in constructor.GetParameters())
            {
                var instance = CreateInstance(parameter.ParameterType);
                parametersValues.Add(instance);
            }

            return constructor.Invoke(parametersValues.ToArray());
        }

        public T CrateInstabce<T>()
        {
            return (T) CreateInstance(typeof(T));
        }
    }
}
