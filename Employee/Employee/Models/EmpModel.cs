using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee.Models
{
    public class EmpModel
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpDeg { get; set; }

        public string EmpCode { get; set; }
        public int EmpAge { get; set; }
    }
}