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
    public class GetTree_Tests
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
        public async Task GetTreeAsync_IsSuccessful()
        {
            _treeService = GetTreeService();

            var excpectResult = GetNodes();

            foreach (var node in excpectResult)
            {
                await _treeService.CreateNodeAsync(node);
            }

            var actualResult = await _treeService.GetTreeAsync();

            for (int i = 0; i < excpectResult.Count; i++)
            {
                switch (excpectResult[i].Type)
                {
                    case "Organization":
                        Assert.Equal(excpectResult[i].Id, actualResult[i].Id);
                        Assert.Equal(excpectResult[i].Name, actualResult[i].Name);
                        Assert.Equal(excpectResult[i].ParentId, actualResult[i].ParentId);
                        Assert.Equal(excpectResult[i].Type, actualResult[i].Type);
                        Assert.Equal(excpectResult[i].Properties.Code, actualResult[i].Properties.Code);
                        Assert.Equal(excpectResult[i].Properties.OrganizationType, actualResult[i].Properties.OrganizationType);
                        Assert.Equal(excpectResult[i].Properties.OrganizationOwner, actualResult[i].Properties.OrganizationOwner);
                        return;

                    case "Country":
                        Assert.Equal(excpectResult[i].Id, actualResult[i].Id);
                        Assert.Equal(excpectResult[i].Name, actualResult[i].Name);
                        Assert.Equal(excpectResult[i].ParentId, actualResult[i].ParentId);
                        Assert.Equal(excpectResult[i].Type, actualResult[i].Type);
                        Assert.Equal(excpectResult[i].Properties.Code, actualResult[i].Properties.Code);
                        return;

                    default:
                        Assert.Equal(excpectResult[i].Id, actualResult[i].Id);
                        Assert.Equal(excpectResult[i].Name, actualResult[i].Name);
                        Assert.Equal(excpectResult[i].ParentId, actualResult[i].ParentId);
                        Assert.Equal(excpectResult[i].Type, actualResult[i].Type);
                        return;

                }
            }
        }

        private List<NodeDto> GetNodes()
        {
            var listOfNodes = new List<NodeDto>
            {
                new NodeDto
                {
                    Id = 1,
                    Name = "Organization1",
                    Type = "Organization",
                    Properties = new NodePropertiesDto
                    {
                        Code = "402568",
                        OrganizationType = OrganizationType.GeneralPartnership,
                        OrganizationOwner = "Mr. Smith"
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
                        Code = "687456",
                        OrganizationType = OrganizationType.GeneralPartnership,
                        OrganizationOwner = "Mr. Bernz"
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
                    Name = "Moldova",
                    Type = "Country",
                    Properties = new NodePropertiesDto
                    {
                        Code = "1025"
                    },
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 3,
                    Name = "China",
                    Type = "Country",
                    Properties = new NodePropertiesDto
                    {
                        Code = "2165"
                    },
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 4,
                    Name = "USA",
                    Type = "Country",
                    Properties = new NodePropertiesDto
                    {
                        Code = "1111"
                    },
                    ParentId = 2
                },

                new NodeDto
                {
                    Id = 5,
                    Name = "Italy",
                    Type = "Country",
                    Properties = new NodePropertiesDto
                    {
                        Code = "3645"
                    },
                    ParentId = 2
                },

                new NodeDto
                {
                    Id = 1,
                    Name = "Internet",
                    Type = "Business",
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 2,
                    Name = "Food",
                    Type = "Business",
                    ParentId = 2
                },

                new NodeDto
                {
                    Id = 3,
                    Name = "Gym",
                    Type = "Business",
                    ParentId = 3
                },

                new NodeDto
                {
                    Id = 4,
                    Name = "Casino",
                    Type = "Business",
                    ParentId = 4
                },

                new NodeDto
                {
                    Id = 5,
                    Name = "Internet",
                    Type = "Business",
                    ParentId = 5
                },

                new NodeDto
                {
                    Id = 1,
                    Name = "Network",
                    Type = "Family",
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 2,
                    Name = "Restaurant",
                    Type = "Family",
                    ParentId = 2
                },

                new NodeDto
                {
                    Id = 3,
                    Name = "Body building",
                    Type = "Family",
                    ParentId = 3
                },

                new NodeDto
                {
                    Id = 4,
                    Name = "Black jack",
                    Type = "Family",
                    ParentId = 4
                },

                new NodeDto
                {
                    Id = 5,
                    Name = "Data",
                    Type = "Family",
                    ParentId = 5
                },

                new NodeDto
                {
                    Id = 1,
                    Name = "Cabels",
                    Type = "Offering",
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 2,
                    Name = "Sushi",
                    Type = "Offering",
                    ParentId = 2
                },

                new NodeDto
                {
                    Id = 3,
                    Name = "Instructors",
                    Type = "Offering",
                    ParentId = 3
                },

                new NodeDto
                {
                    Id = 4,
                    Name = "Some kind of black jack",
                    Type = "Offering",
                    ParentId = 4
                },

                new NodeDto
                {
                    Id = 5,
                    Name = "Servers",
                    Type = "Offering",
                    ParentId = 5
                },

                new NodeDto
                {
                    Id = 1,
                    Name = "Sales",
                    Type = "Department",
                    ParentId = 1
                },

                new NodeDto
                {
                    Id = 2,
                    Name = "Kitchen",
                    Type = "Department",
                    ParentId = 2
                },

                new NodeDto
                {
                    Id = 3,
                    Name = "Commercial",
                    Type = "Department",
                    ParentId = 3
                },

                new NodeDto
                {
                    Id = 4,
                    Name = "Accounting",
                    Type = "Department",
                    ParentId = 4
                },

                new NodeDto
                {
                    Id = 5,
                    Name = "Support",
                    Type = "Department",
                    ParentId = 5
                }
            };
            return listOfNodes;
        }
    }
}
