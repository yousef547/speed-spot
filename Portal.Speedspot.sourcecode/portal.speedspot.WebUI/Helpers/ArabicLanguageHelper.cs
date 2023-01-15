using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Helpers
{
    public class ArabicLanguageHelper
    {
        public string GetDayStringByNumber(int DaysNo)
        {
            string arabicWord = "";
            switch (DaysNo)
            {
                case > 10:
                    arabicWord = DaysNo + " يوم";
                    break;
                case > 2:
                    arabicWord = DaysNo + " أيام";
                    break;
                case 2:
                    arabicWord = "يومين";
                    break;
                case 1:
                    arabicWord = "يوم";
                    break;
            }

            return arabicWord;
        }
    }
}
