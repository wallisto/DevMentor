using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace IoC
{
    public class Container
    {
        private static Container container;

        private Container()
        {
            UnityConfigurationSection section = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
            if (section != null)
            {
                section.Configure(unityContainer);
            }
        }

        readonly IUnityContainer unityContainer = new UnityContainer();

        public static IUnityContainer Instance
        {
            get
            {
                if (container == null)
                {
                    container = new Container();
                }
                return container.unityContainer;
            }
        }
    }
}
