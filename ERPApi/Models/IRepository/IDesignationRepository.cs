namespace ERPApi.Models.IRepository
{
    public interface IDesignationRepository
    {
        object GetAllDesignations();

        designation GetDesignationByID(int designation_id);

        designation GetDesignationByName(string designation_name);

        bool CheckDesignationForDuplicateByname(string designation_name);

        bool InsertDesignation(designation oDesignation);

        bool UpdateDesignation(designation oDesignation);

        bool DeleteDesignation(int designation_id);
    }
}