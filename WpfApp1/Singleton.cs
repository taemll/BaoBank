using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Singleton
    {
        private static Singleton instance;
        public Singleton()
        { }
        public string TBLastName = "";
        public Singleton Getin()
        {
            if (instance == null)
                instance = new Singleton();
            return instance;
        }
    }
}
