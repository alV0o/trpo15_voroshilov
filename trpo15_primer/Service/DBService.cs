using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using trpo15_primer.Models;

namespace trpo15_primer.Service
{
    public class DBService
    {
        private Trpo15PrimerVoroshilovContext context;
        public Trpo15PrimerVoroshilovContext Context => context;
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
            context = new Trpo15PrimerVoroshilovContext();
        }
    }
}

