FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app

# copy everything and build app
COPY . .
RUN msbuild WindowsContainer.Wrapper.csproj -t:restore -p:Configuration=Release

FROM mcr.microsoft.com/dotnet/framework/runtime:4.8
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]


WORKDIR /app
COPY --from=build /app/bin/Release ./

#install service using powershell or installutil.exe 
RUN "C:/Windows/Microsoft.NET/Framework64/v4.0.30319/installutil.exe" /LogToConsole=true /ShowCallStack C:/app/WindowsContainer.Wrapper.exe ;

#The container stays up as long as this process is running.
ENTRYPOINT ["powershell"] 
CMD Start-Service -Name "MyService"; \
    Get-EventLog -LogName System -After (Get-Date).AddHours(-1) | Format-List ;\
    $idx = (get-eventlog -LogName System -Newest 1).Index; \
    while ($true) \
    {; \
      start-sleep -Seconds 1; \
      $idx2  = (Get-EventLog -LogName System -newest 1).index; \
      get-eventlog -logname system -newest ($idx2 - $idx) |  sort index | Format-List; \
      $idx = $idx2; \
    }