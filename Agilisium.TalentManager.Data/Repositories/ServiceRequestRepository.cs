using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class ServiceRequestRepository : RepositoryBase<ServiceRequest>, IServiceRequestRepository
    {
        public void Add(ServiceRequestDto entity)
        {
            int statusID = DataContext.DropDownSubCategories.Where(c => c.SubCategoryName == "Email Sent").FirstOrDefault().SubCategoryID;
            ServiceRequest request = CreateBusinessEntity(entity, true);
            request.RequestStatusID = statusID;
            Entities.Add(request);
            DataContext.Entry(request).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Add(IEnumerable<ServiceRequestDto> serviceRequests)
        {
            foreach(var request in serviceRequests)
            {
                Add(request);
            }
        }

        public void Delete(ServiceRequestDto entity)
        {
            ServiceRequest buzEntity = Entities.FirstOrDefault(e => e.ServiceRequestID == entity.ServiceRequestID);
            buzEntity.IsDeleted = true;
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.ServiceRequestID == id && c.IsDeleted == false);
        }

        public bool Exists(string itemName)
        {
            return false;
        }

        public IEnumerable<ServiceRequestDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<ServiceRequestDto> requests = from p in Entities
                                                join v in DataContext.Vendors on p.VendorID equals v.VendorID into ve
                                                from vd in ve.DefaultIfEmpty()
                                                join s in DataContext.DropDownSubCategories on p.RequestStatusID equals s.SubCategoryID into se
                                                from sd in se.DefaultIfEmpty()
                                                orderby p.ServiceRequestID
                                                where p.IsDeleted == false
                                                select new ServiceRequestDto
                                                {
                                                    CompletedDate = p.CompletedDate,
                                                    RequestedDate = p.RequestedDate,
                                                    RequestedSkill = p.RequestedSkill,
                                                    RequestStatus = sd.SubCategoryName,
                                                    RequestStatusID = p.RequestStatusID,
                                                    ServiceRequestID = p.ServiceRequestID,
                                                    VendorID = p.VendorID,
                                                    VendorName = vd.VendorName
                                                };

            if (pageSize <= 0 || pageNo < 1)
            {
                return requests;
            }

            return requests.Skip((pageNo - 1) * pageSize).Take(pageSize);

        }

        public ServiceRequestDto GetByID(int id)
        {
            return (from p in Entities
                    where p.ServiceRequestID == id && p.IsDeleted == false
                    join v in DataContext.Vendors on p.VendorID equals v.VendorID into ve
                    from vd in ve.DefaultIfEmpty()
                    select new ServiceRequestDto
                    {
                        CompletedDate = p.CompletedDate,
                        RequestedDate = p.RequestedDate,
                        RequestedSkill = p.RequestedSkill,
                        RequestStatusID = p.RequestStatusID,
                        ServiceRequestID = p.ServiceRequestID,
                        VendorID = p.VendorID,
                        VendorName = vd.VendorName
                    }).FirstOrDefault();
        }

        public void Update(ServiceRequestDto entity)
        {
            ServiceRequest buzEntity = Entities.FirstOrDefault(e => e.ServiceRequestID == entity.ServiceRequestID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private ServiceRequest CreateBusinessEntity(ServiceRequestDto categoryDto, bool isNewEntity = false)
        {
            ServiceRequest request = new ServiceRequest
            {
                CompletedDate = categoryDto.CompletedDate,
                RequestedDate = categoryDto.RequestedDate,
                RequestedSkill = categoryDto.RequestedSkill,
                RequestStatusID = categoryDto.RequestStatusID,
                ServiceRequestID = categoryDto.ServiceRequestID,
                VendorID = categoryDto.VendorID,
            };

            request.UpdateTimeStamp(categoryDto.LoggedInUserName, true);

            return request;
        }

        private void MigrateEntity(ServiceRequestDto sourceEntity, ServiceRequest targetEntity)
        {
            targetEntity.CompletedDate = sourceEntity.CompletedDate;
            targetEntity.RequestedDate = sourceEntity.RequestedDate;
            targetEntity.RequestedSkill = sourceEntity.RequestedSkill;
            targetEntity.ServiceRequestID = sourceEntity.ServiceRequestID;
            targetEntity.RequestStatusID = sourceEntity.RequestStatusID;
            targetEntity.VendorID = sourceEntity.VendorID;

            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }
    }

    public interface IServiceRequestRepository : IRepository<ServiceRequestDto>
    {
        void Add(IEnumerable<ServiceRequestDto> serviceRequests);
    }
}
