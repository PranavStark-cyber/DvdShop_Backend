﻿namespace DvdShop.DTOs.Requests
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Nic { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }

}