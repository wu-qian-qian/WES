# 请参阅 https://aka.ms/customizecontainer 以了解如何自定义调试容器，以及 Visual Studio 如何使用此 Dockerfile 生成映像以更快地进行调试。

# 根据将生成或运行容器的主机的操作系统，可能需要更改 FROM 语句中指定的映像。
# 有关详细信息，请参阅 https://aka.ms/containercompat

# 此阶段用于在快速模式(默认为调试配置)下从 VS 运行时
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080


# 此阶段用于生成服务项目
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2019 AS build
# 安装 Visual Studio 生成工具，进行 aot 发布时会需要它们
# 注意: 使用 Visual Studio 生成工具需要有效的 Visual Studio 许可证。
RUN curl -SL --output vs_buildtools.exe https://aka.ms/vs/17/release/vs_buildtools.exe
RUN vs_buildtools.exe --installPath C:\BuildTools --add Microsoft.VisualStudio.Component.VC.Tools.x86.x64 Microsoft.VisualStudio.Component.Windows10SDK.19041 --quiet --wait --norestart --nocache
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Identity/Identity.csproj", "Services/Identity/"]
RUN dotnet restore "./Services/Identity/Identity.csproj"
COPY . .
WORKDIR "/src/Services/Identity"
RUN dotnet build "./Identity.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# 此阶段用于发布要复制到最终阶段的服务项目
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=true

# 此阶段在生产中使用，或在常规模式下从 VS 运行时使用(在不使用调试配置时为默认值)
FROM mcr.microsoft.com/dotnet/runtime:8.0-nanoserver-1809 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["Identity.exe"]