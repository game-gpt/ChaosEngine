# 快速开始

## 环境要求

### 必需组件

| 组件 | 版本要求 | 说明 |
|------|----------|------|
| .NET SDK | 8.0 或更高 | 核心运行时环境 |
| Git | 最新版 | 版本控制 |

### 推荐工具

| 工具 | 用途 |
|------|------|
| Visual Studio 2022 / Rider | IDE 开发环境 |
| Docker | 容器化部署 |

---

## 获取源码

```bash
# 克隆仓库
git clone https://github.com/your-org/chaos-engine.git

# 进入项目目录
cd chaos-engine
```

---

## 构建项目

```bash
# 还原依赖
dotnet restore

# 构建项目
dotnet build

# 运行测试
dotnet test
```

---

## 项目结构概览

```
chaos-engine/
├── src/
│   ├── ChaosEngine.Core/          # 核心引擎
│   ├── ChaosEngine.Consensus/     # 共识场/业力系统
│   ├── ChaosEngine.Emergence/     # 涌现生成层
│   ├── ChaosEngine.Legends/       # 传奇系统
│   ├── ChaosEngine.Karma/         # 天理系统
│   └── ChaosEngine.Assets/        # 资产记录层
├── tests/                          # 测试项目
├── docs/                           # 文档
└── samples/                        # 示例项目
```

---

## 核心模块说明

| 模块 | 职责 |
|------|------|
| **Core** | 基础交互层，玩家行为事件处理 |
| **Consensus** | 业力系统，社会应力计算与阈值监控 |
| **Emergence** | 涌现生成层，事件模板与实例化 |
| **Legends** | 传奇系统，玩家成长与角色涌现 |
| **Karma** | 天理系统，宏观趋势参数管理 |
| **Assets** | 资产记录层，知识资产存证 |

---

## 下一步

1. 阅读 [项目介绍](./introduction.md) 了解核心设计理念
2. 查看核心系统文档深入了解各模块
3. 运行示例项目体验引擎功能

---

## 常见问题

### Q: Chaos 引擎支持哪些底层游戏引擎？

Chaos 引擎架构于传统游戏引擎（如 Unreal Engine 或专有服务器框架）之上，作为独立的世界模拟与涌现逻辑层运行。

### Q: 如何参与开发？

请查阅项目贡献指南，提交 Pull Request 或 Issue。
