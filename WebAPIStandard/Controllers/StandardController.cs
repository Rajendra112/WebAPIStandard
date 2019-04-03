using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIStandard.Models;
namespace WebAPIStandard.Controllers
{
    /// <summary>
    /// This is controller name
    /// </summary>
    public class StandardController : ApiController
    {
        /// <summary>
        /// To list all the persons in table in Summary Section
        /// </summary>
        /// <remarks>To list all the persons in table in Remarks Section</remarks>
        public IHttpActionResult getAllPersons()
        {
            using (TestEntities ctx = new TestEntities()) //change from master
            {
                var person = ctx.People.ToList();
                if (person != null)
                {
                    return Ok(person);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        /// <summary>
        /// To show only person with matching ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult getAllPersons(int id)
        {
            using(TestEntities ctx = new TestEntities())
            {
                Person person = ctx.People.Where(x=>x.Id == id).FirstOrDefault();
                if(person != null)
                {
                    return Ok(person);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        /// <summary>
        /// It takes person object for new record
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public IHttpActionResult postNewPerson(Person per)
        {
            if(ModelState.IsValid)
            {
                using(TestEntities ctx = new TestEntities())
                {
                    ctx.People.Add(new Person { Age = per.Age, DOB = DateTime.Now, Name = per.Name });
                    ctx.SaveChanges();
                    return Ok();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// It takes person object to udpate existing record
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public IHttpActionResult putPerson(Person per)
        {
            if (ModelState.IsValid)
            {
                using(TestEntities ctx = new TestEntities())
                {
                    per.DOB = DateTime.Now;
                    ctx.Entry(per).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                    return Ok();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// To delete a record by sending its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult deletePerson(int id)
        {
            using (TestEntities ctx = new TestEntities())
            {
                Person perToDelete = ctx.People.Where(x => x.Id == id).FirstOrDefault();
                if(perToDelete != null)
                {
                    ctx.People.Remove(perToDelete);
                    ctx.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            } 
        }
    }
}
