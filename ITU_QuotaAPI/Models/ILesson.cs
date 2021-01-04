namespace ITU_QuotaAPI.Models
{
    public interface ILesson
    {
        int CRN { get; }
        string Code { get; }
        string Day { get;}
        string Time { get; }
        int Capacity { get; }
        int Enrolled { get; }
        string[] Restrictions { get; }
        bool IsEligible ( string major );
    }
}