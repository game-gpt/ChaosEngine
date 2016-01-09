# 快速开始

本文档帮助您快速搭建 Chaos 引擎开发环境。

---

## 环境要求

### 必需软件

| 软件 | 版本要求 | 说明 |
|:---|:---|:---|
| .NET SDK | 8.0 或更高 | 项目目标框架 |
| Git | 最新版 | 版本控制 |

### 推荐IDE

- **JetBrains Rider** - 推荐，功能强大
- **Visual Studio 2022** - 官方IDE
- **Visual Studio Code** - 轻量级选择

---

## 克隆仓库

```bash
git clone <repository-url>
cd ChaosEngine
```

---

## 构建项目

### 还原依赖

```bash
dotnet restore
```

### 编译项目

```bash
# Debug 模式
dotnet build

# Release 模式
dotnet build -c Release
```

---

## 运行测试

```bash
# 运行所有测试
dotnet test

# 运行特定项目测试
dotnet test projects/Chaos/tests

# 生成覆盖率报告
dotnet test --collect:"XPlat Code Coverage"
```

---

## 项目结构

```
ChaosEngine/
├── .github/                    # GitHub 配置
│   └── workflows/              # CI/CD 工作流
│       └── dotnet-ci.yml       # .NET CI 配置
├── projects/                   # 项目目录
│   └── Chaos/                  # 核心模块
│       ├── Chaos.csproj        # 项目文件
│       └── readme.md           # 模块文档
├── ChaosEngine.sln             # 解决方案文件
├── .editorconfig               # 编辑器配置
├── .gitignore                  # Git 忽略规则
└── License.md                  # 许可证
```

---

## 核心模块说明

Chaos 模块是引擎的核心实现，包含以下系统：

| 系统 | 说明 |
|:---|:---|
| 传奇系统 | 玩家行为追踪与称号涌现 |
| 天理系统 | 世界宏观参数调控 |
| 业力系统 | 社会交互应力计算 |
| 气运系统 | 反作弊防御机制 |

---

## 下一步

- 阅读 [编码规范](./coding-standards.md)
- 参考 [测试指南](./testing-guide.md)
- 查看 [模块文档](../../projects/Chaos/readme.md)
