using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class page1 : System.Web.UI.Page
{
    int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
           bindGrid();
    }
    
    void bindGrid()
    {
        grdData.DataSource = GetData();
        grdData.DataBind();
    }

    List<clsEmpDep> GetData()
    {
        #region Code Commented
        /*
        List<clsEmpDep> cED = new List<clsEmpDep>();

        DataClassesDataContext objD = new DataClassesDataContext();

        var data = (from d in objD.tblDepartments
                    join e1 in objD.tblEmployees
                     on d.id equals e1.DeptId
                    where d.Active == true
                    //&& e1.Active == true
                    select new
                    {
                        Id = e1.Id,
                        DeptId = d.id,
                        Name = e1.Name,
                        Active = e1.Active.Equals(true) ? "Yes" : "No"
                    });

        if (data.Count() > 0)
        {
            foreach (var item in data)
            {
                clsEmpDep c = new clsEmpDep();
                c.Id = item.Id;
                c.Name = item.Name;
                c.DeptId = item.DeptId;
                c.Active = item.Active;
                cED.Add(c);
            }
        }
       return cED;
         */
        #endregion

        #region Code
         
        DataClassesDataContext objD = new DataClassesDataContext();
        List<clsEmpDep> cED = null;

        #region Inner Join

            #region Sql Query inner join
            //select * from tblEmployees e inner join tblDepartment d on e.DeptId = d.id 
            //where e.Active = 1 order by e.Id 
            #endregion

        cED = (from e in objD.tblEmployees
               join d in objD.tblDepartments
               on e.DeptId equals d.id
               where e.Active == true //&& d.Active == true
               orderby e.Id ascending
               select new clsEmpDep(e.Id, d.id, e.Name, e.Active.Equals(true) ? "Yes" : "No")
                     ).ToList();

      
        #endregion

        #region Left Outer join WAY 1

            #region Sql Left Join Query 
               //select * from   tblEmployees e left join tblDepartment d on e.DeptId = d.id 
               //where e.Active = 1 order by e.Id
            #endregion
         
        cED = (from e in objD.tblEmployees
               join d in objD.tblDepartments
                on e.DeptId equals d.id into sr  // all data of left side table (i.e.tblEmployees) will save into sr 
               where e.Active == true
               orderby e.Id ascending
               from rit in sr.DefaultIfEmpty()
               select new clsEmpDep(e.Id, rit.id == null ? 0 : rit.id, e.Name, e.Active.Equals(true) ? "Yes" : "No")
                   ).ToList();

        #endregion

        #region Left Outer join WAY 2
        //cED = (from e in objD.tblEmployees
        //        from d in objD.tblDepartments.Where(d => d.id == e.DeptId && e.Active == true).DefaultIfEmpty()
        //        where e.Active == true
        //        orderby e.Id ascending
        //        select new clsEmpDep(e.Id, d.id == null ? 0 : d.id, e.Name, e.Active.Equals(true) ? "Yes" : "No")
        //           ).ToList();

        #endregion

        return cED;
        
        #endregion
    }
     
    void BindData()
    {

        DataClassesDataContext objD = new DataClassesDataContext();

        var data = (from d in objD.tblDepartments
                    join e1 in objD.tblEmployees
                     on d.id equals e1.DeptId
                    where d.Active == true
                    //&& e1.Active == true
                    select new
                    {
                        Id = e1.Id,
                        DeptId = d.id,
                        Name = e1.Name,
                        Active = e1.Active.Equals(true) ? "Yes" : "No"
                    }).ToList(); 

        if (data.Count > 0)
        {
           //if(i==0) grdData.DataSource = data.Take(1);
           //else grdData.DataSource = data.Skip(i).Take(1);
            grdData.DataSource = data;
            grdData.DataBind();
        }
        else
        {
            grdData.DataSource = null;
            grdData.DataBind();
        }

 

        
    }

    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        bindGrid();
    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        DisplayAdd();
        txtEmpName.Text = "";
        BindDrp();
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (DataClassesDataContext objD = new DataClassesDataContext())
        {
            tblEmployee tblEmp = new tblEmployee();
            tblEmp.Name = txtEmpName.Text;
            tblEmp.DeptId = int.Parse(drpDepartment.SelectedValue);
            tblEmp.Active = Convert.ToBoolean(OptActive.SelectedValue);
            objD.tblEmployees.InsertOnSubmit(tblEmp);
            objD.SubmitChanges();     
        }
        DisplayData();
        bindGrid();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DisplayData();
    }

    void DisplayAdd() {
        divData.Visible = false;
        divAdd.Visible = true;
    }

    void DisplayData() {
        divData.Visible = true;
        divAdd.Visible = false;
    }

    void BindDrp() {

        DataClassesDataContext objD = new DataClassesDataContext();
        var v = (from c in objD.tblDepartments
                 where c.Active == true
                 select new { Id = c.id, Name = c.Name }
                 ).Distinct().ToList();

        if (v.Count > 0) {
            drpDepartment.DataSource = v;
            drpDepartment.DataTextField = "Name";
            drpDepartment.DataValueField = "Id";
            drpDepartment.DataBind();

            drpDepartment.Items.Insert(0,new ListItem("--Select--", "0"));
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        btnUpdate.Visible = true;

        DisplayAdd();
        BindDrp();

        LinkButton lnkEdit = sender as LinkButton;
        lnkWangle.CommandArgument = lnkEdit.CommandArgument;
        
        List<clsEmpDep> d = GetData();
        List<clsEmpDep> d1 = (from c in d
                              where c.Id == int.Parse(lnkEdit.CommandArgument)
                              select c).ToList();

        txtEmpName.Text = d1[0].Name;
        drpDepartment.SelectedValue = d1[0].DeptId.ToString();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (DataClassesDataContext objD = new DataClassesDataContext())
        {
            tblEmployee objEmp = objD.tblEmployees.Single(t => t.Id == int.Parse(lnkWangle.CommandArgument));
            objEmp.Name = txtEmpName.Text;
            objEmp.DeptId =int.Parse(drpDepartment.SelectedValue);
            objEmp.Active = Convert.ToBoolean(OptActive.SelectedValue);
            //objD.tblEmployees.InsertOnSubmit(objEmp);
            objD.SubmitChanges();
        }
        DisplayData();
        bindGrid();
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkDelete = sender as LinkButton;
        using (DataClassesDataContext objD = new DataClassesDataContext())
        { 
            tblEmployee t= objD.tblEmployees.Single(i=>i.Id == int.Parse(lnkDelete.CommandArgument));
            objD.tblEmployees.DeleteOnSubmit(t);
            objD.SubmitChanges();
        }
        DisplayData();
        bindGrid();
    }
}