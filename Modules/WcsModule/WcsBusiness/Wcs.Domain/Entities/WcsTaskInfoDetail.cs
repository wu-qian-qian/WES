using Common.Domain.Entity;

public class WcsTaskInfoDetail:BaseEntity
{
    public WcsTaskInfoDetail()
    {
        Id=Guid.NewGuid();
    }

    public int Index { get; set; }
    public Guid TaskInfoId { get; set; }

    public string StartLocation { get; set; }

    public string EndLocation { get; set; }

    public DeviceTaskTypeEnum DeviceTaskType { get; set; }

    public TaskStatusTypeEnum TaskStatusType{get;set;}

    public string DeviceName { get; set; }
}