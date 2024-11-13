﻿using DvdShop.Database;
using DvdShop.DTOs.Requests;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Repositorys
{
    
    public class UserRepository : IUserRepository
    {
        private readonly DvdStoreContext _context;

        public UserRepository(DvdStoreContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(RegisterDTO registerDTO)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                IsConfirmed = false
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddVerificationOTPAsync(string email, string otp)
        {
            var user = await GetUserByEmailAsync(email);
            var otpEntity = new OTP
            {
                UserId = user.Id,
                Type = "EmailVerification",
                Code = otp,
                ExpiryDate = DateTime.UtcNow.AddMinutes(15)
            };
            _context.OTPs.Add(otpEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateOTPAsync(string email, string otp)
        {
            var user = await GetUserByEmailAsync(email);
            var otpEntity = await _context.OTPs
                .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Code == otp && o.ExpiryDate > DateTime.UtcNow);
            return otpEntity != null;
        }
    }

}
