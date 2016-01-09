# 编码规范

本文档定义 Chaos 引擎项目的 C# 编码规范。

---

## 命名约定

### 大小写规则

| 标识符类型 | 命名风格 | 示例 |
|:---|:---|:---|
| 类、结构、枚举 | PascalCase | `LegendImprint` |
| 接口 | PascalCase + I前缀 | `ILegendSystem` |
| 公共属性、方法 | PascalCase | `GetImprint()` |
| 私有字段 | camelCase + _前缀 | `_playerId` |
| 局部变量、参数 | camelCase | `playerId` |
| 常量 | PascalCase | `MaxLevel` |

### 示例代码

```csharp
public class LegendImprint
{
    private readonly PlayerId _playerId;
    
    public PlayerId PlayerId => _playerId;
    
    public double Conquest { get; set; }
    
    public const int MaxDimensionValue = 100;
    
    public LegendImprint(PlayerId playerId)
    {
        _playerId = playerId;
    }
    
    public void UpdateDimension(string dimensionName, double value)
    {
        // 方法实现
    }
}
```

---

## 接口命名

接口必须以 `I` 前缀开头，后接 PascalCase 名称。

```csharp
// 正确
public interface ILegendSystem
{
    LegendImprint GetImprint(PlayerId playerId);
}

public interface IHeavenlyPrincipleSystem
{
    double GetParameter(HeavenlyParameter param);
}

// 错误
public interface LegendSystem { }      // 缺少 I 前缀
public interface IlegendSystem { }     // L 应大写
```

---

## 命名空间约定

命名空间遵循 `Chaos.{ModuleName}` 格式。

```csharp
// 核心模块
namespace Chaos;

// 子模块
namespace Chaos.Legend;
namespace Chaos.Heavenly;
namespace Chaos.Karma;
namespace Chaos.Fortune;

// 模型
namespace Chaos.Models;

// 公共工具
namespace Chaos.Common;
```

---

## 可空引用类型

项目启用可空引用类型（Nullable Reference Types），必须正确处理空值。

```csharp
// 启用可空引用类型
// 在 .csproj 中：<Nullable>enable</Nullable>

public class FortuneState
{
    // 可空属性
    public Title? ActiveTitle { get; set; }
    
    // 非空属性，必须初始化
    public byte Level { get; set; } = 0;
    
    // 集合属性，初始化为空集合
    public List<FortuneEffect> Effects { get; set; } = [];
}

public LegendImprint? FindImprint(PlayerId playerId)
{
    // 返回可空类型
    return _imprints.TryGetValue(playerId, out var imprint) ? imprint : null;
}

public void ProcessPlayer(PlayerId playerId)
{
    var imprint = FindImprint(playerId);
    
    // 空值检查
    if (imprint is null)
    {
        return;
    }
    
    // 使用空条件运算符
    var title = imprint.ActiveTitle?.Name;
}
```

---

## 代码注释

代码注释使用简体中文编写。

### 类和接口注释

```csharp
/// <summary>
/// 传奇系统接口，负责管理玩家传奇印记和称号涌现。
/// </summary>
public interface ILegendSystem
{
    /// <summary>
    /// 获取玩家的传奇印记数据。
    /// </summary>
    /// <param name="playerId">玩家唯一标识符</param>
    /// <returns>玩家的传奇印记，若不存在则返回 null</returns>
    LegendImprint? GetImprint(PlayerId playerId);
}
```

### 方法注释

```csharp
/// <summary>
/// 更新玩家的传奇印记。
/// </summary>
/// <param name="playerId">玩家ID</param>
/// <param name="action">玩家行为数据</param>
/// <exception cref="ArgumentNullException">playerId 为空时抛出</exception>
public void UpdateImprint(PlayerId playerId, PlayerAction action)
{
    ArgumentNullException.ThrowIfNull(playerId);
    // 实现逻辑
}
```

### 行内注释

```csharp
public void CalculateKarma(PlayerAction action)
{
    // 计算杀伐业力：PVP行为权重更高
    var combatKarma = action.IsPvp ? action.Value * 1.5 : action.Value;
    
    // 更新区域业力分布
    RegionalKarma[action.Region] += combatKarma;
}
```

---

## 代码组织

### 文件结构

```csharp
// 1. using 语句
using System;
using System.Collections.Generic;

// 2. 命名空间
namespace Chaos.Legend;

// 3. 类定义
public class LegendSystem : ILegendSystem
{
    // 3.1 常量
    public const double MaxDimensionValue = 1.0;
    
    // 3.2 静态字段
    private static readonly Dictionary<PlayerId, LegendImprint> _cache = [];
    
    // 3.3 实例字段
    private readonly IKarmaSystem _karmaSystem;
    
    // 3.4 构造函数
    public LegendSystem(IKarmaSystem karmaSystem)
    {
        _karmaSystem = karmaSystem;
    }
    
    // 3.5 属性
    public int ImprintCount => _cache.Count;
    
    // 3.6 公共方法
    public LegendImprint GetImprint(PlayerId playerId) { }
    
    // 3.7 私有方法
    private void ValidateImprint(LegendImprint imprint) { }
}
```

---

## 最佳实践

1. **单一职责**：每个类只负责一个功能
2. **依赖注入**：通过构造函数注入依赖
3. **不可变性**：优先使用只读属性和 init 访问器
4. **异常处理**：使用 `ArgumentNullException.ThrowIfNull()` 等辅助方法
