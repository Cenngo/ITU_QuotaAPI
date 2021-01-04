using ITU_QuotaAPI.Models;
using System.Linq;

namespace ITU_QuotaAPI
{
    /// <summary>
    /// Class that stores all of the information retrieved
    /// </summary>
    public class Lesson : ILesson
    {
        public int CRN { get; internal set; }
        public string Code { get; internal set; }
        public string Title { get; internal set; }
        public string Instructor { get; internal set; }
        public string Building { get; internal set; }
        public string Day { get; internal set; }
        public string Time { get; internal set; }
        public string Room { get; internal set; }
        public int Capacity { get; internal set; }
        public int Enrolled { get; internal set; }
        public string Reservations { get; internal set; }

        public string[] Restrictions { get; internal set; }
        public string Prerequisites { get; internal set; }
        public string ClassRestrictions { get; internal set; }

        /// <summary>
        /// Check the eligability of a major
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public bool IsEligible ( string major ) =>
            Restrictions.Contains(major.ToUpper());

        /// <summary>
        /// Check if there is any space in the lesson
        /// </summary>
        /// <returns></returns>
        public bool IsAvailable ( ) =>
            Enrolled <= Capacity;
    }
}