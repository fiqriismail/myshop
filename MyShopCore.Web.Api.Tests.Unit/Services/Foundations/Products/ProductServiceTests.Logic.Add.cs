using FluentAssertions;
using Moq;
using MyShopCore.Web.Api.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyShopCore.Web.Api.Tests.Unit.Services.Foundations.Products
{
    public partial class ProductServiceTests
    {

        [Fact]
        public async ValueTask ShouldAddProductAsync()
        {
            // given 
            DateTimeOffset randomDateTime = DateTimeOffset.Now;
            DateTimeOffset dateTime = randomDateTime;

            Product randomProduct = new Product();
            randomProduct.Id = Guid.NewGuid();
            randomProduct.CostPrice = 100;
            randomProduct.SellingPrice = 120;
            randomProduct.Title = "Unit Test Product";
            randomProduct.Description = "Description";
            randomProduct.Created = dateTime;
            randomProduct.CreatedBy = Guid.NewGuid();
            randomProduct.OrderAfter = 20;

            Product inputProduct = randomProduct;
            Product storageProduct = inputProduct;
            Product expectedProduct = storageProduct;

            this.dateTimeBrokerMock.Setup(broker =>
             broker.GetCurrentDateTime()).
             Returns(dateTime);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertProductAsync(inputProduct))
                .ReturnsAsync(storageProduct);

            // when 
            Product actualProduct = 
                await this.productService.AddProductAsync(inputProduct);

            // then
            actualProduct.Should().BeEquivalentTo(expectedProduct);

            /*
             // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            DateTimeOffset dateTime = randomDateTime;
            Student randomStudent = CreateRandomStudent(randomDateTime);
            randomStudent.UpdatedBy = randomStudent.CreatedBy;
            Student inputStudent = randomStudent;
            Student storageStudent = randomStudent;
            Student expectedStudent = storageStudent;

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(dateTime);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertStudentAsync(inputStudent))
                    .ReturnsAsync(storageStudent);

            // when
            Student actualStudent =
                await this.studentService.RegisterStudentAsync(inputStudent);

            // then
            actualStudent.Should().BeEquivalentTo(expectedStudent);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentAsync(inputStudent),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
             */
        }
    }
}
