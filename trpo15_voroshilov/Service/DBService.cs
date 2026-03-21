using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using trpo15_voroshilov.Models;

namespace trpo15_voroshilov.Service
{
    public class DBService
    {
        private Trpo15VoroshilovContext context;
        public Trpo15VoroshilovContext Context => context;
        private static DBService? instance;
        public static DBService Instance
        {
            get 
            {
                if (instance == null)
                    instance = new DBService();
                return instance; 
            }
        }
        private DBService()
        {
            context = new Trpo15VoroshilovContext();
        }
    }
}
