# 催化剂事件

本文档介绍如何设计和管理催化剂事件，实现运营对世界的"无形干预"。

---

## 催化剂事件概念

催化剂事件是运营团队主动投放的干预手段，用于：
- 引导玩家注意力
- 加速/延缓涌现进程
- 创造节日氛围
- 修正系统失衡

**核心原则**：改变应力而非直接改变结果

---

## 催化剂类型

### 应力冲击型

直接对特定区域的共识场施加应力增量。

```csharp
var catalyst = new CatalystEvent
{
    Id = "royal_emissary_visit",
    Name = "王室密使的到来",
    Type = CatalystType.StressImpact,
    
    // 叙事描述
    Narrative = new NarrativeContent
    {
        Introduction = "一名来自帝都的王室密使出现在城镇广场...",
        Conclusion = "密使宣布调查结果，森林的愤怒已被唤醒。"
    },
    
    // 应力冲击
    StressImpact = new StressImpact
    {
        Region = RegionId.From("eastern_forest"),
        Type = StressType.Ecology,
        Delta = 0.05  // 推动应力突破阈值
    },
    
    // 前置任务（玩家参与）
    PreTasks = new[]
    {
        new CatalystTask
        {
            Type = TaskType.Investigation,
            Description = "调查森林生态恶化原因",
            Duration = TimeSpan.FromHours(24),
            PlayerContribution = true  // 玩家贡献影响最终效果
        }
    }
};

await engine.Catalyst.TriggerAsync(catalyst);
```

### 参数调整型

临时调整天理参数或阈值。

```csharp
var festivalCatalyst = new CatalystEvent
{
    Id = "spring_festival",
    Name = "春日祭典",
    Type = CatalystType.ParameterAdjustment,
    
    // 临时参数调整
    ParameterAdjustment = new ParameterAdjustment
    {
        Parameter = HeavenlyParameter.CulturalFusion,
        Modifier = 0.2,  // 临时 +0.2
        Duration = TimeSpan.FromDays(7)
    },
    
    // 阈值调整
    ThresholdAdjustment = new ThresholdAdjustment
    {
        Region = RegionId.From("imperial_city"),
        StressType = StressType.Trade,
        ThresholdMultiplier = 0.8  // 交易事件更容易触发
    }
};
```

### 事件链型

触发一系列关联事件。

```csharp
var storyChainCatalyst = new CatalystEvent
{
    Id = "dynasty_crisis",
    Name = "王朝危机",
    Type = CatalystType.EventChain,
    
    EventChain = new[]
    {
        new ChainEvent
        {
            TemplateId = "assassination_attempt",
            Delay = TimeSpan.Zero
        },
        new ChainEvent
        {
            TemplateId = "civil_war",
            Delay = TimeSpan.FromDays(7),
            Condition = new TriggerCondition
            {
                Parameter = HeavenlyParameter.DynastyFate,
                Operator = ComparisonOperator.LessThan,
                Value = 0.4
            }
        },
        new ChainEvent
        {
            TemplateId = "new_dynasty",
            Delay = TimeSpan.FromDays(30),
            Condition = new TriggerCondition
            {
                Parameter = HeavenlyParameter.DynastyFate,
                Operator = ComparisonOperator.GreaterThan,
                Value = 0.6
            }
        }
    }
};
```

---

## 投放时机

### 监控指标

```csharp
// 监控应力停滞
var stagnantRegions = await engine.Monitor.GetStagnantRegionsAsync(
    threshold: 0.1,      // 距离阈值 0.1 以内
    duration: TimeSpan.FromDays(7)  // 持续 7 天
);

if (stagnantRegions.Any())
{
    // 投放催化剂推动涌现
    await engine.Catalyst.SuggestCatalystAsync(stagnantRegions);
}
```

### 自动建议

```csharp
// 引擎自动分析并建议催化剂
var suggestions = await engine.Catalyst.GetSuggestionsAsync();

foreach (var suggestion in suggestions)
{
    Console.WriteLine($"建议投放: {suggestion.Name}");
    Console.WriteLine($"原因: {suggestion.Reason}");
    Console.WriteLine($"预期效果: {suggestion.ExpectedOutcome}");
}
```

---

## 最佳实践

### 符合世界观

```csharp
// 好的设计：有叙事支撑
Narrative = "王室密使调查森林生态恶化..."

// 不好的设计：生硬的数值调整
// 直接修改参数，无叙事解释
```

### 提供机遇而非奖励

```csharp
// 好的设计：创造机遇窗口
Effects = new[]
{
    new Effect
    {
        Type = EffectType.PriceSurge,
        Commodity = "iron_ore",
        Multiplier = 2.0,
        Duration = TimeSpan.FromDays(3)
    }
}

// 不好的设计：直接发放奖励
// Effects = new[] { GiveGold(1000) }
```

### 让玩家参与

```csharp
// 玩家行为影响催化剂最终效果
PreTasks = new[]
{
    new CatalystTask
    {
        PlayerContribution = true,
        WeightFormula = "player_contributions.Sum() / total_required"
    }
}
```

---

## 催化剂面板

运营团队通过 Web 面板监控和投放催化剂：

| 功能 | 描述 |
|:---|:---|
| 世界状态总览 | 实时应力分布、天理参数 |
| 涌现预测 | 基于当前趋势预测即将触发的事件 |
| 催化剂库 | 预设的催化剂模板 |
| 投放历史 | 过往投放记录和效果分析 |
