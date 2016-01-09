# 涌现事件设计

本文档介绍如何设计涌现事件模板，定义事件的触发条件、生成规则和效果。

---

## 事件模板结构

```csharp
public class EmergenceTemplate
{
    public string Id { get; set; }              // 模板唯一标识
    public string Name { get; set; }            // 事件名称
    public EmergenceLevel Level { get; set; }   // 影响范围
    
    // 触发条件
    public TriggerCondition[] Triggers { get; set; }
    
    // 上下文参数
    public ContextParameter[] Parameters { get; set; }
    
    // 生成规则
    public GenerationRule[] Rules { get; set; }
    
    // 效果定义
    public Effect[] Effects { get; set; }
}
```

---

## 触发条件设计

### 单条件触发

```csharp
var wildfireTemplate = new EmergenceTemplate
{
    Id = "wildfire",
    Name = "野火",
    Level = EmergenceLevel.Regional,
    
    Triggers = new[]
    {
        new TriggerCondition
        {
            StressType = StressType.Ecology,
            Operator = ComparisonOperator.GreaterThan,
            Value = 0.8,
            RegionTypes = new[] { RegionType.Forest, RegionType.Grassland }
        }
    }
};
```

### 多条件组合

```csharp
var beastTideTemplate = new EmergenceTemplate
{
    Id = "beast_tide",
    Name = "兽潮",
    Level = EmergenceLevel.Regional,
    
    Triggers = new[]
    {
        // 条件组：生态破坏 AND 战斗烈度高
        new TriggerCondition
        {
            Logic = LogicOperator.And,
            Conditions = new[]
            {
                new TriggerCondition
                {
                    StressType = StressType.Ecology,
                    Operator = ComparisonOperator.GreaterThan,
                    Value = 0.7
                },
                new TriggerCondition
                {
                    StressType = StressType.Combat,
                    Operator = ComparisonOperator.GreaterThan,
                    Value = 0.5
                }
            }
        }
    }
};
```

---

## 上下文参数

参数从触发信号中提取，用于填充事件的具体内容。

```csharp
Parameters = new[]
{
    // 规模：基于应力超出阈值的程度
    new ContextParameter
    {
        Name = "scale",
        Source = ParameterSource.StressOverflow,
        Mapping = new LinearMapping(0.0, 1.0, 1, 10)  // 映射到 1-10 级
    },
    
    // 主要贡献者：从贡献者图谱提取
    new ContextParameter
    {
        Name = "primary_contributor",
        Source = ParameterSource.TopContributor,
        Rank = 1
    },
    
    // 触发位置
    new ContextParameter
    {
        Name = "epicenter",
        Source = ParameterSource.TriggerPosition
    }
}
```

---

## 生成规则

规则决定事件如何实例化到世界中。

```csharp
Rules = new[]
{
    // 生成怪物群
    new GenerationRule
    {
        Type = GenerationType.SpawnMonsters,
        Template = "angry_treant",
        Count = new ParameterReference("scale"),  // 引用上下文参数
        Position = new AreaDistribution
        {
            Center = new ParameterReference("epicenter"),
            Radius = 500  // 米
        },
        
        // 怪物属性
        Overrides = new MonsterOverrides
        {
            Aggression = AggressionLevel.High,
            TargetPreference = new ParameterReference("primary_contributor")
        }
    },
    
    // 改变地形
    new GenerationRule
    {
        Type = GenerationType.ModifyTerrain,
        Area = new ParameterReference("epicenter"),
        Modification = TerrainModification.Burn,
        Duration = TimeSpan.FromDays(7)
    }
}
```

---

## 效果定义

事件触发后对世界的影响。

```csharp
Effects = new[]
{
    // 记录历史
    new Effect
    {
        Type = EffectType.RecordHistory,
        Description = "{primary_contributor}的过度伐木激怒了森林之灵"
    },
    
    // 更新天理参数
    new Effect
    {
        Type = EffectType.UpdateParameter,
        Parameter = HeavenlyParameter.NaturalForce,
        Delta = -0.05
    },
    
    // 触发后续事件链
    new Effect
    {
        Type = EffectType.ScheduleEvent,
        TemplateId = "forest_recovery",
        Delay = TimeSpan.FromDays(30),
        Condition = new TriggerCondition
        {
            StressType = StressType.Ecology,
            Operator = ComparisonOperator.LessThan,
            Value = 0.3
        }
    }
}
```

---

## 注册模板

```csharp
// 注册单个模板
await engine.Emergence.RegisterTemplateAsync(wildfireTemplate);

// 批量注册
await engine.Emergence.RegisterTemplatesAsync(new[]
{
    wildfireTemplate,
    beastTideTemplate,
    treantAwakeningTemplate
});
```

---

## 涌现层级

| 层级 | 影响范围 | 示例事件 |
|:---|:---|:---|
| Micro | 单个玩家/小队 | 发现隐秘洞穴 |
| Local | 单个城镇/区域 | 盗贼设立路障 |
| Regional | 多个相连区域 | 森林大火蔓延 |
| World | 整个游戏世界 | 帝都陷落 |
