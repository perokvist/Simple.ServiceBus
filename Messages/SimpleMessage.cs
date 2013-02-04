using System;
using System.Runtime.Serialization;

namespace Messages
{
    [DataContract]
    public class SimpleMessage
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public DateTime DateTime { get; set; }
    }
}