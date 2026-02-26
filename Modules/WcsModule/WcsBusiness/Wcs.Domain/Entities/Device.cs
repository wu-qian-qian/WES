using Common.Domain.Entity;

namespace Wcs.Domain.Entities
{
    public class Device : DomainEntity
    {
        public Device():base(Guid.NewGuid())
        {
            
        }
        public string Name { get; set; }
        
        public DeviceTypeEnum DeviceType { get; set; }

        /// <summary>
        /// 配置信息，格式可以是JSON字符串
        /// </summary>
        public string DeviceConfiguration { get; set; }

        public string DeviceDescription { get; set; }

        public bool IsActive { get; set; }


        /// <summary>
        /// 通知上层系统激活或禁用
        /// </summary>
        /// <param name="activate"></param>
        public void Activate(bool activate)
        {
            if(activate)
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }
            //AddDomainEvent();
        }

        /// <summary>
        ///设备配置信息更新
        /// </summary>
        /// <param name="configuration"></param>
        public void UpdateConfiguration(string configuration)
        {
            DeviceConfiguration = configuration;
        }
        /// <summary>
        /// 设备基本信息更新
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="regionCode"></param>
        public void Update(string name, string description)
        {
            Name = name;
            DeviceDescription = description;
        }
    }
}