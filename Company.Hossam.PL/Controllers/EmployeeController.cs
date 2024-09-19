using AutoMapper;
using Company.Hossam.BLL.InterFaces;
using Company.Hossam.BLL.Repositories;
using Company.Hossam.DAL.Model;
using Company.Hossam.PL.Helper;
using Company.Hossam.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Company.Hossam.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IDepartmentRepository _DepartmentRepository;
        private readonly IMapper _Mapper;

        public EmployeeController(IEmployeeRepository EmployeeRepository, IDepartmentRepository DepartmentRepository,IMapper mapper) 
        {
            _EmployeeRepository = EmployeeRepository;
            _DepartmentRepository = DepartmentRepository;
            _Mapper = mapper;
        }


        public IActionResult Index(string searchInput ="")
        {
            var Employee = Enumerable.Empty<Employees>();

            if (searchInput.IsNullOrEmpty())
            {
                Employee = _EmployeeRepository.GetAll();    

            }
            else
            {

                 Employee = _EmployeeRepository.SearchByName(searchInput);
            }
            var EmployeeViewModel=_Mapper.Map<IEnumerable<EmployeeViewModel>>(Employee);

            return View(EmployeeViewModel);
        }

        public IActionResult Create()
        {
            var departments = _DepartmentRepository.GetAll();

            ViewData["Departments"]=departments;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel EmployeeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (EmployeeViewModel.Image != null)
                    {
                        EmployeeViewModel.ImageName = Document.UploadFile(EmployeeViewModel.Image, "Images");
                    }
                    else
                    {
                        EmployeeViewModel.ImageName = null;
                    }



                    var Employee = _Mapper.Map<Employees>(EmployeeViewModel);
                    
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

            return View(EmployeeViewModel);


        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var employee=_EmployeeRepository.GetSpacificById(id);

            var count = _EmployeeRepository.DeletebYiD(id);
            
            if (count > 0)
            {
                Document.DeleteFile(employee.ImageName, "Images");
                return RedirectToAction("Index");
            }

            return BadRequest();

        }





        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest();
            var Employee = _EmployeeRepository.GetSpacificById(id);

            var employeeViewModel = _Mapper.Map<EmployeeViewModel>(Employee);

            return View(employeeViewModel);
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
            var EmployeeViewModel = _Mapper.Map<EmployeeViewModel>(Employee);
           

            return View(EmployeeViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(EmployeeViewModel EmployeeViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (EmployeeViewModel.Image is not null)
                    {

                        if (EmployeeViewModel.ImageName is not null)
                        {
                            Document.DeleteFile(EmployeeViewModel.ImageName, "Images");
                        }

                        EmployeeViewModel.ImageName = Document.UploadFile(EmployeeViewModel.Image, "Images");

                    }
                    else
                    {
                        EmployeeViewModel.ImageName = EmployeeViewModel.ImageName;

                    }


                    var Employee = _Mapper.Map<Employees>(EmployeeViewModel);
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


            return View(EmployeeViewModel);


        }
    }
}
