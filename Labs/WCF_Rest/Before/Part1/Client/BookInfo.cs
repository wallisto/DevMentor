using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Client
{
    [DataContract(Namespace = "http://develop.com")]
    class BookInfo
    {
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Topic { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public string Isbn { get; set; }
        [DataMember]
        public string[] Authors { get; set; }
        [DataMember]
        public string WebSite { get; set; }

        public string AuthorsString
        {
            get { return string.Join(", ", this.Authors); }
        }
	
        public static BookInfo Parse(string xmlStr)
        {
            // Return if null
            if (xmlStr == null) return null;

            DataContractSerializer ser = new DataContractSerializer(typeof(BookInfo));
            byte[] data = ASCIIEncoding.ASCII.GetBytes(xmlStr);
            using (MemoryStream ms = new MemoryStream(data))
            {
                return (BookInfo)ser.ReadObject(ms);
            }
        }
    }
}
