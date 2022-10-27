using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        //data of registered Dependency it
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public static UserEntity userEntity = new UserEntity();
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                //UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password = userRegistrationModel.Password;

                fundooContext.Usertable.Add(userEntity);

                int result = fundooContext.SaveChanges();

                if (result != 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                var resultLog = fundooContext.Usertable.Where(x => x.Email == userLoginModel.Email && x.Password == userLoginModel.Password).FirstOrDefault();

                if (resultLog != null)
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
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
