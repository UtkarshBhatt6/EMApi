﻿using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class DepartmentsService : IDepartmentsService
{
    private readonly IDepartmentsRepository _departmentsRepository;

    public DepartmentsService(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;
    }

    public async Task<IEnumerable<DepartmentResponse>> GetAllDepartments()
    {
        var departments = await _departmentsRepository.GetAllDepartments();
        return departments.Select(department => department.ToDepartmentResponse());
    }

    public async Task<DepartmentResponse?> GetDepartmentById(Guid? departmentId)
    {
        if (departmentId == null) throw new ArgumentNullException();

        var employee = await _departmentsRepository.GetDepartmentById(departmentId);

        if (employee == null) throw new ArgumentException();

        return employee.ToDepartmentResponse();
    }

    public async Task<DepartmentResponse> AddDepartment(AddDepartmentRequest addDepartmentRequest)
    {
        if (addDepartmentRequest == null) throw new ArgumentNullException();

        var department = addDepartmentRequest.ToDepartment();
        department.DepartmentId = Guid.NewGuid();
        await _departmentsRepository.AddDepartment(department);
        return department.ToDepartmentResponse();
    }

    public async Task<bool> UpdateDepartment(UpdateDepartmentRequest updateDepartmentRequest)
    {
        if (updateDepartmentRequest == null) throw new ArgumentNullException();

        var matchingDepartment =
            await _departmentsRepository.GetDepartmentById(updateDepartmentRequest.DepartmentId);

        if (matchingDepartment == null) throw new ArgumentException();

        matchingDepartment.DepartmentName = updateDepartmentRequest.DepartmentName;

        return await _departmentsRepository.UpdateDepartment(matchingDepartment);
    }

    public async Task<bool> DeleteDepartment(Guid? departmentId)
    {
        if (departmentId == null) throw new ArgumentNullException();

        var department = await _departmentsRepository.GetDepartmentById(departmentId);

        if (department == null) throw new ArgumentException();

        return await _departmentsRepository.DeleteDepartment(departmentId);
    }
}