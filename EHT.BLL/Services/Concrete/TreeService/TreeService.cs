using AutoMapper;
using EHT.BLL.DTOs;
using EHT.DAL.Entities;
using EHT.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHT.BLL.Services.Concrete.TreeService
{
    public class TreeService : ITreeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public TreeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<NodeDto>> GetTree()
        {
            try
            {
                var listOfNodes = new List<NodeDto>();

                var listOfOrganizations = await _uow.Organizations.AsQueryable().ToListAsync();

                listOfNodes.AddRange(_mapper.Map<List<NodeDto>>(listOfOrganizations));

                var listOfCountry = await _uow.Countries.AsQueryable().ToListAsync();

                listOfNodes.AddRange(_mapper.Map<List<NodeDto>>(listOfCountry));

                var listOfBusinesses = await _uow.Businesses.AsQueryable().ToListAsync();

                listOfNodes.AddRange(_mapper.Map<List<NodeDto>>(listOfBusinesses));

                var listOfFamilies = await _uow.Families.AsQueryable().ToListAsync();

                listOfNodes.AddRange(_mapper.Map<List<NodeDto>>(listOfFamilies));

                var listOfOfferings = await _uow.Offerings.AsQueryable().ToListAsync();

                listOfNodes.AddRange(_mapper.Map<List<NodeDto>>(listOfOfferings));

                var listOfDepartments = await _uow.Departments.AsQueryable().ToListAsync();

                listOfNodes.AddRange(_mapper.Map<List<NodeDto>>(listOfDepartments));

                return listOfNodes;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ServiceResult> CreateOrUpdateNodeAsync(NodeDto dto)
        {
            try
            {
                switch (dto.Type)
                {
                    case "Organization":
                        var organization = _mapper.Map<Organization>(dto);
                        await _uow.Organizations.CreateOrUpdate(organization);
                        await _uow.CommitAsync();
                        break;

                    case "Country":
                        var country = _mapper.Map<Country>(dto);
                        await _uow.Countries.CreateOrUpdate(country);
                        await _uow.CommitAsync();
                        break;

                    case "Business":
                        var business = _mapper.Map<Business>(dto);
                        await _uow.Businesses.CreateOrUpdate(business);
                        await _uow.CommitAsync();
                        break;

                    case "Family":
                        var family = _mapper.Map<Family>(dto);
                        await _uow.Families.CreateOrUpdate(family);
                        await _uow.CommitAsync();
                        break;

                    case "Offering":
                        var offering = _mapper.Map<Offering>(dto);
                        await _uow.Offerings.CreateOrUpdate(offering);
                        await _uow.CommitAsync();
                        break;

                    case "Department":
                        var department = _mapper.Map<Department>(dto);
                        await _uow.Departments.CreateOrUpdate(department);
                        await _uow.CommitAsync();
                        break;

                    default:
                        return new ServiceResult("Not found such type as:" + dto.Type);
                }

                return new ServiceResult();
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteNodeAsync(NodeDto dto)
        {
            try
            {
                switch (dto.Type)
                {
                    case "Organization":
                        await _uow.Organizations.DeleteAsync(dto.Id);
                        await _uow.CommitAsync();
                        break;

                    case "Country":
                        await _uow.Countries.DeleteAsync(dto.Id);
                        await _uow.CommitAsync();
                        break;

                    case "Business":
                        await _uow.Businesses.DeleteAsync(dto.Id);
                        await _uow.CommitAsync();
                        break;

                    case "Family":
                        await _uow.Families.DeleteAsync(dto.Id);
                        await _uow.CommitAsync();
                        break;

                    case "Offering":
                        await _uow.Offerings.DeleteAsync(dto.Id);
                        await _uow.CommitAsync();
                        break;

                    case "Department":
                        await _uow.Departments.DeleteAsync(dto.Id);
                        await _uow.CommitAsync();
                        break;

                    default:
                        return new ServiceResult("Not found such type as:" + dto.Type);
                }

                return new ServiceResult();
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex.Message);
            }
        }

    }
}
