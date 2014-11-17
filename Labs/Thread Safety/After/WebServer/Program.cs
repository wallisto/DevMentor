using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using Fractals;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace WebServer
{
    class Program
    {
      
        private static Random rnd = new Random();

        private static BlockingCollection<HttpListenerContext> queue = new BlockingCollection<HttpListenerContext>( new ConcurrentQueue<HttpListenerContext>() );

        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();

            listener.Prefixes.Add("http://localhost:9001/Fractals/");

            listener.Start();

            for (int nTask = 0; nTask < 5; nTask++)
            {
                Task.Factory.StartNew(RequestHandler);
            }

            Console.WriteLine("Server up and Listening");

            while (true)
            {
                HttpListenerContext ctx = listener.GetContext();

                queue.Add(ctx);
            }
        }

        private static void RequestHandler()
        {
            foreach (HttpListenerContext ctx in queue.GetConsumingEnumerable())
            {
                Console.Write("Handling request...");
                if (ctx.Request.Url.OriginalString.Contains("time"))
                {
                    using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
                    {
                        writer.WriteLine("<i> {0} </i>", DateTime.Now);
                    }
                }
                else
                {
                    GenerateFractalImage(ctx);
                }

                ctx.Response.Close();
                Console.WriteLine("Done");
            }
        }
  

        private static void GenerateFractalImage( HttpListenerContext ctx)
        {
            ctx.Response.ContentType = "image/jpg";
            Fractal fractal = new Fractal();


            int realAxisStart = rnd.Next(0, 25);
            int realAxisEnd = realAxisStart + 25;
            int imaginaryStart = rnd.Next(25, 50);
            int imaginaryEnd = imaginaryStart + 25;

            Bitmap img = fractal.RenderFractal(
                realAxisStart / 100.0,
                realAxisEnd / 100.0,
                imaginaryStart / 100.0,
                imaginaryEnd / 100.0,
                new System.Drawing.Size(3200, 3200));

            img.Save(ctx.Response.OutputStream, ImageFormat.Jpeg);
        }
    }
}
