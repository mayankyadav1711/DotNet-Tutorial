﻿namespace Books
{
    public class User
    {
        public int Id { get; set; } // Add Id property for database key
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
    }
}
