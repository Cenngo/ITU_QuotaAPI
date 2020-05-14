using ITU_QuotaAPI.Models;
using System.Linq;

namespace ITU_QuotaAPI
{
    /// <summary>
    /// Class that stores all of the information retrieved
    /// </summary>
    public class Lesson : ILesson
    {
        public int CRN { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Instructor { get; set; }
        public string Building { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Room { get; set; }
        public int Capacity { get; set; }
        public int Enrolled { get; set; }
        public string Reservations { get; set; }

        public string[] Restrictions { get; set; }
        public string Prerequisites { get; set; }
        public string ClassRestrictions { get; set; }

        /// <summary>
        /// Check the eligability of a major
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public bool IsEligible ( string major )
        {
            return Restrictions.Contains(major.ToUpper());
        }

        /// <summary>
        /// Check if there is any space in the lesson
        /// </summary>
        /// <returns></returns>
        public bool IsAvailable ( )
        {
            return Enrolled <= Capacity ? true : false;
        }
    }
}