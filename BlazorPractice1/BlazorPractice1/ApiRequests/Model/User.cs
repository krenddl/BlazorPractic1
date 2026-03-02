namespace BlazorPractice1.ApiRequests.Model
{
    public class User
    {
        public int id_User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role_Id { get; set; }
        public Role Role { get; set; }
    }
    public class StatusRegResponse
    {
        public bool status {  get; set; }
    }
    public class AuthorizeResponse
    {
        public bool status { get; set; }
        public User user { get; set; }
    }
    public class UsersListResponse
    {
        public bool status { get; set; }
        public List<User> user { get; set; }
    }
    public class AddNewUserRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public int Role_id { get; set; }
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserRequest
    {
        public int id_User { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public int Role_id { get; set; }
    }
}
