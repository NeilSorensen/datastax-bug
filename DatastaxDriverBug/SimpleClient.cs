using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace DatastaxDriverBug
{
    public class SimpleClient
    {
        private Cluster _cluster;

        public Cluster Cluster { get { return _cluster; } }

        public void Connect(String node)
        {
            _cluster = Cluster.Builder()
                .AddContactPoint(node).Build();
            Metadata metadata = _cluster.Metadata;
            Console.WriteLine("Connected to cluster: "
                + metadata.ClusterName.ToString());
        }

        public void Close()
        {
            _cluster.Shutdown();
        }
    }

}
