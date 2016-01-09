# 游戏开发指南

本文档面向使用 Chaos 引擎开发游戏的开发者，介绍如何利用引擎 API 构建涌现式游戏世界。

---

## 快速导航

| 文档 | 描述 |
|------|------|
| [世界配置](./world-configuration.md) | 配置世界参数、区域、初始状态 |
| [涌现事件设计](./emergence-events.md) | 设计事件模板与触发条件 |
| [催化剂事件](./catalyst-events.md) | 运营干预与事件投放 |
| [API 参考](./api-reference.md) | 核心 API 使用示例 |

---

## 开发流程概览

```
1. 世界配置
   └── 定义天理参数、区域应力阈值、初始状态

2. 事件模板设计
   └── 编写涌现事件模板、上下文参数

3. 游戏逻辑集成
   └── 调用引擎 API、处理涌现回调

4. 测试与调优
   └── 模拟玩家行为、验证涌现结果
```

---

## 核心概念

### 开发者角色转变

传统 MMO 开发者 → Chaos 引擎开发者

| 传统模式 | Chaos 模式 |
|:---|:---|
| 编写固定任务脚本 | 设计事件模板规则 |
| 手动放置 NPC/怪物 | 定义涌现生成条件 |
| 预设装备属性表 | 设计材料与工艺参数空间 |
| 编写剧情对话 | 编写叙事钩子规则 |

### 你需要关注的核心 API

```csharp
// 世界配置
IWorldConfiguration.Configure(parameters);

// 事件模板注册
IEmergenceEngine.RegisterTemplate(template);

// 应力监控
IConsensusField.OnThresholdReached += HandleEmergence;

// 催化剂投放
ICatalystSystem.Trigger(eventId);
```

---

## 与引擎维护者的区别

| 角色 | 关注点 | 文档目录 |
|:---|:---|:---|
| **游戏开发者** | 如何使用引擎构建游戏 | `development/` |
| **引擎维护者** | 如何维护和扩展引擎 | `maintenance/` |
