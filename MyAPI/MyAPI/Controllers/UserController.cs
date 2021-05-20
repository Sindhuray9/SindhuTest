using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyDBA;


namespace MyAPI.Controllers
{
    public class UserController : ApiController
    {

        public IHttpActionResult Get()
        {
            try
            {
                using (ABCompanyEntities entities = new ABCompanyEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;

                    var entity = entities.tblUsers.ToList();
                    if (entity != null && entity.Count>0)
                    {
                        return Ok(entity);
                     
                    }
                    else
                    {
                        return Ok<string> ("No record in table");
                    }
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
          
        }

        public IHttpActionResult Get(int id)
        {
            using (ABCompanyEntities entities = new ABCompanyEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                try
                {
                  var entity = entities.tblUsers.FirstOrDefault(p => p.ID == id);
                    if (entity != null)
                    {
                        return Ok(entity);

                    }
                    else
                    {
                        return Ok<string>("No record in table for id " + id);
                    }
                    
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
               
            }
        }

         
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (ABCompanyEntities entities = new ABCompanyEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var empdelete = entities.tblUsers.FirstOrDefault(p => p.ID == id);
                    if (empdelete != null)
                    {
                        entities.tblUsers.Remove(empdelete);
                        entities.SaveChanges();
                        return Ok<string>("Record is deleted for id " + id);
                    }
                    else
                    {
                        return Ok<string>("No record in table for id " + id);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        public IHttpActionResult Post([FromBody] tblUser emp)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Data is not valid please recheck!");
                }

                using (ABCompanyEntities entities = new ABCompanyEntities())
                {
                    entities.tblUsers.Add(emp);
                    var output =   entities.SaveChanges();                   
                    if (output == 1)
                    {
                        return Ok<string>("Data Saved Succcessfully");
                        
                    }
                    else
                    {
                        return Ok<string>("Data Not Saved ");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }

        public IHttpActionResult put(tblUser user)
        {
            try
            {
                using (ABCompanyEntities entities = new ABCompanyEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;

                    var userUpdate = entities.tblUsers.Where(s => s.ID == user.ID).FirstOrDefault<tblUser>();
                    
                    if (userUpdate != null)
                    {
                        userUpdate.Name = user.Name;
                        userUpdate.PhoneNumber = user.PhoneNumber;
                        userUpdate.Email = user.Email;
                        userUpdate.Address = user.Address;
                        entities.SaveChanges();
                        return Ok<string>("Data Updated Succcessfully");
                    }
                    else
                    {
                        return Ok<string>("Data Not Updated ");
                    }


                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
