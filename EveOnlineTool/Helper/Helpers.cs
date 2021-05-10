using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveOnlineTool.Helper
{
    public static class Helpers
    {
        public static int MilisecondsToMinutes(int miliseconds)
        {
            return miliseconds / 1000 / 60;
        }

        public static int MinutesToMiliseconds(int minutes)
        {
            return minutes * 60 * 1000;
        }
    }
}
