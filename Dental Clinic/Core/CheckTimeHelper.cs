using Dental_Clinic.Models;
using Microsoft.EntityFrameworkCore.Update;
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
            
            return CheckTime(Record);
        }


        private static string CheckTime(EntryFormModel Record)
        {
            if (Record.StartOfProcedure <= DateTime.Now)
            {
                return "You can't put a record in the past \n choose another time";
            }

            if (Record.StartOfProcedure.Hour < 6 || Record.EndOfProcedure.Hour > 20)
            {
                return "We work from 6 to 8 \n choose another time";
            }

            if (Record.StartOfProcedure.DayOfWeek == DayOfWeek.Sunday)
            {
                return "We don't work on Sunday \n choose another day";
            }

            return Comparison_With_Time_Of_Other_Users(Record);
        }

        private static string Comparison_With_Time_Of_Other_Users(EntryFormModel user)
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

            if(ResultOfComparison)
            {
                return null;
            }
            return "Time isn't available \n choose another time";
            
        }

        private static bool Comparison_Of_Time_Gaps(EntryFormModel userFromDataBase, EntryFormModel user)
        {
            return ((user.StartOfProcedure < userFromDataBase.StartOfProcedure && user.EndOfProcedure <= userFromDataBase.StartOfProcedure) || user.StartOfProcedure >= userFromDataBase.EndOfProcedure);
        }
    }
}
