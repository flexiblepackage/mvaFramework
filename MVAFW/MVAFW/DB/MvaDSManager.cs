using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVAFW.DB
{
    internal class MvaDSManager
    {
        private static MvaDSMain mainDB;
        public static MvaDSMain MainDB 
        {
            get
            {
                if (mainDB == null)
                {
                    mainDB = new MvaDSMain();
                }
                return mainDB;
            }
        }
    }
}
