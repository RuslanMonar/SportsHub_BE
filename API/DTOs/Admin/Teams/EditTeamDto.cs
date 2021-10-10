namespace API.DTOs.Admin.Teams
{
    public class EditTeamDto
    {
        public int Id { get; set; }
        public string CurrentLocation { get; set; }
        public string NewLocation { get; set; }
        public string CurrentCategory { get; set; }
        public string NewCategory { get; set; }
        public string CurrentSubCategory { get; set; }
        public string NewSubCategory { get; set; }
        public string CurrentName { get; set; }
        public string NewName { get; set; }
    }
        
}