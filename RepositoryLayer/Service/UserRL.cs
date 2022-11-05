using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        //data of registered Dependency it
        private readonly IConfiguration iconfiguration;
        public static string Key = "Imran*84";
        public UserRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }
        public static UserEntity userEntity = new UserEntity();
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password= Encrypt(userRegistrationModel.Password);

                fundooContext.Usertable.Add(userEntity);

                int result = fundooContext.SaveChanges();
                
                if(result !=0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static string Encrypt(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return "";
                }
                else
                {
                    password += Key;
                    var passwordBytes = Encoding.UTF8.GetBytes(password);
                    return Convert.ToBase64String(passwordBytes);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static string Decrypt(string base64EncodeData)
        {
            try
            {
                if (string.IsNullOrEmpty(base64EncodeData))
                {
                    return "";
                }
                else
                {
                    var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
                    var result = Encoding.UTF8.GetString(base64EncodeBytes);
                    result = result.Substring(0, result.Length - Key.Length);
                    return result;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                var resultLog = fundooContext.Usertable.Where(x => x.Email == userLoginModel.Email && x.Password == Encrypt(userLoginModel.Password)).FirstOrDefault();
                //var decrptPassword = Decrypt(resultLog.Password);
                if (resultLog != null && Decrypt(resultLog.Password) == userLoginModel.Password)
                {
                    //userLoginModel.Email = resultLog.Email;
                    //userLoginModel.Password = resultLog.Password;
                    var token = GenerateSecurityToken(resultLog.Email, resultLog.UserId);
                    return token;
                    //return userLoginModel;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public string GenerateSecurityToken(string email,long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.iconfiguration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        public string ForgetPassword(string email)
        {
            try
            {
                var emailCheck = fundooContext.Usertable.FirstOrDefault(x=> x.Email == email);
                if(emailCheck != null)
                {
                    var token = GenerateSecurityToken(emailCheck.Email, emailCheck.UserId);
                    MSMQ mSMQ = new MSMQ();
                    mSMQ.sendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public UserEntity ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                if(Encrypt(newPassword) == Encrypt(confirmPassword))
                {
                    var user = fundooContext.Usertable.FirstOrDefault(x=>x.Email == email);
                    user.Password = Encrypt(newPassword);
                    fundooContext.SaveChanges();
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
