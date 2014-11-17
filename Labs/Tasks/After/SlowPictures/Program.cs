using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading;

namespace SlowPictures
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new WebServiceHost(typeof(PictureService), new Uri("http://localhost:9000/pictures"));

            host.Open();

            Console.WriteLine("Service Ready ...");
            Console.ReadLine();

            host.Close();
            
        }
    }

    [ServiceContract]
    interface IGetPictures
    {
        [OperationContract]
        [WebGet(UriTemplate="picture")]
        Stream GetPicture();
    }

    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single,
        ConcurrencyMode=ConcurrencyMode.Multiple)]
    class PictureService : IGetPictures
    {
        List<string> pictures = new List<string>();
        Random rnd = new Random();

        public PictureService()
        {
            DirectoryInfo dir = new DirectoryInfo(@"..\..\images");
            pictures.AddRange(dir.GetFiles().Select(fi => fi.FullName));
        }

        public Stream GetPicture()
        {
            string filename = pictures[rnd.Next(pictures.Count)];


            Console.WriteLine("Picture requested - returning {0}", filename);

            Thread.Sleep(rnd.Next(10000));

            FileStream fs = File.OpenRead(filename);

            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";

            return fs;
        }
    }
}
