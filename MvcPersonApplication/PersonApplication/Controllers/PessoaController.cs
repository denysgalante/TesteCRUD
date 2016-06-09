using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonApplication.PersonReference;

namespace CRUD_XML_MVC.Controllers
{
    public class PessoaController : Controller
    {
        private IPersonService _repository;

        public PessoaController(): this(new PersonServiceClient())
        {
        }

        public PessoaController(IPersonService repository)
        {
            _repository = repository;
        }


        public ActionResult Index()
        {
            return View(_repository.Get());
        }


        public ActionResult Details(int id)
        {            
            Person person = _repository.GetByID(id);
            if (person == null)
                return RedirectToAction("Index");

            return View(person);
        }


        public ActionResult Create()
        {
            return View();
        } 


        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Insert(person);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    //error msg for failed insert in XML file
                    ModelState.AddModelError("", "Error creating record. " + ex.Message);
                }
            }

            return View(person);
        }

 
        public ActionResult Edit(int id)
        {
            Person person = _repository.GetByID(id);
            if (person == null)
                return RedirectToAction("Index");

            return View(person);
        }


        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Edit(person);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed edit in XML file
                    ModelState.AddModelError("", "Error editing record. " + ex.Message);
                }
            }

            return View(person);
        }

 
        public ActionResult Delete(int id)
        {
            Person person = _repository.GetByID(id);
            if (person == null)
                return RedirectToAction("Index");
            return View(person);
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _repository.Delete(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                //error msg for failed delete in XML file
                ViewBag.ErrorMsg = "Error deleting record. " + ex.Message;
                return View(_repository.GetByID(id));
            }
        }
    }
}
