# Tasks & Repo Auditor: MCP Server (Deep Dive PoC)

This project is a high-performance Model Context Protocol (MCP) Server built with .NET 9. It demonstrates how to transform a standard Web API into an AI-driven agent capable of orchestrating tasks and auditing GitHub repositories in real-time.

## Features
**1. Tasks API**

A robust Minimal API serving as the backend for task management:

- Full CRUD: Create, Read, Update, and Delete tasks.
- Smart Filters: Search tasks by keywords and identify overdue items.
- In-Memory Store: Lightweight, zero-config list-based database.
- Notification System: Internal endpoint for tracking task lifecycle events.

**2. MCP Server Capabilities**

The core engine that translates AI intent into code execution:

- Multi-Client Integration: Simultaneous communication with local Task API and GitHub REST API.
- AI Prompts: Pre-defined templates for "Daily Planning" and "Repository Auditing."
- Advanced Logging: Clean stderr logging to ensure protocol stability during debugging.

## Project Structure

- /Tasks.Api - The backend service (Port: 7376).
- /Tasks.McpServer - The MCP gateway using the latest ModelContextProtocol SDK.
  - ApiClients: Optimized HttpClient implementations for Tasks and GitHub.
  - Tools: The "skills" provided to the AI (CRUD + Repo Sync).
  - Prompts: Structured workflows for the LLM to follow.

## MCP Tools Catalog

### Tasks Tools

| Tool Name | Description |
|-----------|-------------|
| `GetAllTasksAsync` | Fetches the full workload overview. |
| `SearchTasksAsync` | Finds tasks using semantic keywords. |
| `CreateTaskAsync` | Adds a new task to the system. |
| `UpdateTaskAsync` | Modifies existing tasks (Status, Title, etc.). |
| `DeleteTaskAsync` | Removes a task by ID. |
| `GetOverdueTasksAsync` | Isolates urgent, past-due items. |

### GitHub & Integration Tools

| Tool Name | Description |
|-----------|-------------|
| `GetLatestIssuesAsync` | Fetches filtered open issues (excluding PRs) from any public repo. |
| `GetRepoStatsAsync` | Returns repo health metrics (Stars, Open Issues count). |
| `SyncLatestIssuesToTasksAsync` | Orchestrator Tool: Automatically fetches the top issues from GitHub and creates corresponding tasks in the local API. |

## AI Prompts (Workflows)

- `daily-planner`: Instructs the AI to analyze all tasks, identify overdue items, and suggest a 3-point priority list for the user.
- `auto-sync-repo`: A specialized agent workflow that audits a specific repository and syncs the latest issues into the local task database in one click.

## MCP Resources

| Resource Name | Description |
|-----------|-------------|
| `GitHub Repository README` | Fetches the README file of a GitHub repository. |

## Configuration & Installation

To use this server with Claude Desktop or any MCP-compatible IDE, add the following to your configuration file (e.g., %APPDATA%\Labs\Claude\claude_desktop_config.json):

```json
{
  "inputs": [],
  "servers": {
    "TasksMcpServer": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "<YOUR-PROJECT-PATH>\\Tasks.McpServer\\Tasks.McpServer.csproj"
      ]
    }
  }
}
```

### Prerequisites
- .NET 10 SDK
- (Optional) GitHub Personal Access Token (for higher rate limits).

## Deep Dive: Why this matters?

This PoC illustrates the Agentic Shift in software development. Instead of a developer manually checking GitHub issues and updating a Jira/Task board, the MCP Server allows the AI to act as a bridge, automating the data flow through natural language commands.