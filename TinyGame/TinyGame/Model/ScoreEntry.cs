using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TinyGame.Model
{
    [DataContract]
    public class ScoreEntry
    {
        [DataMember]
        public int DisplayScore { get; set; }
        [DataMember]
        public double Score { get; set; }
        [DataMember]
        public string PlayerName { get; set; }
    }
}
