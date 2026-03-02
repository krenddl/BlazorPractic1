namespace AuthApi.Requests
{
    public class UpdateUser
    {
        public int id_User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role_id { get; set; }
    }
}
