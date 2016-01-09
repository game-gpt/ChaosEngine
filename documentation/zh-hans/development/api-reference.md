# API 参考

本文档提供 Chaos 引擎核心 API 的使用示例。

---

## 世界配置 API

### IWorldConfiguration

```csharp
public interface IWorldConfiguration
{
    Task ConfigureAsync(WorldConfiguration config);
    Task<WorldConfiguration> GetAsync();
    Task<ValidationResult> ValidateAsync();
}

// 使用示例
var worldConfig = new WorldConfiguration
{
    Parameters = new HeavenlyParameters { DynastyFate = 0.65 }
};

await engine.World.ConfigureAsync(worldConfig);
```

---

## 共识场 API

### IConsensusField

```csharp
public interface IConsensusField
{
    // 查询区域应力
    Task<StressVector> GetStressAsync(RegionId region);
    
    // 查询贡献者图谱
    Task<ContributorGraph> GetContributorsAsync(RegionId region);
    
    // 订阅阈值事件
    event EventHandler<ThresholdReachedEventArgs> OnThresholdReached;
}

// 使用示例
var stress = await engine.Consensus.GetStressAsync(RegionId.From("eastern_forest"));
Console.WriteLine($"生态应力: {stress.Ecology}");

// 监控涌现触发
engine.Consensus.OnThresholdReached += (sender, args) =>
{
    Console.WriteLine($"区域 {args.Region} 应力突破阈值!");
    Console.WriteLine($"触发类型: {args.StressType}");
    Console.WriteLine($"当前值: {args.CurrentValue}");
};
```

---

## 涌现引擎 API

### IEmergenceEngine

```csharp
public interface IEmergenceEngine
{
    // 注册事件模板
    Task RegisterTemplateAsync(EmergenceTemplate template);
    Task RegisterTemplatesAsync(IEnumerable<EmergenceTemplate> templates);
    
    // 查询可用模板
    Task<IEnumerable<EmergenceTemplate>> GetTemplatesAsync(RegionId region);
    
    // 手动触发涌现（调试用）
    Task<EmergenceResult> TriggerAsync(RegionId region, string templateId);
    
    // 订阅涌现事件
    event EventHandler<EmergenceEventArgs> OnEmergence;
}

// 使用示例
engine.Emergence.OnEmergence += (sender, args) =>
{
    Console.WriteLine($"涌现事件: {args.Event.Name}");
    Console.WriteLine($"影响区域: {args.Event.Region}");
    Console.WriteLine($"主要贡献者: {args.Event.PrimaryContributor}");
};
```

---

## 传奇系统 API

### ILegendSystem

```csharp
public interface ILegendSystem
{
    // 获取玩家印记
    Task<LegendImprint> GetImprintAsync(PlayerId player);
    
    // 更新印记（通常由引擎自动调用）
    Task UpdateImprintAsync(PlayerId player, PlayerAction action);
    
    // 检查称号涌现
    Task<Title?> CheckTitleEmergenceAsync(PlayerId player);
    
    // 获取玩家称号列表
    Task<IEnumerable<Title>> GetTitlesAsync(PlayerId player);
}

// 使用示例
var imprint = await engine.Legend.GetImprintAsync(playerId);
Console.WriteLine($"征伐之痕: {imprint.Conquest}");
Console.WriteLine($"货殖之痕: {imprint.Commerce}");

var newTitle = await engine.Legend.CheckTitleEmergenceAsync(playerId);
if (newTitle != null)
{
    await BroadcastAsync($"恭喜 {playerId} 获得「{newTitle.Name}」称号!");
}
```

---

## 天理系统 API

### IHeavenlyPrincipleSystem

```csharp
public interface IHeavenlyPrincipleSystem
{
    // 获取参数
    Task<double> GetParameterAsync(HeavenlyParameter param);
    
    // 获取所有参数
    Task<HeavenlyParameters> GetAllParametersAsync();
    
    // 订阅参数变化
    event EventHandler<ParameterChangedEventArgs> OnParameterChanged;
}

// 使用示例
var dynastyFate = await engine.Heavenly.GetParameterAsync(HeavenlyParameter.DynastyFate);

engine.Heavenly.OnParameterChanged += (sender, args) =>
{
    if (args.Parameter == HeavenlyParameter.DynastyFate && args.NewValue < 0.3)
    {
        Console.WriteLine("警告: 王朝气数过低，可能触发重大事件!");
    }
};
```

---

## 气运系统 API

### IFortuneSystem

```csharp
public interface IFortuneSystem
{
    // 获取气运等级
    Task<FortuneLevel> GetLevelAsync(PlayerId player);
    
    // 处理诅咒举报
    Task<CurseResult> ProcessCurseAsync(PlayerId curser, PlayerId target);
    
    // 订阅气运变化
    event EventHandler<FortuneChangedEventArgs> OnFortuneChanged;
}

// 使用示例
var fortuneLevel = await engine.Fortune.GetLevelAsync(suspiciousPlayer);
if (fortuneLevel >= FortuneLevel.KarmaBound)
{
    Console.WriteLine($"玩家 {suspiciousPlayer} 已被标记为高脚本嫌疑");
}
```

---

## 催化剂系统 API

### ICatalystSystem

```csharp
public interface ICatalystSystem
{
    // 触发催化剂
    Task TriggerAsync(CatalystEvent catalyst);
    
    // 获取建议
    Task<IEnumerable<CatalystSuggestion>> GetSuggestionsAsync();
    
    // 获取投放历史
    Task<IEnumerable<CatalystRecord>> GetHistoryAsync(TimeSpan? period = null);
}

// 使用示例
var suggestions = await engine.Catalyst.GetSuggestionsAsync();
foreach (var suggestion in suggestions)
{
    Console.WriteLine($"建议: {suggestion.Name}");
    Console.WriteLine($"原因: {suggestion.Reason}");
}
```

---

## 依赖注入

```csharp
// 注册引擎服务
services.AddChaosEngine(options =>
{
    options.WorldConfigurationPath = "config/world.json";
    options.TemplatesPath = "config/templates";
});

// 获取引擎实例
public class GameService
{
    private readonly IChaosEngine _engine;
    
    public GameService(IChaosEngine engine)
    {
        _engine = engine;
    }
}
```
