using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Threading;

class Generator
{
    public static event EventHandler Overheat;
    static Random rng = new Random();

    public static int GetTemperature()
    {
        lock (rng)
        {
            int temp = rng.Next(800);
            if (temp > 500 && Overheat != null)
                Overheat(null, EventArgs.Empty);

            return temp;
        }
    }

}

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Generator.Overheat += new EventHandler(OnOverheat);
        Label2.Text = "";

        Label1.Text = Generator.GetTemperature().ToString();

        Thread.Sleep(10);
    }

    void OnOverheat(object sender, EventArgs e)
    {
        Label2.Text = "Danger - device is overheating!";
    }
}
