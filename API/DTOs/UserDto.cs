namespace API.DTOs
{
    public class UserDto
    {
        //info that returns to the user after successfull login/refister
        public string Username { get; set; }

        public string Token { get; set; }

        public string Image { get; set; }

    }
}