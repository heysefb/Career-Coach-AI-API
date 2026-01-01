namespace KariyerKocuAPI.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CvFilePath { get; set; }
        public string AiComment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PhoneNumber { get; set; }

    }
}