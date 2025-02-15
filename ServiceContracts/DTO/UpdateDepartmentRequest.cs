﻿using System;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO
{
    public class UpdateDepartmentRequest
    {
        public Guid DepartmentId { get; set; }

        [Required(ErrorMessage = "Department cannot be empty!")]
        public string DepartmentName { get; set; }

        public Department ToDepartment()
        {
            return new Department
            {
                DepartmentId = DepartmentId,
                DepartmentName = DepartmentName
            };
        }
    }
}