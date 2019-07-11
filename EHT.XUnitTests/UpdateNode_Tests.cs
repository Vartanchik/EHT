using AutoMapper;
using EHT.BLL.DTOs;
using EHT.BLL.Services;
using EHT.BLL.Services.Concrete.TreeService;
using EHT.DAL;
using EHT.DAL.Entities;
using EHT.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EHT.XUnitTests
{
    public class UpdateNode_Tests
    {
        private ITreeService _treeService;

        private ITreeService GetTreeService()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var context = new ApplicationDbContext(contextOptions);

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<DtoMapProfile>());

            var mapper = new Mapper(mapperConfig);

            var logger = new Logger<TreeService>(new LoggerFactory());

            return new TreeService(new UnitOfWork(context, null), mapper, logger);
        }

        [Fact]
        public async Task UpdateOrganization_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newOrganization = new NodeDto
            {
                Id = 1,
                Name = "newOrganization",
                Type = "Organization",
                Properties = new NodePropertiesDto
                {
                    Code = "1234",
                    OrganizationType = OrganizationType.IncorporatedCompany,
                    OrganizationOwner = "Mr. Paul"
                },
                ParentId = 0
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.UpdateNodeAsync(newOrganization);

            var actualOrganization = (await _treeService.GetTreeAsync())[0];

            Assert.True(result.Succeeded);
            Assert.Equal(newOrganization.Id, actualOrganization.Id);
            Assert.Equal(newOrganization.Name, actualOrganization.Name);
            Assert.Equal(newOrganization.ParentId, actualOrganization.ParentId);
            Assert.Equal(newOrganization.Type, actualOrganization.Type);
            Assert.Equal(newOrganization.Properties.Code, actualOrganization.Properties.Code);
            Assert.Equal(newOrganization.Properties.OrganizationType, actualOrganization.Properties.OrganizationType);
            Assert.Equal(newOrganization.Properties.OrganizationOwner, actualOrganization.Properties.OrganizationOwner);
        }

        [Fact]
        public async Task UpdateCountry_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newCountry = new NodeDto
            {
                Id = 1,
                Name = "Canada",
                Type = "Country",
                Properties = new NodePropertiesDto
                {
                    Code = "3333"
                },
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.UpdateNodeAsync(newCountry);

            var actualCountry = (await _treeService.GetTreeAsync())[2];

            Assert.True(result.Succeeded);
            Assert.Equal(newCountry.Id, actualCountry.Id);
            Assert.Equal(newCountry.Name, actualCountry.Name);
            Assert.Equal(newCountry.ParentId, actualCountry.ParentId);
            Assert.Equal(newCountry.Type, actualCountry.Type);
            Assert.Equal(newCountry.Properties.Code, actualCountry.Properties.Code);
        }

        [Fact]
        public async Task UpdateBusiness_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newBusiness = new NodeDto
            {
                Id = 1,
                Name = "Internet",
                Type = "Business",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.UpdateNodeAsync(newBusiness);

            var actualBusiness = (await _treeService.GetTreeAsync())[4];

            Assert.True(result.Succeeded);
            Assert.Equal(newBusiness.Id, actualBusiness.Id);
            Assert.Equal(newBusiness.Name, actualBusiness.Name);
            Assert.Equal(newBusiness.ParentId, actualBusiness.ParentId);
            Assert.Equal(newBusiness.Type, actualBusiness.Type);
        }

        [Fact]
        public async Task UpdateFamily_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newFamily = new NodeDto
            {
                Id = 1,
                Name = "Fast food",
                Type = "Family",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.UpdateNodeAsync(newFamily);

            var actualFamily = (await _treeService.GetTreeAsync())[6];

            Assert.True(result.Succeeded);
            Assert.Equal(newFamily.Id, actualFamily.Id);
            Assert.Equal(newFamily.Name, actualFamily.Name);
            Assert.Equal(newFamily.ParentId, actualFamily.ParentId);
            Assert.Equal(newFamily.Type, actualFamily.Type);
        }

        [Fact]
        public async Task UpdateOffering_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newOffering = new NodeDto
            {
                Id = 1,
                Name = "Sushi",
                Type = "Offering",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.UpdateNodeAsync(newOffering);

            var actualOffering = (await _treeService.GetTreeAsync())[8];

            Assert.True(result.Succeeded);
            Assert.Equal(newOffering.Id, actualOffering.Id);
            Assert.Equal(newOffering.Name, actualOffering.Name);
            Assert.Equal(newOffering.ParentId, actualOffering.ParentId);
            Assert.Equal(newOffering.Type, actualOffering.Type);
        }

        [Fact]
        public async Task UpdateDepartment_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newDepartment = new NodeDto
            {
                Id = 1,
                Name = "Kitchen",
                Type = "Department",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.UpdateNodeAsync(newDepartment);

            var actualDepartment = (await _treeService.GetTreeAsync())[10];

            Assert.True(result.Succeeded);
            Assert.Equal(newDepartment.Id, actualDepartment.Id);
            Assert.Equal(newDepartment.Name, actualDepartment.Name);
            Assert.Equal(newDepartment.ParentId, actualDepartment.ParentId);
            Assert.Equal(newDepartment.Type, actualDepartment.Type);
        }

        [Fact]
        public async Task UpdateOrganization_WrongNodeType_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newOrganization = new NodeDto
            {
                Id = 1,
                Name = "newOrganization1",
                Type = "Home",
                Properties = new NodePropertiesDto
                {
                    Code = "3548",
                    OrganizationType = OrganizationType.LimitedLiabilityCompany,
                    OrganizationOwner = "Mr. Bin"
                },
                ParentId = 0
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newOrganization);

            var expectResult = new ServiceResult($"Not found such type as: {newOrganization.Type}");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateCountry_WrongNodeType_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newCountry = new NodeDto
            {
                Id = 1,
                Name = "Canada",
                Type = "Home",
                Properties = new NodePropertiesDto
                {
                    Code = "1234"
                },
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newCountry);

            var expectResult = new ServiceResult($"Not found such type as: {newCountry.Type}");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateBusiness_WrongNodeType_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newBusiness = new NodeDto
            {
                Id = 1,
                Name = "Internet",
                Type = "Home",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newBusiness);

            var expectResult = new ServiceResult($"Not found such type as: {newBusiness.Type}");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateFamily_WrongNodeType_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newFamily = new NodeDto
            {
                Id = 1,
                Name = "Fast food",
                Type = "Home",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newFamily);

            var expectResult = new ServiceResult($"Not found such type as: {newFamily.Type}");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateOffering_WrongNodeType_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newOffering = new NodeDto
            {
                Id = 1,
                Name = "Pizza",
                Type = "Home",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newOffering);

            var expectResult = new ServiceResult($"Not found such type as: {newOffering.Type}");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateDepartment_WrongNodeType_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newDepartment = new NodeDto
            {
                Id = 1,
                Name = "Service",
                Type = "Home",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newDepartment);

            var expectResult = new ServiceResult($"Not found such type as: {newDepartment.Type}");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateOrganization_OrganizationNotFound_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newOrganization = new NodeDto
            {
                Id = 0,
                Name = "newOrganization1",
                Type = "Organization",
                Properties = new NodePropertiesDto
                {
                    Code = "1234",
                    OrganizationType = OrganizationType.GeneralPartnership,
                    OrganizationOwner = "Mr. Lu"
                },
                ParentId = 0
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newOrganization);

            var expectResult = new ServiceResult($"Organization with id: {newOrganization.Id} - not found.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateCountry_CountryNotFound_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newCountry = new NodeDto
            {
                Id = 0,
                Name = "Canada",
                Type = "Country",
                Properties = new NodePropertiesDto
                {
                    Code = "1234"
                },
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newCountry);

            var expectResult = new ServiceResult($"Country with id: {newCountry.Id} - not found.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateBusiness_BusinessNotFound_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newBusiness = new NodeDto
            {
                Id = 0,
                Name = "Internet",
                Type = "Business",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newBusiness);

            var expectResult = new ServiceResult($"Business with id: {newBusiness.Id} - not found.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateFamily_FamilyNotFound_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newFamily = new NodeDto
            {
                Id = 0,
                Name = "Fast food",
                Type = "Family",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newFamily);

            var expectResult = new ServiceResult($"Family with id: {newFamily.Id} - not found.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateOffering_OfferingNotFound_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newOffering = new NodeDto
            {
                Id = 0,
                Name = "Pizza",
                Type = "Offering",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newOffering);

            var expectResult = new ServiceResult($"Offering with id: {newOffering.Id} - not found.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateDepartment_DepartmentNotFound_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newDepartment = new NodeDto
            {
                Id = 0,
                Name = "Service",
                Type = "Department",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newDepartment);

            var expectResult = new ServiceResult($"Department with id: {newDepartment.Id} - not found.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }
        
        [Fact]
        public async Task UpdateOrganization_OrganizationAlreadyExist_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newOrganization = new NodeDto
            {
                Id = 1,
                Name = "Organization2",
                Type = "Organization",
                Properties = new NodePropertiesDto
                {
                    Code = "5678",
                    OrganizationType = OrganizationType.GeneralPartnership,
                    OrganizationOwner = "Mr. Paul"
                },
                ParentId = 0
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newOrganization);

            var expectResult = new ServiceResult($"Organization with code: {newOrganization.Properties.Code} - already exist.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateCountry_CountryAlreadyExist_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newCountry = new NodeDto
            {
                Id = 1,
                Name = "China",
                Type = "Country",
                Properties = new NodePropertiesDto
                {
                    Code = "4444"
                },
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newCountry);

            var expectResult = new ServiceResult($"Country with code: {newCountry.Properties.Code} - already exist.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateBusiness_BusinessAlreadyExist_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newBusiness = new NodeDto
            {
                Id = 1,
                Name = "Film",
                Type = "Business",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newBusiness);

            var expectResult = new ServiceResult($"Business with name: {newBusiness.Name} - already exist.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateFamily_FamilyAlreadyExist_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newFamily = new NodeDto
            {
                Id = 1,
                Name = "Cinema",
                Type = "Family",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newFamily);

            var expectResult = new ServiceResult($"Family with name: {newFamily.Name} - already exist.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateOffering_OfferingAlreadyExist_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newOffering = new NodeDto
            {
                Id = 1,
                Name = "Multfilms",
                Type = "Offering",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newOffering);

            var expectResult = new ServiceResult($"Offering with name: {newOffering.Name} - already exist.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        [Fact]
        public async Task UpdateDepartment_DepartmentAlreadyExist_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var newDepartment = new NodeDto
            {
                Id = 1,
                Name = "Sales",
                Type = "Department",
                ParentId = 1
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.UpdateNodeAsync(newDepartment);

            var expectResult = new ServiceResult($"Department with name: {newDepartment.Name} - already exist.");

            Assert.Equal(expectResult.Succeeded, actualResult.Succeeded);
            Assert.Equal(expectResult.Error, actualResult.Error);
        }

        private List<NodeDto> GetNodes()
        {
            var output = new List<NodeDto>
            {
                new NodeDto
                {
                    Id = 1,
                    Name = "Organization1",
                    Type = "Organization",
                    Properties = new NodePropertiesDto
                    {
                        Code = "1234",
                        OrganizationType = OrganizationType.LimitedLiabilityCompany,
                        OrganizationOwner = "Mr. Bin"
                    },
                    ParentId = 0
                },

                new NodeDto
                {
                    Id = 2,
                    Name = "Organization2",
                    Type = "Organization",
                    Properties = new NodePropertiesDto
                    {
                        Code = "5678",
                        OrganizationType = OrganizationType.GeneralPartnership,
                        OrganizationOwner = "Mr. Lu"
                    },
                    ParentId = 0
                },

                new NodeDto
                {
                    Id = 1,
                    Name = "USA",
                    Type = "Country",
                    Properties = new NodePropertiesDto
                    {
                        Code = "1111"
                    },
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 2,
                    Name = "China",
                    Type = "Country",
                    Properties = new NodePropertiesDto
                    {
                        Code = "4444"
                    },
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 1,
                    Name = "Food",
                    Type = "Business",
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 2,
                    Name = "Film",
                    Type = "Business",
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 1,
                    Name = "Restaurant",
                    Type = "Family",
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 2,
                    Name = "Cinema",
                    Type = "Family",
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 1,
                    Name = "Sushi",
                    Type = "Offering",
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 2,
                    Name = "Multfilms",
                    Type = "Offering",
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 1,
                    Name = "Kitchen",
                    Type = "Department",
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 2,
                    Name = "Sales",
                    Type = "Department",
                    ParentId = 1
                }
            };

            return output;
        }
    }
}
