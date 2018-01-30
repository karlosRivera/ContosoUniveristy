using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contoso.Domain.ViewModels
{
    public class EnrollmentIndexData  
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public string CourseTitle { get; set; }
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public decimal? Grade { get; set; }
    }

    
    //public class EnrollmentIndexDataList : List<EnrollmentIndexData>
    //{

    //    public EnrollmentIndexDataList()
    //        : base()
    //    {
    //    }

    //    public EnrollmentIndexDataList(IEnumerable<EnrollmentIndexData> collection)
    //         : base(collection) 
    //    {
    //    }

    //    public IEnumerable<EnrollmentIndexData> SortByCourseID(IEnumerable<EnrollmentIndexData> ListItems)
    //    {
    //        return ListItems.OrderBy(p => p.CourseID);
    //    }

    //    public IEnumerable<EnrollmentIndexData> SortByCourseIDDesc(IEnumerable<EnrollmentIndexData> ListItems)
    //    {
    //        return ListItems.OrderByDescending(p => p.CourseID);
    //    } 

    //    public IEnumerable<EnrollmentIndexData> SortByFullName(IEnumerable<EnrollmentIndexData> ListItems)
    //    {
    //        return ListItems.OrderBy(p => p.FullName);
    //    }

    //    public IEnumerable<EnrollmentIndexData> SortByFullNameDesc(IEnumerable<EnrollmentIndexData> ListItems)
    //    {
    //        return ListItems.OrderByDescending(p => p.FullName);
    //    }
        
    //    public IEnumerable<EnrollmentIndexData> SortByCourseTitleDesc(IEnumerable<EnrollmentIndexData> ListItems) 
    //    { 
    //        return ListItems.OrderByDescending(p => p.CourseTitle); 
    //    }

    //    public IEnumerable<EnrollmentIndexData> SortByCourseTitle(IEnumerable<EnrollmentIndexData> ListItems)
    //    {
    //        return ListItems.OrderBy(p => p.CourseTitle);
    //    }

     
    //}
}