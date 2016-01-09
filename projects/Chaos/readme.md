# Chaos Core Module

**Chaos 引擎核心模块 - 涌现式世界模拟引擎**

---

## 模块概述

Chaos 模块是 Chaos 引擎的核心实现，负责处理游戏世界的涌现式模拟、状态管理和玩家行为追踪。本模块实现了白皮书中描述的核心系统，包括传奇系统、天理系统、业力系统和气运系统。

---

## 核心系统

### 🎭 传奇系统（Legend System）

记录角色所有关键行为的多维向量系统，是角色在世界上留下的不可磨灭的因果指纹。

#### 核心维度

| 维度 | 标识符 | 描述 | 数据范围 |
|:---|:---|:---|:---|
| 征伐之痕 | `Conquest` | PVE 与 PVP 偏好，激进果敢或谋定后动 | `[-1.0, 1.0]` |
| 羁绊之痕 | `Bond` | 独行侠或团队轴心，引领者或协作者 | `[-1.0, 1.0]` |
| 货殖之痕 | `Commerce` | 生产者、贸易家或市场操纵者 | `[-1.0, 1.0]` |
| 开拓之痕 | `Exploration` | 安土重迁或向往未知边疆 | `[-1.0, 1.0]` |
| 天理之痕 | `Order` | 维护秩序或倾向混沌 | `[-1.0, 1.0]` |
| 地缘烙印 | `Regional` | 对各区域势力的归属感与贡献度 | `Dictionary<RegionId, double>` |

#### 职业倾向系统

新手引导解决方案，解决沙盒游戏初期迷茫问题。

**核心机制**：
1. **零数值加成**：职业倾向不提供任何实际战斗属性或技能加成
2. **零样本推荐算法**：利用初始倾向作为冷启动阶段的推荐权重
3. **抗工作室设计**：初始任务链包含非脚本化的人类交互

**职业倾向类型**：
- 佣兵（Mercenary）
- 游商（Merchant）
- 学者（Scholar）
- 游侠（Ranger）
- 方士（Alchemist）

#### 传奇称号涌现

当传奇数据在某一维度持续突破阈值，世界会为玩家凝结出传奇称号。

**示例称号**：

| 称号 | 触发条件 | 效果 |
|:---|:---|:---|
| 绿林故交 | 在某片森林活跃极久，且多次通过"气运举报"清除脚本 | 动物不主动攻击，可感知该区域业力失衡 |
| 角斗场之星 | 在PVP区域获得一定数量的真人观众认可 | 解锁独特外观动作，比赛时自动发送观战邀请 |

---

### ⚖️ 天理系统（Heavenly Principle System）

世界的宏观调配者，由全体玩家的共业驱动。

#### 天理参数

| 参数 | 标识符 | 范围 | 描述 |
|:---|:---|:---|:---|
| 王朝气数 | `DynastyFate` | `[0, 1]` | 受玩家完成官府任务、剿匪影响 |
| 灵气潮汐 | `SpiritTide` | `[-1, 1]` | 受玩家使用术法技能频率影响 |
| 格物致知 | `Innovation` | `[-1, 1]` | 受玩家发明新技法、蓝图的熵减行为影响 |

#### 运营模式

运营团队仅需通过天理面板观察参数并投放催化剂，运营人力成本降至传统 MMO 的 5% 以下。

---

### 🎲 业力系统（Karma System）

社会交互的应力单位，是天理系统运算的基础。

#### 业力类型

```csharp
public enum KarmaType
{
    Combat,    // 杀伐业力
    Trade,     // 流通业力
    Ecology,   // 生灭业力（正为破坏，负为滋养）
    Order,     // 秩序/混沌业力
}
```

#### 业力计算

```text
F(x, y, z, t) = { K₁, K₂, K₃, ... }
```

业力值基于玩家行为、位置、时间等维度进行实时计算和累积。

---

### 🛡️ 气运系统（Fortune System）

双轨防御体系，通过经济学手段而非粗暴封禁来使作弊行为无利可图。

#### 轨道一：系统侧 - 气运剥夺

| 嫌疑等级 | 气运状态 | 游戏内叙事反馈 | 工作室感知成本 |
|:---|:---|:---|:---|
| 1 | 时运不济 | 材料掉率、制作暴击率微降 | "这波样本数据有偏差？" |
| 2 | 财星隐匿 | NPC回收价降低，强化失败率提升 | "是不是物价暗改了？" |
| 3 | 业力缠身 | 怪物仇恨半径异常，优先攻击该角色 | "脚本寻路逻辑需要重写。" |
| 4 | 天厌人弃 | 制造品必然附带负面词条，无法进入高级流通市场 | **所有前期成本沉没** |

#### 轨道二：玩家侧 - 诅咒（主动举报）

- **机制**：被多名高传奇等级玩家诅咒的目标，跳过系统检测缓冲期，直接进入业力缠身状态
- **反滥用**：如果诅咒对象实为正常玩家，诅咒者将承受业力反噬

---

## API 设计

### 传奇系统 API

```csharp
public interface ILegendSystem
{
    LegendImprint GetImprint(PlayerId playerId);
    
    void UpdateImprint(PlayerId playerId, PlayerAction action);
    
    Title? CheckTitleEmergence(PlayerId playerId);
    
    Orientation GetOrientation(PlayerId playerId);
}
```

### 天理系统 API

```csharp
public interface IHeavenlyPrincipleSystem
{
    double GetParameter(HeavenlyParameter param);
    
    void UpdateParameters(GlobalStatistics globalStats);
    
    void TriggerCatalyst(CatalystEvent catalystEvent);
    
    WorldTrends GetWorldTrends();
}
```

### 业力系统 API

```csharp
public interface IKarmaSystem
{
    Karma CalculateKarma(PlayerAction action, ActionContext context);
    
    RegionalKarma GetRegionalKarma(RegionId region);
    
    void ApplyKarmaEffects(PlayerId target, Karma karma);
}
```

### 气运系统 API

```csharp
public interface IFortuneSystem
{
    FortuneLevel CheckFortuneLevel(PlayerId playerId);
    
    void ApplyDeprivation(PlayerId playerId, byte level);
    
    CurseResult ProcessCurse(PlayerId curser, PlayerId target);
    
    bool CheckCurseBacklash(PlayerId curser, PlayerId target);
}
```

---

## 数据结构

### 传奇印记（LegendImprint）

```csharp
public class LegendImprint
{
    public PlayerId PlayerId { get; init; }
    
    public double Conquest { get; set; }       // 征伐之痕
    
    public double Bond { get; set; }           // 羁绊之痕
    
    public double Commerce { get; set; }       // 货殖之痕
    
    public double Exploration { get; set; }    // 开拓之痕
    
    public double Order { get; set; }          // 天理之痕
    
    public Dictionary<RegionId, double> Regional { get; set; }  // 地缘烙印
    
    public List<PermanentAnchor> PermanentAnchors { get; set; }  // 永久锚点
    
    public DateTime LastUpdated { get; set; }
}
```

### 天理参数（HeavenlyParameters）

```csharp
public class HeavenlyParameters
{
    public double DynastyFate { get; set; }       // 王朝气数 [0, 1]
    
    public double SpiritTide { get; set; }        // 灵气潮汐 [-1, 1]
    
    public double Innovation { get; set; }        // 格物致知 [-1, 1]
    
    public double NaturalForce { get; set; }      // 自然之力 [-1, 1]
    
    public double CulturalFusion { get; set; }    // 文化融合度 [0, 1]
}
```

### 业力向量（KarmaVector）

```csharp
public class KarmaVector
{
    public double Combat { get; set; }    // 杀伐业力
    
    public double Trade { get; set; }     // 流通业力
    
    public double Ecology { get; set; }   // 生灭业力
    
    public double Order { get; set; }     // 秩序/混沌业力
}
```

### 气运状态（FortuneState）

```csharp
public class FortuneState
{
    public byte Level { get; set; }                   // 气运等级 (0-4)
    
    public FortuneStatus Status { get; set; }         // 气运状态
    
    public List<FortuneEffect> Effects { get; set; }  // 当前效果
    
    public uint CurseCount { get; set; }              // 被诅咒次数
    
    public DateTime LastCheck { get; set; }
}

public enum FortuneStatus
{
    Normal,         // 正常
    Unlucky,        // 时运不济
    WealthHidden,   // 财星隐匿
    KarmaBound,     // 业力缠身
    Abandoned,      // 天厌人弃
}
```

---

## 使用示例

### 更新玩家传奇印记

```csharp
using Chaos.Legend;
using Chaos.Models;

var legendSystem = new LegendSystem();

var action = new PlayerAction
{
    Type = ActionType.KillMonster,
    MonsterId = new MonsterId(12345),
    Region = new RegionId(100),
    IsPvp = false,
};

legendSystem.UpdateImprint(playerId, action);

var title = legendSystem.CheckTitleEmergence(playerId);
if (title is not null)
{
    Console.WriteLine($"玩家获得新称号: {title.Name}");
}
```

### 查询天理参数

```csharp
using Chaos.Heavenly;
using Chaos.Models;

var heavenlySystem = new HeavenlyPrincipleSystem();

var dynastyFate = heavenlySystem.GetParameter(HeavenlyParameter.DynastyFate);
var spiritTide = heavenlySystem.GetParameter(HeavenlyParameter.SpiritTide);

Console.WriteLine($"王朝气数: {dynastyFate}");
Console.WriteLine($"灵气潮汐: {spiritTide}");
```

### 处理玩家诅咒

```csharp
using Chaos.Fortune;
using Chaos.Models;

var fortuneSystem = new FortuneSystem();

var result = fortuneSystem.ProcessCurse(curserId, targetId);

switch (result)
{
    case CurseResult.Success:
        Console.WriteLine("诅咒成功，目标进入业力缠身状态");
        break;
    case CurseResult.Backlash:
        Console.WriteLine("诅咒失败，触发业力反噬");
        break;
    case CurseResult.InsufficientLegend:
        Console.WriteLine("传奇值不足，无法发起诅咒");
        break;
}
```

---

## 性能考虑

### 应力计算优化

- **降频计算**：共识场应力以 30 秒至 1 分钟的间隔更新
- **批量聚合**：将过去周期内的玩家行为按类型聚合后一次性计算
- **稀疏存储**：仅存储应力值显著偏离 0 的区域

### 数据存储

- **分片存储**：按区域分片存储业力数据
- **冷热分离**：活跃玩家数据保存在内存，历史数据归档
- **事件溯源**：关键状态变更采用仅追加日志模式

---

## 测试

```bash
# 运行单元测试
dotnet test

# 运行特定项目测试
dotnet test projects/Chaos/tests

# 运行性能测试
dotnet run --project projects/Chaos/benchmarks

# 生成测试覆盖率报告
dotnet test --collect:"XPlat Code Coverage"
```

---
