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
        public IActionResult Index()
        {
            var AllDepaprtment = _departmentRepository.GetAll();
            var deparmentViewModel = _Mapper.Map<IEnumerable< DepartmentViewModel>>(AllDepaprtment) ;

            return View(deparmentViewModel);
        }

        public IActionResult Create() 
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel DepartmentViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var department = _Mapper.Map<Departments>(DepartmentViewModel);
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

            return View(DepartmentViewModel);


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
            var departmentViewModel = _Mapper.Map<DepartmentViewModel>(dept);

            return View(departmentViewModel);
        }



        public IActionResult Update(int? id)
        {

            if (id == null) return BadRequest();

            var department = _departmentRepository.GetSpacificById(id);

            if (department is null)
            {
                return NotFound();
            }
            var departmentViewModel = _Mapper.Map<DepartmentViewModel>(department);

            return View(departmentViewModel);
        }


        [HttpPost]
        public IActionResult Update(DepartmentViewModel DepartmentViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var department=_Mapper.Map<Departments>(DepartmentViewModel);
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


            return View(DepartmentViewModel);


        }


    }
}
