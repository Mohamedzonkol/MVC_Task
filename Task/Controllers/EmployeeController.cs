using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task.DataAccess.Repositories.Interface;
using Task.Models;
using Task.Models.ViewModel;

namespace Task.Controllers
{

    public class EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost) : Controller
    {
        public IActionResult Index()
        {
            var employees = unitOfWork.Employee.GetAll(includeProperty: "Department").ToList();
            return View(employees);
        }

        public IActionResult Details(int id)
        {
            var employee = unitOfWork.Employee.Get(x => x.Id == id, includeProperties: "Department,CreatedByUser");
            return View(employee);
        }

        public IActionResult Create()
        {

            EmployeeViewModel employeeVM = new()
            {
                Employee = new Employee(),
                DepartmentList = unitOfWork.Department.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                UserList = unitOfWork.User.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(employeeVM);


        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                if (employeeVM.Employee.Image is not null)
                {
                    string fileName =
                        Guid.NewGuid().ToString() + Path.GetExtension(employeeVM.Employee.Image.FileName);
                    string imagePath = Path.Combine(webHost.WebRootPath, @"Images\Employee");
                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    employeeVM.Employee.Image.CopyTo(fileStream);
                    employeeVM.Employee.ProfileImage = @"\Images\Employee\" + fileName;
                }
                else
                {
                    employeeVM.Employee.ProfileImage = "https://placehold.co/600x400"; //default image
                }

                unitOfWork.Employee.Add(employeeVM.Employee);
                unitOfWork.Save();
                TempData["success"] = "Employee created successfully";
                return RedirectToAction("Index");
            }

            employeeVM.DepartmentList = unitOfWork.Department.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            employeeVM.UserList = unitOfWork.User.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            TempData["Error"] = "The Employee Can Not Be Created .";
            return View(employeeVM);
        }

        public IActionResult Update(int id)
        {

            EmployeeViewModel employeeVM = new()
            {
                Employee = unitOfWork.Employee.Get(x => x.Id == id, includeProperties: "Department,CreatedByUser"),
                DepartmentList = unitOfWork.Department.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                UserList = unitOfWork.User.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (employeeVM.Employee is null)
            {
                TempData["Error"] = "The Employee Can Not Be Updated .";
                return RedirectToAction("Error", "Home");
            }

            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                if (employeeVM.Employee.Image is not null)
                {
                    string fileName =
                        Guid.NewGuid().ToString() + Path.GetExtension(employeeVM.Employee.Image.FileName);
                    string imagePath = Path.Combine(webHost.WebRootPath, @"Images\Employee");
                    if (!string.IsNullOrEmpty(employeeVM.Employee.ProfileImage))
                    {
                        string oldImagePath = Path.Combine(webHost.WebRootPath, employeeVM.Employee.ProfileImage.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                            System.IO.File.Delete(oldImagePath);
                    }
                    await using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    await employeeVM.Employee.Image.CopyToAsync(fileStream);
                    employeeVM.Employee.ProfileImage = @"\Images\Employee\" + fileName;
                }
                unitOfWork.Employee.Update(employeeVM.Employee);
                unitOfWork.Save();
                TempData["Success"] = "The Employee Has Been Updated Successfully .";
                return RedirectToAction("Index", "Employee");
            }
            TempData["Error"] = "The Employee Can Not Be Updated .";

            return View(employeeVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            EmployeeViewModel employeeVM = new()
            {
                Employee = unitOfWork.Employee.Get(x => x.Id == id, includeProperties: "Department,CreatedByUser"),
                DepartmentList = unitOfWork.Department.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                UserList = unitOfWork.User.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (employeeVM.Employee is null)
            {
                TempData["Error"] = "The Employee Can Not Be Updated .";
                return RedirectToAction("Error", "Home");
            }
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                employeeVM.Employee = unitOfWork.Employee.Get(x => x.Id == employeeVM.Employee.Id);
                if (!string.IsNullOrEmpty(employeeVM.Employee.ProfileImage))
                {
                    string oldImagePath =
                        Path.Combine(webHost.WebRootPath, employeeVM.Employee.ProfileImage.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                unitOfWork.Employee.Remove(employeeVM.Employee);
                unitOfWork.Save();
                TempData["Success"] = "The Employee Has Been Deleted Successfully .";
                return RedirectToAction("Index", "Employee");
            }

            catch (Exception ex)
            {
                TempData["Error"] = "The Employee Can Not Be Deleted .";
                return RedirectToAction("Error", "Home");
            }

        }
    }
}

