using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace DatastaxDriverBug
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleClient client = new SimpleClient();
            client.Connect("127.0.0.1");
            DateTimeOffset eventTime = DateTimeOffset.Parse("2012-09-28T20:12:33.4649999Z");
            var session = client.Cluster.Connect("sample");

            var insert = session.Prepare("insert into events (event_creator, event_id, note) values (?, ?, ?)");
            var bound = insert.Bind("Me", TimeUuid.NewId(eventTime), "This is a note").SetConsistencyLevel(ConsistencyLevel.Quorum);
            session.Execute(bound);

            var select =
                session.Prepare(
                    "select * from events where event_creator = :creator AND event_id >= minTimeuuid(:timestamp) AND event_id <= maxTimeuuid(:timestamp)");
            var boundSelect = select.Bind(new {creator = "Me", timestamp = eventTime}).SetConsistencyLevel(ConsistencyLevel.Quorum);
            var results = session.Execute(boundSelect);
            Console.Out.WriteLine("Result count:{0}", results.Count());
            Console.ReadLine();
        }
    }
}
