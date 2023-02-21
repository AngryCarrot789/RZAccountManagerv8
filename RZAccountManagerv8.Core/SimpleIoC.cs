using System;
using System.Collections.Generic;

namespace RZAccountManagerv8.Core {
    public class SimpleIoC {
        private readonly Dictionary<Type, object> services;

        public SimpleIoC() {
            this.services = new Dictionary<Type, object>();
        }

        public void Register(Type type, object value) {
            if (this.services.ContainsKey(type)) {
                throw new Exception("Type already registered: " + type);
            }

            this.services[type] = value;
        }

        public void Register<T>(T instance) {
            this.Register(typeof(T), instance);
        }

        public object Provide(Type type) {
            if (this.services.TryGetValue(type, out object value)) {
                return value;
            }
            else {
                throw new Exception($"No value registered for {type}");
            }
        }

        public T Provide<T>() {
            object value = this.Provide(typeof(T));
            if (value is T t) {
                return t;
            }

            throw new Exception($"Invalid registered value. {typeof(T)} cannot be assigned to value of {value?.GetType()}");
        }
    }
}
