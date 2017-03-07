namespace Ocelot.Cluster
{
    public class GetAvailableOcelots : IGetAvailableOcelots
    {
        public void Get()
        {
            //get known cluster member
            //look up against that member for available ocelos
        }
    }

    public interface IGetAvailableOcelots
    {

    }
}