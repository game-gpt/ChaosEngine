# 测试指南

本文档介绍 Chaos 引擎项目的测试规范和最佳实践。

---

## 测试框架

项目使用 **xUnit** 作为单元测试框架。

### 安装测试依赖

测试项目需要在 `.csproj` 中添加以下引用：

```xml
<ItemGroup>
  <PackageReference Include="xunit" Version="2.6.0" />
  <PackageReference Include="xunit.runner.visualstudio" Version="2.5.0" />
  <PackageReference Include="coverlet.collector" Version="6.0.0" />
</ItemGroup>
```

---

## 测试命名约定

### 测试类命名

测试类以被测试类名 + `Tests` 后缀命名。

```csharp
// 被测试类
public class LegendSystem { }

// 测试类
public class LegendSystemTests { }
```

### 测试方法命名

使用 `方法名_场景_预期结果` 格式。

```csharp
public class LegendSystemTests
{
    [Fact]
    public void GetImprint_ExistingPlayer_ReturnsImprint() { }
    
    [Fact]
    public void GetImprint_NonExistingPlayer_ReturnsNull() { }
    
    [Fact]
    public void UpdateImprint_NullPlayerId_ThrowsArgumentNullException() { }
}
```

---

## 编写单元测试

### 基本测试结构

```csharp
using Chaos.Legend;
using Chaos.Models;
using Xunit;

namespace Chaos.Tests.Legend;

public class LegendSystemTests
{
    private readonly LegendSystem _system;
    
    public LegendSystemTests()
    {
        _system = new LegendSystem();
    }
    
    [Fact]
    public void GetImprint_ExistingPlayer_ReturnsImprint()
    {
        // Arrange
        var playerId = new PlayerId(Guid.NewGuid());
        var action = new PlayerAction { Type = ActionType.KillMonster };
        
        // Act
        _system.UpdateImprint(playerId, action);
        var result = _system.GetImprint(playerId);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(playerId, result.PlayerId);
    }
    
    [Fact]
    public void GetImprint_NonExistingPlayer_ReturnsNull()
    {
        // Arrange
        var playerId = new PlayerId(Guid.NewGuid());
        
        // Act
        var result = _system.GetImprint(playerId);
        
        // Assert
        Assert.Null(result);
    }
}
```

### 参数化测试

使用 `[Theory]` 和 `[InlineData]` 进行参数化测试。

```csharp
[Theory]
[InlineData(0.5, 0.5, 1.0)]
[InlineData(-0.5, 0.5, 0.0)]
[InlineData(1.0, -1.0, 0.0)]
public void UpdateImprint_CombatAction_UpdatesConquestDimension(
    double initialValue, 
    double delta, 
    double expected)
{
    // Arrange
    var playerId = new PlayerId(Guid.NewGuid());
    var action = new PlayerAction 
    { 
        Type = ActionType.KillMonster,
        Delta = delta 
    };
    
    // Act
    _system.UpdateImprint(playerId, action);
    var imprint = _system.GetImprint(playerId);
    
    // Assert
    Assert.Equal(expected, imprint!.Conquest, precision: 2);
}
```

### 异常测试

```csharp
[Fact]
public void UpdateImprint_NullPlayerId_ThrowsArgumentNullException()
{
    // Arrange
    var action = new PlayerAction { Type = ActionType.KillMonster };
    
    // Act & Assert
    Assert.Throws<ArgumentNullException>(() => 
        _system.UpdateImprint(null!, action));
}
```

---

## 运行测试

### 命令行运行

```bash
# 运行所有测试
dotnet test

# 运行特定项目测试
dotnet test projects/Chaos/tests

# 运行特定测试类
dotnet test --filter "FullyQualifiedName~LegendSystemTests"

# 运行特定测试方法
dotnet test --filter "FullyQualifiedName~LegendSystemTests.GetImprint_ExistingPlayer_ReturnsImprint"

# 详细输出
dotnet test --logger "console;verbosity=detailed"
```

### 运行配置

在 `xunit.runner.json` 中配置运行选项：

```json
{
  "parallelizeAssembly": true,
  "parallelizeTestCollections": true,
  "maxParallelThreads": 4
}
```

---

## 代码覆盖率

### 生成覆盖率报告

```bash
# 生成覆盖率数据
dotnet test --collect:"XPlat Code Coverage"

# 使用 ReportGenerator 生成 HTML 报告
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage-report
```

### 覆盖率目标

| 指标 | 目标值 |
|:---|:---|
| 行覆盖率 | ≥ 80% |
| 分支覆盖率 | ≥ 70% |
| 核心模块覆盖率 | ≥ 90% |

---

## 测试最佳实践

### 1. 遵循 AAA 模式

```csharp
[Fact]
public void Method_Scenario_ExpectedResult()
{
    // Arrange - 准备测试数据
    
    // Act - 执行被测试方法
    
    // Assert - 验证结果
}
```

### 2. 保持测试独立

每个测试应该独立运行，不依赖其他测试的执行顺序。

```csharp
// 正确：每个测试创建自己的实例
public class LegendSystemTests
{
    private readonly LegendSystem _system = new();
}

// 错误：共享可变状态
public class LegendSystemTests
{
    private static LegendSystem _sharedSystem = new(); // 避免这样做
}
```

### 3. 测试边界条件

```csharp
[Theory]
[InlineData(-1.0)]  // 最小值
[InlineData(0.0)]   // 零值
[InlineData(1.0)]   // 最大值
[InlineData(0.001)] // 接近零
public void SetDimension_BoundaryValues_AcceptsValidRange(double value)
{
    // 测试边界值
}
```

### 4. 使用有意义的断言消息

```csharp
Assert.NotNull(imprint);
Assert.InRange(imprint.Conquest, -1.0, 1.0);
```

---

## Mock 和依赖注入

使用 Moq 模拟依赖项。

```csharp
using Moq;
using Xunit;

public class FortuneSystemTests
{
    private readonly Mock<ILegendSystem> _legendMock;
    private readonly FortuneSystem _system;
    
    public FortuneSystemTests()
    {
        _legendMock = new Mock<ILegendSystem>();
        _system = new FortuneSystem(_legendMock.Object);
    }
    
    [Fact]
    public void CheckFortuneLevel_HighLegendPlayer_ReturnsNormalLevel()
    {
        // Arrange
        var playerId = new PlayerId(Guid.NewGuid());
        _legendMock
            .Setup(x => x.GetImprint(playerId))
            .Returns(new LegendImprint { Conquest = 0.8 });
        
        // Act
        var result = _system.CheckFortuneLevel(playerId);
        
        // Assert
        Assert.Equal(FortuneLevel.Normal, result);
    }
}
```
