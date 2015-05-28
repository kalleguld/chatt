namespace serviceInterface
{
    public class ConnectionFactory
    {
        public Connection GetConnection()
        {
            return new Connection();
        }
    }
}
