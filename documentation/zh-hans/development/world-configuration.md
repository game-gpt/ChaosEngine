# 世界配置

本文档介绍如何配置 Chaos 引擎的世界参数、区域设置和初始状态。

---

## 天理参数配置

天理参数决定世界的宏观趋势，影响所有涌现事件的触发概率。

### 配置示例

```csharp
var worldConfig = new WorldConfiguration
{
    Parameters = new HeavenlyParameters
    {
        DynastyFate = 0.65,      // 王朝气数 [0, 1]
        SpiritTide = 0.3,        // 灵气潮汐 [-1, 1]
        Innovation = -0.2,       // 格物致知 [-1, 1]
        NaturalForce = 0.1,      // 自然之力 [-1, 1]
        CulturalFusion = 0.4,    // 文化融合度 [0, 1]
    },
    
    // 参数变化速率限制
    ParameterConstraints = new ParameterConstraints
    {
        MaxDailyChange = 0.05,   // 每日最大变化幅度
        MomentumFactor = 0.8,    // 惯性因子
    }
};

await engine.ConfigureWorldAsync(worldConfig);
```

### 参数影响

| 参数 | 影响范围 |
|:---|:---|
| DynastyFate | S_order 阈值、NPC 守卫强度、犯罪率 |
| SpiritTide | 魔法效果强度、魔力生物出现频率 |
| Innovation | 新配方发现概率、工匠技能提升速度 |
| NaturalForce | S_ecology 恢复速度、野怪强度 |

---

## 区域配置

每个区域有独立的应力阈值和涌现规则。

### 区域定义

```csharp
var regions = new[]
{
    new RegionConfiguration
    {
        Id = RegionId.From("eastern_forest"),
        Name = "东部森林",
        Type = RegionType.Forest,
        
        // 应力阈值配置
        Thresholds = new StressThresholds
        {
            Ecology = new Threshold(-0.6, 0.8),  // [下限, 上限]
            Combat = new Threshold(-0.5, 0.7),
            Trade = new Threshold(-0.3, 0.9),
            Order = new Threshold(-0.8, 0.5),
        },
        
        // 应力衰减速率
        DecayRates = new StressDecayRates
        {
            Ecology = 0.001,   // 每秒衰减
            Combat = 0.002,
            Trade = 0.0005,
        },
        
        // 关联的涌现事件模板
        EventTemplates = new[] { "wildfire", "treant_awakening", "beast_tide" }
    },
    
    new RegionConfiguration
    {
        Id = RegionId.From("imperial_city"),
        Name = "帝都",
        Type = RegionType.Urban,
        
        Thresholds = new StressThresholds
        {
            Order = new Threshold(-0.95, 0.3),  // 城市极难发生动乱
        },
        
        EventTemplates = new[] { "coup", "plague", "festival" }
    }
};

await engine.RegisterRegionsAsync(regions);
```

---

## 初始状态设置

### 贡献者图谱种子

为世界设置初始的"历史痕迹"，让新世界不至于完全空白。

```csharp
var seedEvents = new[]
{
    new HistoricalEvent
    {
        Type = "ancient_battle",
        Region = RegionId.From("eastern_forest"),
        Timestamp = GameTime.Now - TimeSpan.FromDays(365),
        Contributors = new[] { "ancient_hero_001" },
        KarmaImpact = new KarmaVector { Combat = 0.3, Ecology = 0.2 }
    }
};

await engine.SeedHistoryAsync(seedEvents);
```

---

## 配置验证

```csharp
var validationResult = await engine.ValidateConfigurationAsync();

if (!validationResult.IsValid)
{
    foreach (var error in validationResult.Errors)
    {
        Console.WriteLine($"配置错误: {error.Message}");
    }
}
```

### 常见验证错误

| 错误 | 原因 |
|:---|:---|
| `ThresholdRangeInvalid` | 阈值下限大于上限 |
| `MissingEventTemplate` | 区域引用了未注册的模板 |
| `ParameterOutOfRange` | 天理参数超出有效范围 |
