using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using WorldVision.BusinessLogic.DBModel;
using WorldVision.BussinesLogic.DBModel;
using WorldVision.BussinesLogic.Interfaces;
using WorldVision.Domain.Entities.Images;
using WorldVision.Domain.Entities.User;
using WorldVision.Helpers;

namespace WorldVision.BussinesLogic.Core
{

    public class UserApi
    {
        internal ULoginResp UserRegisterAction(URegisterData data)
        {
            UDbTable user = new UDbTable
            {
                Username = data.Credential,
                Id = 1,
                Email = data.Email,
                Password = data.Password,
                LasIp = data.LoginIp,
                LastLogin = data.LoginDataTime,
                Level = Domain.Enums.URole.User

            };
            using (var db = new UserContext())
            {
                //user = db.Users.FirstOrDefault(u => u.Username == data.Credential);
                db.Users.Add(user);
                db.SaveChanges();
            }
            return new ULoginResp { Status = true };
        }


        internal ULoginResp UserLoginAction(ULoginData data)
        {
            UDbTable result;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Credetial))
            {
                /*var pass = LoginHelper.HashGen(data.Password);*/
                var pass = data.Password;
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Email == data.Credetial && u.Password == pass);
                    Console.WriteLine(result);
                }

                if (result == null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDataTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new ULoginResp { Status = true };
            }
            else
            {
                /*var pass = LoginHelper.HashGen(data.Password);*/
                var pass = data.Password;
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Username == data.Credetial && u.Password == pass);
                }

                if (result == null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDataTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new ULoginResp { Status = true };
            }
        }

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable currentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    currentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    currentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }

                if (currentUser == null) return null;
                UserMinimal userminimal = new UserMinimal()
                {
                    Username = currentUser.Username,
                    LasIp = currentUser.LasIp,
                    Id = currentUser.Id,
                    Email = currentUser.Email,
                    Level = currentUser.Level,
                    LastLogin = currentUser.LastLogin
                };

                return userminimal;
            }
        }

        internal List<Image> GetGalerieDataApi()
        {
            List<IDbTable> img_list;
            var local = new List<Image>();
            using (ImageContext db = new ImageContext())
            {
                img_list = db.Images.ToList();
            }

            foreach (var img in img_list)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDbTable, Image>());
                var img_min = Mapper.Map<Image>(img);
                local.Add(img_min);
            }

            return local;
        }

        internal void DeleteImageAction(int Imageid)
        {
            IDbTable image;

            using (var db = new ImageContext())    //delete product data from Product table in database
            {
                image = db.Images.FirstOrDefault(m => m.ImageID == Imageid);
                //System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath("~/") + product.PreImgPath));
                db.Images.Remove(image);
                db.SaveChanges();
            }

        }
    }
}
