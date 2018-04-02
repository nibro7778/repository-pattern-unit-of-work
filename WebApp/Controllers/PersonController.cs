
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Entity;
using Service;
using Service.Mapping;

namespace WebApp.Controllers
{
    public class PersonController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Person> personRepository;

        public PersonController()
        {
            personRepository = unitOfWork.Repository<Person>();
        }
        
        // GET: List of all Person
        public ActionResult Index()
        {
            IEnumerable<Person> person = personRepository.Table.ToList();
            return View(person);
        }

        public ActionResult CreateEditPerson(int? id)
        {
            Person person = new Person();
            if (id.HasValue)
            {
                person = personRepository.GetById(id);
            }

            return View(person);
        }

        [HttpPost]
        public ActionResult CreateEditPerson(Person person)
        {
            if (person.Id == 0)
            {
                person.CreatedBy = "Application";
                person.CreatedDate = DateTime.Now;
                person.ModifiedBy = "Application";
                person.ModifiedDate = DateTime.Now;
            }
            else
            {
                var editedPerson = personRepository.GetById(person.Id);
                person.Name = editedPerson.Name;
                person.City = editedPerson.City;
                person.EmailAddress = editedPerson.EmailAddress;
                person.ContactNumber = editedPerson.ContactNumber;
                person.Country = editedPerson.Country;
                personRepository.Update(person);
            }

            if (person.Id > 0)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public ActionResult DeletePerson(int id)
        {
            var deletedPerson = personRepository.GetById(id);
            return View(deletedPerson);
        }

        [HttpPost, ActionName("DeletePerson")]
        public ActionResult ConfirmDeletePerson(int id)
        {
            var deletePerson = personRepository.GetById(id);
            personRepository.Delete(deletePerson);
            return RedirectToAction("Index");
        }

        public ActionResult DetailPerson(int id)
        {
            var person = personRepository.GetById(id);
            return View(person);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}