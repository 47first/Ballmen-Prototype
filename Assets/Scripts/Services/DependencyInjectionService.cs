using System;
using System.Collections.Generic;

namespace Ballmen.Services
{
    // Can be accessed in any time, so better register all
    // dependencies in some start point
    public sealed class DependencyInjectionService
    {
        private static DependencyInjectionService _instance;
        private Dictionary<Type, object> _dependencies;
        private DependencyInjectionService() 
        {
            _dependencies = new();
        }

        public static DependencyInjectionService Singleton 
        {
            get 
            {
                if(_instance == null)
                    _instance = new();

                return _instance;
            }
        }

        public void Register<T, K>(K instance, bool overrideDependency) where T : K
        {
            var type = typeof(T);

            if (_dependencies.TryAdd(type, instance) == false && overrideDependency)
                _dependencies[type] = instance;
        }

        public T GetDependency<T>()
        { 
            return (T)_dependencies[typeof(T)];
        }
    }
}
