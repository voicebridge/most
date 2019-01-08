using System.Collections.Generic;

namespace VoiceBridge.Most
{
    public class ExtensionModel
    {
        private readonly Dictionary<string, object> store = new Dictionary<string, object>();
        
        public void Add<TModel>(TModel instance, string instanceName = null)
        {
            var key = GetKey<TModel>(instanceName);
            this.store[key] = instance;
        }

        public TModel Get<TModel>(string name = null)
        {
            if (!this.IsAvailable<TModel>(name))
            {
                return default(TModel);
            }

            return (TModel) this.store[GetKey<TModel>(name)];
        }

        public bool IsAvailable<TModel>(string name = null)
        {
            return this.store.ContainsKey(GetKey<TModel>(name));
        }
        
        private static string GetKey<TModel>(string name = null)
        {
            var key = typeof(TModel).FullName;

            if (name != null)
            {
                key += "+" + name;
            }

            return key;
        }
    }
}