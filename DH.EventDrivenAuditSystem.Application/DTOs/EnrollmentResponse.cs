using System;
using System.Collections.Generic;
using System.Text;

namespace DH.EventDrivenAuditSystem.Application.DTOs
{
    public sealed record EnrollmentResponse(int CourseId,
                                            string CourseName,
                                            int UserId,
                                            string UserName,
                                            DateTime EnrollmentDate, 
                                            DateTime ExpirationDate);
}                                           