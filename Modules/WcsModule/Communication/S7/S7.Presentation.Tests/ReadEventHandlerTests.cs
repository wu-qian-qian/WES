using Common.Domain;
using MediatR;
using Moq;
using S7.Application.Handlers;
using S7.CustomEvents;
using S7.Presentationl;
using System.Threading;
using Xunit;

namespace S7.Presentation.Tests;

public class ReadEventHandlerTests
{
    private readonly Mock<ISender> _mockSender;
    private readonly ReadEventHandler _handler;

    public ReadEventHandlerTests()
    {
        _mockSender = new Mock<ISender>();
        _handler = new ReadEventHandler(_mockSender.Object);
    }

    [Fact]
    public async Task Handle_WithValidDeviceName_ReturnsSuccessResult()
    {
        // Arrange
        var deviceName = "TestDevice";
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        
        IEnumerable<EntityModel> entityModels = new List<EntityModel>
        {
            new() { DBName = "DB1", DBValue = "Value1" },
            new() { DBName = "DB2", DBValue = "Value2" }
        };
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<IEnumerable<EntityModel>>.Success(entityModels)));

        // Act
        var result = await _handler.Handle(eventModel);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count());
        Assert.Equal("DB1", result.Value.First().DBName);
        Assert.Equal("Value1", result.Value.First().DBValue);
    }

    [Fact]
    public async Task Handle_WithEmptyEntityList_ReturnsErrorResult()
    {
        // Arrange
        var deviceName = "TestDevice";
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        
        IEnumerable<EntityModel> emptyList = new List<EntityModel>();
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<IEnumerable<EntityModel>>.Success(emptyList)));

        // Act
        var result = await _handler.Handle(eventModel);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Contains("输入读取失败", result.Message);
    }

    [Fact]
    public async Task Handle_WithFailedReadCommand_ReturnsErrorResult()
    {
        // Arrange
        var deviceName = "TestDevice";
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        var errorMessage = "读取失败";
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Error<IEnumerable<EntityModel>>(errorMessage)));

        // Act
        var result = await _handler.Handle(eventModel);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Contains("输入读取失败", result.Message);
        Assert.Contains(errorMessage, result.Message);
    }

    [Fact]
    public async Task Handle_WithSingleEntity_ReturnsSuccessWithOneItem()
    {
        // Arrange
        var deviceName = "SingleDevice";
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        
        IEnumerable<EntityModel> entityModels = new List<EntityModel>
        {
            new() { DBName = "SingleDB", DBValue = "SingleValue" }
        };
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<IEnumerable<EntityModel>>.Success(entityModels)));

        // Act
        var result = await _handler.Handle(eventModel);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);
        Assert.Equal("SingleDB", result.Value.First().DBName);
        Assert.Equal("SingleValue", result.Value.First().DBValue);
    }

    [Fact]
    public async Task Handle_WithNullDeviceName_ThrowsException()
    {
        // Arrange
        var eventModel = new ReadIntegrationEvent { DeviceName = null! };
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Error<IEnumerable<EntityModel>>("设备名不能为空")));

        // Act
        var result = await _handler.Handle(eventModel);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Contains("输入读取失败", result.Message);
    }

    [Fact]
    public async Task Handle_WithMultipleEntities_PreservesOrder()
    {
        // Arrange
        var deviceName = "OrderedDevice";
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        
        IEnumerable<EntityModel> entityModels = new List<EntityModel>
        {
            new() { DBName = "DB1", DBValue = "First" },
            new() { DBName = "DB2", DBValue = "Second" },
            new() { DBName = "DB3", DBValue = "Third" }
        };
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<IEnumerable<EntityModel>>.Success(entityModels)));

        // Act
        var result = await _handler.Handle(eventModel);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value.Count());
        
        var resultList = result.Value.ToList();
        Assert.Equal("DB1", resultList[0].DBName);
        Assert.Equal("First", resultList[0].DBValue);
        Assert.Equal("DB2", resultList[1].DBName);
        Assert.Equal("Second", resultList[1].DBValue);
        Assert.Equal("DB3", resultList[2].DBName);
        Assert.Equal("Third", resultList[2].DBValue);
    }

    [Fact]
    public async Task Handle_WithCancellationToken_PassesTokenToSender()
    {
        // Arrange
        var deviceName = "CancelTestDevice";
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        var cancellationToken = new CancellationTokenSource().Token;
        
        IEnumerable<EntityModel> entityModels = new List<EntityModel>
        {
            new() { DBName = "DB1", DBValue = "Value1" }
        };
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<IEnumerable<EntityModel>>.Success(entityModels)));

        // Act
        var result = await _handler.Handle(eventModel, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _mockSender.Verify(s => s.Send(
            It.IsAny<ReadBufferCommand>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineData("Device1")]
    [InlineData("Device2")]
    [InlineData("Test_Device_123")]
    public async Task Handle_WithVariousDeviceNames_ReturnsSuccess(string deviceName)
    {
        // Arrange
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        
        IEnumerable<EntityModel> entityModels = new List<EntityModel>
        {
            new() { DBName = "TestDB", DBValue = "TestValue" }
        };
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<IEnumerable<EntityModel>>.Success(entityModels)));

        // Act
        var result = await _handler.Handle(eventModel);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);
    }

    [Fact]
    public async Task Handle_WithSpecialCharactersInValues_ReturnsCorrectValues()
    {
        // Arrange
        var deviceName = "SpecialCharDevice";
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        
        IEnumerable<EntityModel> entityModels = new List<EntityModel>
        {
            new() { DBName = "DB_特殊字符", DBValue = "值_@#$%" },
            new() { DBName = "DB_Special", DBValue = "Value_123!@#" }
        };
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<IEnumerable<EntityModel>>.Success(entityModels)));

        // Act
        var result = await _handler.Handle(eventModel);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count());
        Assert.Equal("DB_特殊字符", result.Value.First().DBName);
        Assert.Equal("值_@#$%", result.Value.First().DBValue);
    }

    [Fact]
    public async Task Handle_WithEmptyStringValues_ReturnsResultWithEmptyValues()
    {
        // Arrange
        var deviceName = "EmptyValueDevice";
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        
        IEnumerable<EntityModel> entityModels = new List<EntityModel>
        {
            new() { DBName = "EmptyDB", DBValue = "" }
        };
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<IEnumerable<EntityModel>>.Success(entityModels)));

        // Act
        var result = await _handler.Handle(eventModel);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);
        Assert.Equal("", result.Value.First().DBValue);
    }

    [Fact]
    public async Task Handle_WithVeryLongDeviceName_ReturnsSuccess()
    {
        // Arrange
        var deviceName = new string('A', 1000);
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        
        IEnumerable<EntityModel> entityModels = new List<EntityModel>
        {
            new() { DBName = "DB", DBValue = "Value" }
        };
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<IEnumerable<EntityModel>>.Success(entityModels)));

        // Act
        var result = await _handler.Handle(eventModel);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);
    }

    [Fact]
    public async Task Handle_WhenSenderThrowsException_PropagatesException()
    {
        // Arrange
        var deviceName = "ExceptionDevice";
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Sender 异常"));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => 
            _handler.Handle(eventModel));
    }

    [Fact]
    public async Task Handle_WithManyEntities_ReturnsAllEntities()
    {
        // Arrange
        var deviceName = "ManyEntitiesDevice";
        var eventModel = new ReadIntegrationEvent { DeviceName = deviceName };
        
        IEnumerable<EntityModel> entityModels = Enumerable.Range(1, 100)
            .Select(i => new EntityModel 
            { 
                DBName = $"DB{i}", 
                DBValue = $"Value{i}" 
            })
            .ToList();
        
        _mockSender
            .Setup(s => s.Send(It.IsAny<ReadBufferCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<IEnumerable<EntityModel>>.Success(entityModels)));

        // Act
        var result = await _handler.Handle(eventModel);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(100, result.Value.Count());
        Assert.Equal("DB1", result.Value.First().DBName);
        Assert.Equal("DB100", result.Value.Last().DBName);
    }
}
