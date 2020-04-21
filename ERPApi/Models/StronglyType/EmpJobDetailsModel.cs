using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class EmpJobDetailsModel : employee
    {
                  public int emp_job_details_id { get; set; }
                  public int job_location_id {get;set;} 
                  public string start_date {get;set;} 
                  public string end_date {get;set;} 
                  public bool is_active {get;set;} 
                  public int created_by {get;set;} 
                  public string created_date {get;set;} 
                  public int updated_by {get;set;} 
                  public string updated_date {get;set;} 
                  public string emp_prop_confirmation_date {get;set;} 
                  public string emp_supervisor {get;set;} 
                  public string emp_reporting_method { get; set; }
                  public int leave_policy_id { get; set; }
    }
}