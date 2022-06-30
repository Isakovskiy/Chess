using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	[Serializable]
	public class Package
	{
		public string WhoGoing { get; set; }
		public string GameProgress { get; set; }
		public Packet[] Packets { get; set; }
		public Package(string whoGoing, string gameProgress, Packet[] packets)
		{
			WhoGoing = whoGoing;
			GameProgress = gameProgress;
			Packets = packets;
		}
	}
}
