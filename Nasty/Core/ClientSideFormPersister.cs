using Nasty.Utils;
using System;
namespace Nasty.Core
{
    public class ClientSideFormPersister : IFormPersister {

        public readonly static IFormPersister _instance = new ClientSideFormPersister();

        private ClientSideFormPersister() {
        }

        public static IFormPersister Instance {
            get { return _instance; }
        }

        public Form Lookup(string key) {
            return (Form) SerializationUtils.DeserializeObject(Convert.FromBase64String(key));
        }

        public string Persist(Form form) {
            byte[] bytes = SerializationUtils.SerializeObject(form);
            return Convert.ToBase64String(bytes);
        }
    }
}