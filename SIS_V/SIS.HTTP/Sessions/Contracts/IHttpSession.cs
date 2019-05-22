namespace Demo.App.Sessions.Contracts
{
    public interface IHttpSession
    {
        string Id { get; }
        object GetParameter(string parameterName);
        bool ContainsParameter(string parameterName);
        void AddParamete(string parameterName, object parameter);
        void ClearParameters();
    }
}