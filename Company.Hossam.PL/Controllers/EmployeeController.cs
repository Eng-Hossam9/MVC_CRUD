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


        public async Task<IActionResult> Index(string searchInput ="")
        {
            var Employee = Enumerable.Empty<Employees>();

            if (searchInput.IsNullOrEmpty())
            {
                Employee =await _EmployeeRepository.GetAllAsync();    

            }
            else
            {

                 Employee =await _EmployeeRepository.SearchByNameAsync(searchInput);
            }
            var EmployeeViewModel=_Mapper.Map<IEnumerable<EmployeeViewModel>>(Employee);

            return View(EmployeeViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var departments =await _DepartmentRepository.GetAllAsync();

            ViewData["Departments"]=departments;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel EmployeeViewModel)
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
                    
                    var result = await _EmployeeRepository.AddAsync(Employee);
                    
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
        public async Task<IActionResult> Delete(int? id)
        {
            var employee=await _EmployeeRepository.GetSpacificByIdAsync(id);

            var count =await _EmployeeRepository.DeletebYiDAsync(id);
            
            if (count > 0)
            {
                Document.DeleteFile(employee.ImageName, "Images");
                return RedirectToAction("Index");
            }

            return BadRequest();

        }





        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            var Employee =await _EmployeeRepository.GetSpacificByIdAsync(id);

            var employeeViewModel = _Mapper.Map<EmployeeViewModel>(Employee);

            return View(employeeViewModel);
        }



        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var  Employee = await _EmployeeRepository.GetSpacificByIdAsync(id);
            var departments =await _DepartmentRepository.GetAllAsync();
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
        public async Task<IActionResult> Update(EmployeeViewModel EmployeeViewModel)
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
                    var result =await _EmployeeRepository.UpdateAsync( Employee);
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
