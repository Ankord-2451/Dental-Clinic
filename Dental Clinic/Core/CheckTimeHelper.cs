using Dental_Clinic.Models;
using System;
using System.Collections.Generic;

namespace Dental_Clinic.Core
{
    public static class CheckTimeHelper
    {
        static List<EntryFormModel> Records;
        public static String Check(EntryFormModel Record,int Hours,int Minutes, List<EntryFormModel> _Records)
        {
            Records = _Records;
  
            Record.EndOfProcedure = Record.StartOfProcedure.AddHours(Hours).AddMinutes(Minutes);
            if (CheckTime(Record))
            {
                return null;
            }
            else
            {
                return "Wrong time";
            }

        }


        private static bool CheckTime(EntryFormModel Record)
        {
            if (Record.StartOfProcedure <= DateTime.Now)
            {
                return false;
            }

            if (Record.StartOfProcedure.Hour < 6 || Record.StartOfProcedure.Hour > 20)
            {
                return false;
            }

            if (Record.StartOfProcedure.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            return Comparison_With_Time_Of_Other_Users(Record);
        }

        private static bool Comparison_With_Time_Of_Other_Users(EntryFormModel user)
        {
            bool ResultOfComparison = true;
            foreach (var item in Records)
            {
                if (item.Doctor == user.Doctor && item.StartOfProcedure.Year == user.StartOfProcedure.Year && item.StartOfProcedure.Month == user.StartOfProcedure.Month && item.StartOfProcedure.Day == user.StartOfProcedure.Day)
                {
                    ResultOfComparison = Comparison_Of_Time_Gaps(item, user);
                }

                if (!ResultOfComparison) break;
            }


            return ResultOfComparison;
        }

        private static bool Comparison_Of_Time_Gaps(EntryFormModel userFromDataBase, EntryFormModel user)
        {
            return ((user.StartOfProcedure < userFromDataBase.StartOfProcedure && user.EndOfProcedure <= userFromDataBase.StartOfProcedure) || user.StartOfProcedure >= userFromDataBase.EndOfProcedure);
        }
    }
}
