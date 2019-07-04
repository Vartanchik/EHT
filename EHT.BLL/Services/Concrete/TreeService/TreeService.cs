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

        public async Task<IList<NodeDto>> GetTreeAsync()
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

        public async Task<ServiceResult> CreateNodeAsync(NodeDto dto)
        {
            try
            {
                switch (dto.Type)
                {
                    case "Organization":
                        var organization = _mapper.Map<Organization>(dto);
                        return await CreateOrganizationAsync(organization);

                    case "Country":
                        var country = _mapper.Map<Country>(dto);
                        return await CreateCountryAsync(country);

                    case "Business":
                        var business = _mapper.Map<Business>(dto);
                        return await CreateBusinessAsync(business);

                    case "Family":
                        var family = _mapper.Map<Family>(dto);
                        return await CreateFamilyAsync(family);

                    case "Offering":
                        var offering = _mapper.Map<Offering>(dto);
                        return await CreateOfferingAsync(offering);

                    case "Department":
                        var department = _mapper.Map<Department>(dto);
                        return await CreateDepartmentAsync(department);

                    default:
                        return new ServiceResult("Not found such type as: " + dto.Type);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex.Message);
            }
        }

        public async Task<ServiceResult> UpdateNodeAsync(NodeDto dto)
        {
            try
            {
                switch (dto.Type)
                {
                    case "Organization":
                        var organization = _mapper.Map<Organization>(dto);
                        return await UpdateOrganizationAsync(organization);

                    case "Country":
                        var country = _mapper.Map<Country>(dto);
                        return await UpdateCountryAsync(country);

                    case "Business":
                        var business = _mapper.Map<Business>(dto);
                        return await UpdateBusinessAsync(business);

                    case "Family":
                        var family = _mapper.Map<Family>(dto);
                        return await UpdateFamilyAsync(family);

                    case "Offering":
                        var offering = _mapper.Map<Offering>(dto);
                        return await UpdateOfferingAsync(offering);

                    case "Department":
                        var department = _mapper.Map<Department>(dto);
                        return await UpdateDepartmentAsync(department);

                    default:
                        return new ServiceResult("Not found such type as: " + dto.Type);
                }
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
                        return new ServiceResult("Not found such type as: " + dto.Type);
                }

                return new ServiceResult();
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex.Message);
            }
        }

        private async Task<ServiceResult> CreateOrganizationAsync(Organization organization)
        {
            var organizationExist = await _uow.Organizations.AsQueryable()
                                                            .AnyAsync(o => o.Code == organization.Code);

            if (organizationExist) return new ServiceResult($"Organization with code: {organization.Code} - already exist.");
            
            await _uow.Organizations.Create(organization);
            await _uow.CommitAsync();

            return new ServiceResult();

        }

        private async Task<ServiceResult> CreateCountryAsync(Country country)
        {
            var organizationExist = await _uow.Organizations.AsQueryable()
                                                            .AnyAsync(o => o.Id == country.OrganizationId);

            if (!organizationExist) return new ServiceResult($"Organization with id: {country.OrganizationId} - not found.");

            var countryExist = await _uow.Countries.AsQueryable()
                                                   .AnyAsync(c => c.Code == country.Code &&
                                                                  c.OrganizationId == country.OrganizationId);

            if (countryExist) return new ServiceResult($"Country with code: {country.Code} - already exist.");
            
            await _uow.Countries.Create(country);
            await _uow.CommitAsync();

            return new ServiceResult();

        }

        private async Task<ServiceResult> CreateBusinessAsync(Business business)
        {
            var countryExist = await _uow.Countries.AsQueryable()
                                                   .AnyAsync(c => c.Id == business.CountryId);

            if (!countryExist) return new ServiceResult($"Country with id: {business.CountryId} - not found.");

            var businessExist = await _uow.Businesses.AsQueryable()
                                                     .AnyAsync(b => b.Name == business.Name &&
                                                                    b.CountryId == business.CountryId);

            if (businessExist) return new ServiceResult($"Business with name: {business.Name} - already exist.");
            
            await _uow.Businesses.Create(business);
            await _uow.CommitAsync();

            return new ServiceResult();

        }

        private async Task<ServiceResult> CreateFamilyAsync(Family family)
        {
            var businessExist = await _uow.Businesses.AsQueryable()
                                                     .AnyAsync(b => b.Id == family.BusinessId);

            if (!businessExist) return new ServiceResult($"Business with id: {family.BusinessId} - not found.");

            var familyExist = await _uow.Families.AsQueryable()
                                                 .AnyAsync(f => f.Name == family.Name &&
                                                                f.BusinessId == family.BusinessId);

            if (familyExist) return new ServiceResult($"Family with name: {family.Name} - already exist.");
            
            await _uow.Families.Create(family);
            await _uow.CommitAsync();

            return new ServiceResult();

        }

        private async Task<ServiceResult> CreateOfferingAsync(Offering offering)
        {
            var familyExist = await _uow.Families.AsQueryable()
                                                 .AnyAsync(f => f.Id == offering.FamilyId);

            if (!familyExist) return new ServiceResult($"Family with id: {offering.FamilyId} - not found.");

            var offeringExist = await _uow.Offerings.AsQueryable()
                                                    .AnyAsync(o => o.Name == offering.Name &&
                                                                   o.FamilyId == offering.FamilyId);

            if (offeringExist) return new ServiceResult($"Offering with name: {offering.Name} - already exist.");
            
            await _uow.Offerings.Create(offering);
            await _uow.CommitAsync();

            return new ServiceResult();

        }

        private async Task<ServiceResult> CreateDepartmentAsync(Department department)
        {
            var offeringExist = await _uow.Offerings.AsQueryable()
                                                    .AnyAsync(o => o.Id == department.OfferingId);

            if (!offeringExist) return new ServiceResult($"Offering with id: {department.OfferingId} - not found.");

            var departmentExist = await _uow.Departments.AsQueryable()
                                                        .AnyAsync(d => d.Name == department.Name &&
                                                                       d.OfferingId == department.OfferingId);

            if (departmentExist) return new ServiceResult($"Department with name: {department.Name} - already exist.");
            
            await _uow.Departments.Create(department);
            await _uow.CommitAsync();

            return new ServiceResult();

        }

        private async Task<ServiceResult> UpdateOrganizationAsync(Organization organization)
        {
            var organizationExist = await _uow.Organizations.AsQueryable()
                                                            .AnyAsync(o => o.Id == organization.Id);

            if (!organizationExist) return new ServiceResult($"Organization with id: {organization.Id} - not found.");

            var organizationCodeOccupied = await _uow.Organizations.AsQueryable()
                                                                   .AnyAsync(o => o.Code == organization.Code &&
                                                                                  o.Id != organization.Id);

            if (organizationCodeOccupied) return new ServiceResult($"Organization with code: {organization.Code} - already exist.");

            await _uow.Organizations.Update(organization);
            await _uow.CommitAsync();

            return new ServiceResult();
        }

        private async Task<ServiceResult> UpdateCountryAsync(Country country)
        {
            var countryExist = await _uow.Countries.AsQueryable()
                                                   .AnyAsync(c => c.Id == country.Id);

            if (!countryExist) return new ServiceResult($"Country with id: {country.Id} - not found.");

            var countryCodeOccupied = await _uow.Countries.AsQueryable()
                                                          .AnyAsync(c => c.Code == country.Code &&
                                                                         c.OrganizationId == country.OrganizationId &&
                                                                         c.Id != country.Id);

            if (countryCodeOccupied) return new ServiceResult($"Country with code: {country.Code} - already exist.");

            await _uow.Countries.Update(country);
            await _uow.CommitAsync();

            return new ServiceResult();
        }

        private async Task<ServiceResult> UpdateBusinessAsync(Business business)
        {
            var businessExist = await _uow.Businesses.AsQueryable()
                                                     .AnyAsync(b => b.Id == business.Id);

            if (!businessExist) return new ServiceResult($"Business with id: {business.Id} - not found.");

            var businessNameOccupied = await _uow.Businesses.AsQueryable()
                                                            .AnyAsync(b => b.Name == business.Name &&
                                                                           b.CountryId == business.CountryId &&
                                                                           b.Id != business.Id);

            if (businessNameOccupied) return new ServiceResult($"Business with name: {business.Name} - already exist.");

            await _uow.Businesses.Update(business);
            await _uow.CommitAsync();

            return new ServiceResult();
        }

        private async Task<ServiceResult> UpdateFamilyAsync(Family family)
        {
            var familyExist = await _uow.Families.AsQueryable()
                                                 .AnyAsync(f => f.Id == family.Id);

            if (!familyExist) return new ServiceResult($"Family with id: {family.Id} - not found.");

            var familyNameOccupied = await _uow.Families.AsQueryable()
                                                        .AnyAsync(f => f.Name == family.Name &&
                                                                       f.BusinessId == family.BusinessId &&
                                                                       f.Id != family.Id);

            if (familyNameOccupied) return new ServiceResult($"Family with name: {family.Name} - already exist.");

            await _uow.Families.Update(family);
            await _uow.CommitAsync();

            return new ServiceResult();
        }

        private async Task<ServiceResult> UpdateOfferingAsync(Offering offering)
        {
            var offeringExist = await _uow.Offerings.AsQueryable()
                                                    .AnyAsync(o => o.Id == offering.Id);

            if (!offeringExist) return new ServiceResult($"Offering with id: {offering.Id} - not found.");

            var offeringNameOccupied = await _uow.Offerings.AsQueryable()
                                                           .AnyAsync(o => o.Name == offering.Name &&
                                                                          o.FamilyId == offering.FamilyId &&
                                                                          o.Id != offering.Id);

            if (offeringNameOccupied) return new ServiceResult($"Offering with name: {offering.Name} - already exist.");

            await _uow.Offerings.Update(offering);
            await _uow.CommitAsync();

            return new ServiceResult();
        }

        private async Task<ServiceResult> UpdateDepartmentAsync(Department department)
        {
            var departmentExist = await _uow.Departments.AsQueryable()
                                                        .AnyAsync(d => d.Id == department.Id);

            if (!departmentExist) return new ServiceResult($"Department with id: {department.Id} - not found.");

            var departmentNameOccupied = await _uow.Departments.AsQueryable()
                                                               .AnyAsync(d => d.Name == department.Name &&
                                                                              d.OfferingId == department.OfferingId &&
                                                                              d.Id != department.Id);

            if (departmentNameOccupied) return new ServiceResult($"Department with name: {department.Name} - already exist.");

            await _uow.Departments.Update(department);
            await _uow.CommitAsync();

            return new ServiceResult();
        }
    }
}
