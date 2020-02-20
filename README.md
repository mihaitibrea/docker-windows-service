
# Dockerization of a windows service written in .net framework

  

## Docker

* docker image build --no-cache -t windowsservicewrapper:latest -f .\ServiceWrapper.dockerfile .

* docker run windowsservicewrapper:latest

  

## Portainer

* Get-Service -Name "MyService"

* Stop-Service -Name "MyService"

  
  

## Usefull links:

 **Docker .net framework 4.8 runtime image**

* https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/runtime/windowsservercore-1909/Dockerfile

**Windows service**

* https://docs.microsoft.com/en-us/dotnet/framework/windows-services/walkthrough-creating-a-windows-service-application-in-the-component-designer

**Docker cli**

* https://docs.docker.com/engine/reference/commandline/cli/installutil

https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-install-and-uninstall-services

**Portainer**

* https://www.portainer.io/installation/

* check which type of docker host you have running (windows or linux) then use corresponding docker command

* Portainer Server should be enough