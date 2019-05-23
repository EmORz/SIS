using SIS.HTTP.Common;
using System.Collections.Generic;
using SIS.HTTP.Sessions.Contracts;

namespace Demo.App.Sessions
{
    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> sessionParameters;

        public HttpSession(string id)
        {
            this.sessionParameters = new Dictionary<string, object>();
            this.Id = id;
        }
        public string Id { get; }
        public object GetParameter(string parameterName)
        {
            CoreValidator.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));
            return this.sessionParameters[parameterName];
        }

        public bool ContainsParameter(string parameterName)
        {
            CoreValidator.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));
            return this.sessionParameters.ContainsKey(parameterName);
        }

        public void AddParameter(string parameterName, object parameter)
        {
            CoreValidator.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));

            this.sessionParameters[parameterName] = parameter;
        }

        public void ClearParameters()
        {
            this.sessionParameters.Clear();
        }
    }
}