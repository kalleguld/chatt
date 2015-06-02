namespace serviceInterface.service
{
    public class BaseService
    {
        protected Connection Connection { get; private set; }

        protected BaseService(Connection connection)
        {
            Connection = connection;
        }

    }
}
