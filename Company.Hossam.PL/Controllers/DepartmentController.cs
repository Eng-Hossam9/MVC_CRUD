using Company.Hossam.BLL.InterFaces;
using Company.Hossam.BLL.Repositories;
using Company.Hossam.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Company.Hossam.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var AllDepaprtment = _departmentRepository.GetAll();

            return View(AllDepaprtment);
        }

        public IActionResult Create() 
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Departments department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _departmentRepository.Add(department);
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(department);


        }



        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var department = _departmentRepository.DeletebYiD(id);
            if (department > 0)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();

        }





        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest();
            var dept = _departmentRepository.GetSpacificById(id);

            return View(dept);
        }



        public IActionResult Update(int? id)
        {

            if (id == null) return BadRequest();

            var department = _departmentRepository.GetSpacificById(id);

            if (department is null)
            {
                return NotFound();
            }

            return View(department);
        }


        [HttpPost]
        public IActionResult Update(Departments department)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = _departmentRepository.Update(department);
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }

            }
            catch (Exception ex) {

                Console.WriteLine(ex.Message);
            }


            return View(department);


        }


    }
}
