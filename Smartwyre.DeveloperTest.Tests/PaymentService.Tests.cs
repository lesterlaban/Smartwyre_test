using System;
using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    private readonly IRebateService _rebateService;
    private readonly Mock<IRebateDataStore> _rebateRepository;
    private readonly Mock<IProductDataStore> _productRepository;
    public PaymentServiceTests()
    {
        _rebateRepository = new Mock<IRebateDataStore>();
        _productRepository = new Mock<IProductDataStore>();
        SetData();
        _rebateService = new RebateService(_productRepository.Object, _rebateRepository.Object);
    }

    private void SetData()
    {
        _rebateRepository.Setup(_ => _.GetRebate("")).Returns(new Rebate());
        _rebateRepository.Setup(_ => _.GetRebate("0")).Returns(new Rebate() { Incentive = IncentiveType.FixedRateRebate, Amount = 5, Percentage = 15 });
        _rebateRepository.Setup(_ => _.GetRebate("1")).Returns(new Rebate() { Incentive = IncentiveType.AmountPerUom, Amount = 10});
        _rebateRepository.Setup(_ => _.GetRebate("2")).Returns(new Rebate() { Incentive = IncentiveType.FixedCashAmount, Amount = 20 });

        _productRepository.Setup(_ => _.GetProduct("")).Returns(new Product());
        _productRepository.Setup(_ => _.GetProduct("0")).Returns(new Product() { Price = 10, SupportedIncentives = SupportedIncentiveType.FixedRateRebate });
        _productRepository.Setup(_ => _.GetProduct("1")).Returns(new Product() { SupportedIncentives = SupportedIncentiveType.AmountPerUom });
        _productRepository.Setup(_ => _.GetProduct("2")).Returns(new Product() { SupportedIncentives = SupportedIncentiveType.FixedCashAmount });
    }

    [Fact]
    public void InvalidIncentive()
    {
        string expectedMessage = "No valid incentive found.";
        var exception = Assert.Throws<MissingMemberException>(() => _rebateService.Calculate(new CalculateRebateRequest()));
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Fact]
    public void FixedRateRebate()
    {
        var request = new CalculateRebateRequest()
        {
            RebateIdentifier = "0", Volume = 10, ProductIdentifier = "0"
        };
        var result = _rebateService.Calculate(request);
        Assert.True(result.Success);
    }

    [Fact]
    public void AmountPerUom()
    {
        var request = new CalculateRebateRequest()
        {
            RebateIdentifier = "1", Volume = 5, ProductIdentifier = "1"
        };
        var result = _rebateService.Calculate(request);
        Assert.True(result.Success);
    }

    [Fact]
    public void FixedCashAmount()
    {
        var request = new CalculateRebateRequest()
        {
            RebateIdentifier = "2", ProductIdentifier = "2"
        };
        var result = _rebateService.Calculate(request);
        Assert.True(result.Success);
    }
}
