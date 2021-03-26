using System;
using System.Collections.Generic;
using System.Text;
using Extensions;

namespace BusinessLogic
{
    public static class Application
    {
        private static ToursManager manager;

        public static ToursManager GetToursManager()
        {
            if (manager.IsNull())
                manager = new ToursManager();

            return manager;
        }
    }
}
