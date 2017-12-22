using System;
namespace Tatbikat.Operations
{
    public partial class Connector
    {
        public static readonly Connector Current = new Connector();
        internal Client Client { get; private set; }

        public Connector()
        {
            Client = new Client();

        }
    }
}
