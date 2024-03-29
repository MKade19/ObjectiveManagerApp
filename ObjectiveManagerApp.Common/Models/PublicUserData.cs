﻿namespace ObjectiveManagerApp.Common.Models
{
    public class PublicUserData
    {
        public int Id { get; set; } = -1;
        public string Username { get; set; } = string.Empty;

        public string Fullname { get; set; } = string.Empty;
        public List<Role> Roles { get; set; }

        public PublicUserData(int id, string userName, string fullName, List<Role> roles)
        {
            Id = id;
            Username = userName;
            Fullname = fullName;
            Roles = roles;
        }

        public PublicUserData() { }

        public override string ToString()
        {
            return $"{Username} ({Fullname})";
        }
    }
}
