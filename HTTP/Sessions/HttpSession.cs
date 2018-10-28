using System;
using System.Collections.Generic;

namespace HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> parameters;

        public HttpSession(string id)
        {
            this.parameters = new Dictionary<string, object>();
            this.Id = id;
        }
        
        public string Id { get; }

        public void AddParameter(string name, object parameter)
        {
            if (this.ContainsParameter(name))
            {
                throw new ArgumentException($"Parameter {name} already exists");
            }
            this.parameters.Add(name, parameter);
        }

        public void ClearParameters() => this.parameters.Clear();

        public bool ContainsParameter(string name) => this.parameters.ContainsKey(name);

        public object GetParameter(string name)
        {
            if (!this.ContainsParameter(name))
            {
                return null;
            }

            return this.parameters[name];
        }
    }
}
