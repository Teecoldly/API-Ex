using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Common.Service.DbFunc;
using System.Dynamic;
using API.Entity;
using System.Collections.Generic;
using Common.Service.Utility;
using System.Linq;
using Commons.Extention;
using AutoMapper;
using Commons.Profile.Enity;
using Commons.Service;
using Data;

namespace API.App.Service
{
    public class UserService
    {
        public readonly IMapper Mapper;

        public UserService(IMapper _mapper)
        {
            Mapper = _mapper;
        }
        public static dynamic TokenAuthentication(string token)
        {

            var result = ValidateJwtToken(token);
            if (result == null)
            {
                return new
                {
                    status = 401,
                    Message = "กรุณาเข้าสู่ระบบอีกครั้ง"
                };
            }
            else
            {
                DbFunc db = new DbFunc();
                string sql = "select userid from Users where token = @token";
                List<SqlParam> sqlParam = new List<SqlParam>();
                sqlParam = new List<SqlParam>();
                sqlParam.Add(new SqlParam() { name = "@token", value = token });
                dynamic data = db.GetData(sql, sqlParam);
                if (UtilityFunc.ObjHasProp(data, "userid"))
                {
                    db.Close();
                    return new
                    {
                        status = 200,
                        id = data.userid
                    };

                }
                else
                {
                    return new
                    {
                        status = 401,
                        Message = "กรุณาเข้าสู่ระบบอีกครั้ง (data)"
                    };
                }
            }

        }

        public static dynamic login(Authentication u)
        {
            DbFunc db = new DbFunc();

            List<SqlParam> sqlParam = new List<SqlParam>();
            string sql = "select userid,name from Users where username = @username and password = @password";
            sqlParam = new List<SqlParam>();
            sqlParam.Add(new SqlParam() { name = "@username", value = u.username });
            sqlParam.Add(new SqlParam() { name = "@password", value = u.passsword });
            dynamic data = db.GetData(sql, sqlParam);

            if (!UtilityFunc.ObjHasProp(data, "userid"))
            {
                db.Close();
                return new
                {
                    status = 400,
                    Message = "ไม่พบ Username หรือ Password ",


                };
            }
            else
            {
                var token = GenerateJwtToken(data.userid);
                sqlParam = new List<SqlParam>();
                sql = "UPDATE `Users` SET  `token`=@token WHERE `userid` =@userid";
                sqlParam.Add(new SqlParam() { name = "@token", value = token });
                sqlParam.Add(new SqlParam() { name = "@userid", value = data.userid });
                db.ExecNotQuery(sql, sqlParam);
                User userinfo = new User();
                userinfo.name = data.name;
                userinfo.token = token;
                dynamic final = new ExpandoObject();
                final.status = 200;
                final.user = userinfo;
                db.Close();
                return final;
            }


        }
        public static string GenerateJwtToken(int UserId)
        {
            var configuation = DbFunc.GetConfiguration();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuation.GetSection("AppSettings").GetSection("Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static int? ValidateJwtToken(string token)
        {
            try
            {

                var configuation = DbFunc.GetConfiguration();
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuation.GetSection("AppSettings").GetSection("Secret").Value);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                return UtilityFunc.ParseInt(jwtToken.Claims.First(x => x.Type == "id").Value);

            }
            catch
            {
                return null;
            }


        }
        public dynamic GetAllByEf()
        {

            var result = new DB<Users>(Mapper).Get().QueryPage<UsersEntity>(1, 1, null);
            return result;


        }
    }
}
