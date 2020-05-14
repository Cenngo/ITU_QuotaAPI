namespace ITU_QuotaAPI.Models
{
    public interface ILesson
    {
        int CRN { get; set; }
        string Code { get; set; }
        string Day { get; set; }
        string Time { get; set; }
        int Capacity { get; set; }
        int Enrolled { get; set; }

        string[] Restrictions { get; set; }

        bool IsEligible ( string major );
    }
}