﻿using Azure;
using Luman.Busines.DTOs.UserDTO;
using Luman.Busines.Utility;
using Luman.DataLayer.Context;
using Luman.DataLayer.EntityModel.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Services.UserService
{
    public class UserServices : IUserServices
    {

        private readonly LumanContext _context;
        private readonly JwtSettings _Settings;


        public UserServices(LumanContext context, IOptions<JwtSettings> Settings)
        {
            _context = context;
            _Settings = Settings.Value;
        }

        public void ChangePassword(string username, string newPass)
        {
            var user = GetUserByUserName(username);
            user.Password = PasswordHelper.EncodePasswordMd5(newPass);
            UpdateUser(user);
        }

        public bool ComparePassword(string oldpass, string username)
        {
            var hashOldpass = PasswordHelper.EncodePasswordMd5(oldpass);
            _context.users.Any(u => u.UserName == username && u.Password == hashOldpass);
            return true;
        }

        public bool CreateUser(User user)
        {

            _context.users.Add(user);
            _context.SaveChanges();
            return true;


        }

        public bool CreateUserForAdmin(CreateUserDTO user)
        {
            User addUser = new()
            {
                UserName = user.UserName,
                Password = PasswordHelper.EncodePasswordMd5(user.Password),
                Email = user.Email,
                Name = user.UserName,
                Family = user.Family,
                CreateDate = DateTime.Now,
                IsDelete = false,
            };
            return CreateUser(addUser);
        }

        public bool DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            _context.users.Remove(user);
            return Save();
        }

        public User GetUserByUsername(string username) =>
             _context.users.SingleOrDefault(u => u.UserName == username);
        public User GetUserByUserName(string username)
        {
            return _context.users.FirstOrDefault(u => u.UserName == username);
        }

        public int GetUserIdByUserName(string username)
        {
            return _context.users.FirstOrDefault(u => u.UserName == username).UserId;
        }

        public InformationUserPanel GetUserInformation(string username)
        {
            var user = GetUserByUserName(username);

            InformationUserPanel panel = new InformationUserPanel();
            panel.Name = user.Name;
            panel.Family = user.Family;
            panel.Email = user.Email;
            panel.UserName = user.UserName;
            panel.CreateDate = user.CreateDate;

            return panel;
        }

        public UserForAdminDTO GetUsersForAdmin(int pageid = 1, string fillterEmail = "", string fillterUsername = "")
        {
            IQueryable<User> result = _context.users;

            if (!string.IsNullOrEmpty(fillterEmail))
            {
                result = result.Where(u => u.Email.Contains(fillterEmail));
            }
            if (!string.IsNullOrEmpty(fillterUsername))
            {
                result = result.Where(u => u.UserName.Contains(fillterUsername));
            }

            int take = 10;
            int skip = (pageid - 1) * take;


            UserForAdminDTO list = new UserForAdminDTO();
            list.CurrentPage = pageid;
            list.PageCount = result.Count() / take;

            list.users = result.OrderBy(u => u.CreateDate).Skip(skip).Take(take).ToList();



            return list;
        }

        public bool IsCorrectpass(string username, string pass)
        {

            var hashPass = PasswordHelper.EncodePasswordMd5(pass.Trim());
            _context.users.Any(u => u.UserName.Trim() == username.Trim() && u.Password == hashPass);
            return true;


        }

        public bool IsExsitEmail(string email)
        {

            _context.users.Any(u => u.Email == email);
            return true;

        }

        public bool IsExsitUserName(string username)
        {


            _context.users.Any(u => u.UserName == username);
            return true;

        }

        public User LoginUser(string username, string pass)
        {
            var hashPass = PasswordHelper.EncodePasswordMd5(pass);
            var user = _context.users.SingleOrDefault(c => c.UserName == username && c.Password == hashPass);

            if (user == null) { return null; }

            var key = Encoding.ASCII.GetBytes(_Settings.Secret);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name , user.UserName.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // ذخیره userId در توکن
                    new Claim(ClaimTypes.Email, user.Email.ToString()) // ذخیره userId در توکن

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _Settings.Issure,
                Audience = _Settings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            user.JwtSecret = tokenHandler.WriteToken(token);
            return user;
        }

        public bool Save() =>
            _context.SaveChanges() >= 0 ? true : false;


        public bool UpdateUser(User user)
        {

            _context.Update(user);
            Save();
            return true;


        }
        public void KarbareAdi(User user)
        {

            if (IsExsitEmail(user.Email) || IsExsitUserName(user.UserName))
            {
                UserRole role = new UserRole()
                {
                    UserId = user.UserId,
                    RoleId = 2

                };

                _context.Add(role);
                Save();

            }

        }

        public User GetUserById(int userid)
        {
            return _context.users.Find(userid);
        }

        public bool IsExsitUserById(int userid)
        {
            _context.users.Any(u=>u.UserId == userid);
            return true;
        }
    }
}
