using AutoMapper;
using EHT.BLL.DTOs;
using EHT.BLL.Services;
using EHT.BLL.Services.Concrete.TreeService;
using EHT.DAL;
using EHT.DAL.Entities;
using EHT.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EHT.XUnitTests
{
    public class DeleteNode_Tests
    {
        private ITreeService _treeService;

        private ITreeService GetTreeService()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var context = new ApplicationDbContext(contextOptions);

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<DtoMapProfile>());

            var mapper = new Mapper(mapperConfig);

            return new TreeService(new UnitOfWork(context), mapper);
        }

        [Fact]
        public async Task DeleteOrganization_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var organizationToDelete = new NodeDto
            {
                Id = 1,
                Type = "Organization"
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.DeleteNodeAsync(organizationToDelete);

            var actualTree = await _treeService.GetTreeAsync();

            Assert.True(result.Succeeded);
            Assert.Equal(1, actualTree.Count);
        }

        [Fact]
        public async Task DeleteCountry_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var countryToDelete = new NodeDto
            {
                Id = 1,
                Type = "Country"
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.DeleteNodeAsync(countryToDelete);

            var actualTree = await _treeService.GetTreeAsync();

            Assert.True(result.Succeeded);
            Assert.Equal(3, actualTree.Count);
        }

        [Fact]
        public async Task DeleteBusiness_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var businessToDelete = new NodeDto
            {
                Id = 1,
                Type = "Business"
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.DeleteNodeAsync(businessToDelete);

            var actualTree = await _treeService.GetTreeAsync();

            Assert.True(result.Succeeded);
            Assert.Equal(5, actualTree.Count);
        }

        [Fact]
        public async Task DeleteFamily_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var familyToDelete = new NodeDto
            {
                Id = 1,
                Type = "Family"
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.DeleteNodeAsync(familyToDelete);

            var actualTree = await _treeService.GetTreeAsync();

            Assert.True(result.Succeeded);
            Assert.Equal(7, actualTree.Count);
        }

        [Fact]
        public async Task DeleteOffering_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var offeringToDelete = new NodeDto
            {
                Id = 1,
                Type = "Offering"
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.DeleteNodeAsync(offeringToDelete);

            var actualTree = await _treeService.GetTreeAsync();

            Assert.True(result.Succeeded);
            Assert.Equal(9, actualTree.Count);
        }

        [Fact]
        public async Task DeleteDepartment_IsSuccessful()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var departmentToDelete = new NodeDto
            {
                Id = 1,
                Type = "Department"
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var result = await _treeService.DeleteNodeAsync(departmentToDelete);

            var actualTree = await _treeService.GetTreeAsync();

            Assert.True(result.Succeeded);
            Assert.Equal(11, actualTree.Count);
        }

        [Fact]
        public async Task DeleteNode_WrongNodeType_IsFailed()
        {
            _treeService = GetTreeService();

            var dataToSetupDB = GetNodes();

            var nodeToDelete = new NodeDto
            {
                Id = 1,
                Type = "Home"
            };

            foreach (var node in dataToSetupDB)
            {
                var dbSetupResult = await _treeService.CreateNodeAsync(node);
                Assert.True(dbSetupResult.Succeeded);
            }

            var actualResult = await _treeService.DeleteNodeAsync(nodeToDelete);

            var expectResult = new ServiceResult($"Not found such type as: {nodeToDelete.Type}");

            var actualTree = await _treeService.GetTreeAsync();

            Assert.Equal(12, actualTree.Count);
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
