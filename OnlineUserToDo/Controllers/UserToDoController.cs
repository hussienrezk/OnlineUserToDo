using Microsoft.AspNet.Identity;
using OnlineUserToDo.Entity;
using OnlineUserToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineUserToDo.Controllers
{
    public class UserToDoController : Controller
    {
        UserToDoModel _dbEntite;
        // GET: UserToDo
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UserToDo()
        {
            _dbEntite = new UserToDoModel();
            var UserId = User.Identity.GetUserId();
            var userlist = _dbEntite.UserToDoes.Where(h=> h.UserId==UserId && h.IsActive==true);
            var mynewlist = new List<mytodolist>();
            foreach (var item in userlist)
            {
                var newitem = new mytodolist()
                {
                    Id = item.Id,
                    Title = item.Title,
                    UserId = item.UserId,
                    DueDate = item.DueDate.Value.Date.ToString("dd MM yyyy"),
                    IsActive = item.IsActive,
                    Status = item.Status
                };
                mynewlist.Add(newitem);
            }
            return Json(new
            {
                // this is what datatables wants sending back
                // draw = model.draw,
                recordsTotal = mynewlist.Count,
                recordsFiltered = 1,
                data = mynewlist
            });
            //  return Json(mynewlist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddNewToDo()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult AddNewToDo(string title,string duedate)
        {
            try
            {
                var todo = new UserToDo()
                {
                    UserId = User.Identity.GetUserId(),
                    Title = title,
                    DueDate = Convert.ToDateTime(duedate),
                    Status =(int)ToDoStatus.incomplete,
                    IsActive = true
                };
                _dbEntite = new UserToDoModel();
                _dbEntite.UserToDoes.Add(todo);
                _dbEntite.SaveChanges();
                return Json("done", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
           
        }

        public JsonResult CompeleteToDo(int id)
        {
            try
            {
                _dbEntite = new UserToDoModel();
                var todo = _dbEntite.UserToDoes.FirstOrDefault(h => h.Id == id);
                if (todo!=null)
                {
                    todo.Status = (int)ToDoStatus.complete;
                    _dbEntite.SaveChanges();
                    return Json("done", JsonRequestBehavior.AllowGet);
                }
                return Json("No ToDo For This ID", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult DeleteToDo(int id)
        {
            try
            {
                _dbEntite = new UserToDoModel();
                var todo = _dbEntite.UserToDoes.FirstOrDefault(h => h.Id == id);
                if (todo != null)
                {
                    todo.IsActive = false;
                    _dbEntite.SaveChanges();
                    return Json("done", JsonRequestBehavior.AllowGet);
                }
                return Json("No ToDo For This ID", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult EditToDo(int id)
        {
            _dbEntite = new UserToDoModel();
            var todo = _dbEntite.UserToDoes.FirstOrDefault(h => h.Id == id);
            return PartialView(todo);
        }
        [HttpPost]
        public JsonResult EditToDo(int id,string title, string duedate)
        {
            try
            {
                _dbEntite = new UserToDoModel();
                var todo = _dbEntite.UserToDoes.FirstOrDefault(h => h.Id == id);
                todo.Title = title;
                todo.DueDate =Convert.ToDateTime(duedate);
                _dbEntite.SaveChanges();
                return Json("done", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

    }

    public class mytodolist
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string DueDate { get; set; }

        public int? Status { get; set; }

        public Nullable<bool> IsActive { get; set; }

    }
}