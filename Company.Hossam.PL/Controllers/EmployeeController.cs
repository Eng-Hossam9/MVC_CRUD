using Company.Hossam.BLL.InterFaces;
using Company.Hossam.BLL.Repositories;
using Company.Hossam.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace Company.Hossam.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IDepartmentRepository _DepartmentRepository;

        public EmployeeController(IEmployeeRepository EmployeeRepository, IDepartmentRepository DepartmentRepository) 
        {
            _EmployeeRepository = EmployeeRepository;
            _DepartmentRepository = DepartmentRepository;
        }
        public IActionResult Index()
        {
            var Employee = _EmployeeRepository.GetAll();

            return View(Employee);
        }

        public IActionResult Create()
        {
            var departments = _DepartmentRepository.GetAll();
            ViewData["Departments"]=departments;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employees Employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _EmployeeRepository.Add(Employee);
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(Employee);


        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var Employee = _EmployeeRepository.DeletebYiD(id);
            if (Employee > 0)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();

        }





        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest();
            var Employee = _EmployeeRepository.GetSpacificById(id);
           

            return View(Employee);
        }



        public IActionResult Update(int? id)
        {
            if (id == null) return BadRequest();

            var  Employee = _EmployeeRepository.GetSpacificById(id);
            var departments = _DepartmentRepository.GetAll();
            ViewData["Departments"] = departments;

            if (Employee is null)
            {
                return NotFound();
            }

            return View( Employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employees Employee)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = _EmployeeRepository.Update( Employee);
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return View(Employee);


        }
    }
}
