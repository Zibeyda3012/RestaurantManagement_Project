namespace Domain.BaseEntities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public int? UpdatedBy { get; set; }  // user id 
    public int? CreatedBy { get; set; }  // user id 
    public int? DeletedBy { get; set; }  // user id 
    public DateTime CreatedDate { get; set; } 
    public DateTime? UpdatedDate { get; set; }  
    public DateTime? DeletedDate { get; set; }  

    public bool IsDeleted { get; set; }

    public BaseEntity()
    {
        CreatedDate = DateTime.Now;
    }

}
