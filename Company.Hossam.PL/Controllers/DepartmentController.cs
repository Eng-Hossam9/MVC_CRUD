using AutoMapper;
using Company.Hossam.BLL.InterFaces;
using Company.Hossam.BLL.Repositories;
using Company.Hossam.DAL.Model;
using Company.Hossam.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Company.Hossam.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _Mapper;

        public DepartmentController(IDepartmentRepository departmentRepository,IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _Mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var AllDepaprtment = await _departmentRepository.GetAllAsync();
            var deparmentViewModel = _Mapper.Map<IEnumerable< DepartmentViewModel>>(AllDepaprtment) ;

            return View(deparmentViewModel);
        }

        public IActionResult Create() 
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel DepartmentViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var department = _Mapper.Map<Departments>(DepartmentViewModel);
                    var result = await _departmentRepository.AddAsync(department);
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

            return View(DepartmentViewModel);


        }



        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            var department =await _departmentRepository.DeletebYiDAsync(id);
            if (department > 0)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();

        }





        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            var dept = await _departmentRepository.GetSpacificByIdAsync(id);
            var departmentViewModel = _Mapper.Map<DepartmentViewModel>(dept);

            return View(departmentViewModel);
        }



        public async Task<IActionResult> Update(int? id)
        {

            if (id == null) return BadRequest();

            var department =await _departmentRepository.GetSpacificByIdAsync(id);

            if (department is null)
            {
                return NotFound();
            }
            var departmentViewModel = _Mapper.Map<DepartmentViewModel>(department);

            return View(departmentViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Update(DepartmentViewModel DepartmentViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var department=_Mapper.Map<Departments>(DepartmentViewModel);
                    var result = await _departmentRepository.UpdateAsync(department);
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }

            }
            catch (Exception ex) {

                Console.WriteLine(ex.Message);
            }


            return View(DepartmentViewModel);


        }


    }
}
