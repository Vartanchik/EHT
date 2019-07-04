using AutoMapper;
using EHT.BLL.DTOs;
using EHT.BLL.Services;
using EHT.BLL.Services.Concrete.TreeService;
using EHT.DAL.Entities;
using EHT.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    public class CreateOrUpdateNode_UnitTests
    {
        private Mock<IUnitOfWork> _mockUOW;
        private IMapper _mapper;

        private ITreeService GetTreeService()
        {
            _mockUOW = new Mock<IUnitOfWork>();
            //_mockUOW.Setup(m => m.Organizations.AsQueryable().AnyAsync<Organization>(It.IsAny<Expression<Func<Organization, bool>>>())).ReturnsAsync(true);
            //    }).Returns(() => Task.FromResult(foo));
            //_mockUOW.Setup(m => m.Organizations.AsQueryable().AnyAsync(It.IsAny<Expression<Func<Organization, bool>>>())).Callback<Expression<Func<Organization, bool>>>(
            //    expression =>
            //    {
            //        var func = expression.Compile();
            //        var foo = func(new Organization() { Code = "1111" });
            //    }).Returns(() => Task.FromResult(foo));
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DtoMapProfile>());
            _mapper = new Mapper(config);
            return new TreeService(_mockUOW.Object, _mapper);
        }

        [SetUp]
        public void Setup()
        {
        }


        //_mockUOW.Setup(x => x.Businesses.CreateOrUpdate(new Business()));
        [Test]
        public void CreateNode_AllNodeTypes_NodeIsCreated()
        {
            var treeService = GetTreeService();

            treeService.CreateNodeAsync(new NodeDto
            {
                Id = 0,
                Name = "CIA",
                Type = "Organization",
                Properties = new NodePropertiesDto
                {
                    Code = "3524",
                    OrganizationType = OrganizationType.IncorporatedCompany,
                    OrganizationOwner = "Boss"
                },
                ParentId = 0
            });

            treeService.CreateNodeAsync(new NodeDto
            {
                Id = 0,
                Name = "USA",
                Type = "Country",
                Properties = new NodePropertiesDto
                {
                    Code = "1111"
                },
                ParentId = 1
            });

            treeService.CreateNodeAsync(new NodeDto
            {
                Id = 0,
                Name = "GIS",
                Type = "Business",
                ParentId = 1
            });

            treeService.CreateNodeAsync(new NodeDto
            {
                Id = 0,
                Name = "Data",
                Type = "Family",
                ParentId = 1
            });

            treeService.CreateNodeAsync(new NodeDto
            {
                Id = 0,
                Name = "Data store",
                Type = "Offering",
                ParentId = 1
            });

            treeService.CreateNodeAsync(new NodeDto
            {
                Id = 0,
                Name = "Sales",
                Type = "Department",
                ParentId = 1
            });

            _mockUOW.Verify(m => m.Organizations, Times.Exactly(2));
            _mockUOW.Verify(m => m.Countries, Times.Exactly(2));
            _mockUOW.Verify(m => m.Businesses, Times.Exactly(2));
            _mockUOW.Verify(m => m.Families, Times.Exactly(2));
            _mockUOW.Verify(m => m.Offerings, Times.Exactly(2));
            _mockUOW.Verify(m => m.Departments, Times.Exactly(2));
        }

        [Test]
        public async Task CreateOrganizationAsync_SuccessfulResult()
        {
            var treeService = GetTreeService();

            var organization = new Organization
            {
                Id = 0,
                Name = "CIA",
                Code = "3524",
                OrganizationType = OrganizationType.IncorporatedCompany,
                Owner = "Boss"
            };

            //_mockUOW.Setup(x => x.Organizations.Create(organization));

            var node = new NodeDto
            {
                Id = 0,
                Name = "CIA",
                Type = "Organization",
                Properties = new NodePropertiesDto
                {
                    Code = "3524",
                    OrganizationType = OrganizationType.IncorporatedCompany,
                    OrganizationOwner = "Boss"
                },
                ParentId = 0
            };

            var actualResult = await treeService.CreateNodeAsync(node);

            Assert.IsTrue(actualResult.Succeeded);

        }
    }
}