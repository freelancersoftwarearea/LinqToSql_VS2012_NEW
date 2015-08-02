using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{
	public Class1()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}

//public class clsEmpDep
//{
//    public int Id { get; set; }
//    public int DeptId { get; set; }
//    public string Name { get; set; }
//    public string Active { get; set; }
//}

public class clsEmpDep
{
    public int Id { get; set; }
    public int DeptId { get; set; }
    public string Name { get; set; }
    public string Active { get; set; }


    public clsEmpDep(int _id, int _deptId, string _name, string _active)
    {
        Id = _id; DeptId = _deptId; Name = _name; Active = _active;
    }
}