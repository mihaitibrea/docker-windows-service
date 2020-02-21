
# Dockerization of a windows service written in .net framework

The project inside is a console application that runs a windows service. This approach was used in order to help with debugging. This does not impact the windows service in any way.

The important bits of the project are:
 * the windows service
 * the installer for the windows service
 * docker file
 * logging in azure blob storage - to easily verify the service is running from the docker container
 
 Below you can find some helpful information. If you run into any problems or have any questions, reach out and I will try and help. 

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
* https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-install-and-uninstall-services

**Docker cli**

* https://docs.docker.com/engine/reference/commandline/cli/installutil


**Portainer**

* https://www.portainer.io/installation/

* check which type of docker host you have running (windows or linux) then use corresponding docker command

* Portainer Server should be enough
